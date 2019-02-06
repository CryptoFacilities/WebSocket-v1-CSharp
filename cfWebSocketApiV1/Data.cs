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


using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebSocket_v1_CSharp.cfWebSocketApiV1
{
    public class PublicSubscription
    {
        [JsonProperty("event")]
        public string Event { get; set; }
        [JsonProperty("feed")]
        public string Feed { get; set; }
        [JsonProperty("product_ids")]
        public IList<string> ProductsIds { get; set; }

        public PublicSubscription(string @event, string feed, IList<string> productsIds = null)
        {
            Event = @event;
            Feed = feed;
            ProductsIds = productsIds;
        }
    }

    public class PrivateSubscription
    {
        [JsonProperty("event")]
        public string Event { get; set; }
        [JsonProperty("feed")]
        public string Feed { get; set; }
        [JsonProperty("api_key")]
        public string ApiKey { get; set; }
        [JsonProperty("original_challenge")]
        public string OriginalChallenge { get; set; }
        [JsonProperty("signed_challenge")]
        public string SignedChallenge { get; set; }

        public PrivateSubscription(string @event, string feed, string apiKey = null, string originalChallenge = null, string signedChallenge = null)
        {
            Event = @event;
            Feed = feed;
            ApiKey = apiKey;
            OriginalChallenge = originalChallenge;
            SignedChallenge = signedChallenge;
        }
    }

    public class ChallengeRequest
    {
        [JsonProperty("event")]
        public string Event { get; set; }
        [JsonProperty("api_key")]
        public string ApiKey { get; set; }

        public ChallengeRequest(string @event, string apiKey)
        {
            Event = @event;
            ApiKey = apiKey;
        }
    }
}
