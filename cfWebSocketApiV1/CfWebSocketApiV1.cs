// Crypto Facilities Ltd Web Socket API V1

// Copyright (c) 2019 Crypto Facilities

// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR
// IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


using System;
using System.Security.Cryptography;
using System.Text;
using WebSocketSharp;
using Newtonsoft.Json;
using System.Collections.Generic;
using WebSocket_v1_CSharp.cfWebSocketApiV1;
using System.Threading;

namespace WebSocket_v1_CSharp
{
    class CfWebSocketApi
    {

        public CfWebSocketApi(string uri, string apiKey, string apiSecret)
        {
            Url = uri;
            ApiKey = apiKey;
            ApiSecret = apiSecret;
            ChallengeRequestEvent = new CountdownEvent(1);
            WsApp = new Thread(() => Connect());
            WsApp.Start();
        }

        private string Url { get; }
        private string ApiKey { get; }
        private string ApiSecret { get; }
        private string Challenge { get; set; }
        private string SignedChallenge { get; set; }
        public CountdownEvent ChallengeRequestEvent { get; }
        public Thread WsApp { get; }
        public JsonSerializerSettings SerializerSettings { get; }
        private WebSocket Ws { get; set; }
        private readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public void SubscribePublic(string feed, IList<string> products = null)
        {
            PublicSubscription s = new PublicSubscription("subscribe", feed, products);
            string json = JsonConvert.SerializeObject(s, Formatting.None, Settings);
            Console.WriteLine("public subscribe: " + json);
            Ws.Send(json);
        }

        public void UnsubscribePublic(string feed, IList<string> products = null)
        {
            PublicSubscription s = new PublicSubscription("unsubscribe", feed, products);
            string json = JsonConvert.SerializeObject(s, Formatting.None, Settings);
            Console.WriteLine("public unsubscribe: " + json);
            Ws.Send(json);
        }

        public void SubscribePrivate(string feed)
        {
            if (Challenge == null)
            {
                RequestChallenge();
                ChallengeRequestEvent.Wait();
            }

            PrivateSubscription s = new PrivateSubscription("subscribe", feed, ApiKey, Challenge, SignedChallenge);
            string json = JsonConvert.SerializeObject(s, Formatting.None, Settings);
            Console.WriteLine("private subscribe: " + json);
            Ws.Send(json);
        }

        public void UnsubscribePrivate(string feed)
        {
            PrivateSubscription s = new PrivateSubscription("unsubscribe", feed);
            string json = JsonConvert.SerializeObject(s, Formatting.None, Settings);
            Console.WriteLine("private unsubscribe: " + json);
            Ws.Send(json);
        }

        private void Connect()
        {
            var definition = new { @event = "", message = "" };
            using (Ws = new WebSocket(Url))
            {
                Ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;

                Ws.OnMessage += (sender, e) =>
                {
                    string data = e.Data;
                    Console.WriteLine("Received => " + data);
                    if (data.Contains("\"challenge\""))
                    {
                        Challenge = JsonConvert.DeserializeAnonymousType(data, definition).message;
                        SignedChallenge = SignMessage(Challenge);
                        ChallengeRequestEvent.Signal();
                    }
                };

                Ws.OnOpen += (sender, e) =>
                    Console.WriteLine("Connected to {0}", Url);

                Ws.OnError += (sender, e) =>
                    Console.WriteLine("Received exception " + e.Message);

                Ws.OnClose += (sender, e) =>
                    Console.WriteLine("Connection Closed " + e.Reason);

                Ws.ConnectAsync();
                SetupPing();
                Console.ReadKey(true);
            }
        }

        private void RequestChallenge()
        {
            ChallengeRequest c = new ChallengeRequest("challenge", ApiKey);
            string json = JsonConvert.SerializeObject(c, Formatting.None, Settings);
            Console.WriteLine("requesting challenge: " + json);
            Ws.Send(json);
        }

        private void SetupPing()
        {
            var interval = 30000;
            Timer timer = null;

            //ep: must keep reference to timer, othrewise it will get garbage collected
            timer = new Timer(state =>
            {
                Ws.Ping();
                timer.Change(interval, Timeout.Infinite);
            },
            null, 30000, Timeout.Infinite);
        }

        // Signs a message
        private string SignMessage(String message)
        {
            //Step 1: hash the result of step 1 with SHA256
            var hash256 = new SHA256Managed();
            var hash = hash256.ComputeHash(Encoding.UTF8.GetBytes(message));

            //step 2: base64 decode apiPrivateKey
            var secretDecoded = (System.Convert.FromBase64String(ApiSecret));

            //step 3: use result of step 3 to hash the resultof step 2 with HMAC-SHA512
            var hmacsha512 = new HMACSHA512(secretDecoded);
            var hash2 = hmacsha512.ComputeHash(hash);

            //step 4: base64 encode the result of step 4 and return
            return System.Convert.ToBase64String(hash2);
        }
    }



}

