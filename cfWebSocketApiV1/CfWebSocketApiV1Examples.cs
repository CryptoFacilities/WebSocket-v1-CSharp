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
using System.Collections.Generic;

namespace WebSocket_v1_CSharp.cfWebSocketApiV1
{
    class CfWebSocketApiV1Examples
    {
        public static void Main()
        {
            string url = "wss://www.cryptofacilities.com/ws/v1";
            string apiKey = "..."; //accessible on your Account page under Settings->API Keys
            string apiSecret = "..."; //accessible on your Account page under Settings -> API Keys
            var cfWs = new CfWebSocketApi(url, apiKey, apiSecret);

            //Subscribe
            PublicSubscribeExample(cfWs);
            PrivateSubscribeExample(cfWs);

            // Wait for 5 Sec
            System.Threading.Thread.Sleep(5000);

            //Unsubscribe
            PublicUnsubscribeExample(cfWs);
            PrivateUnsubscribeExample(cfWs);

            Console.ReadKey(true);
        }

        private static void PublicSubscribeExample(CfWebSocketApi cfWs)
        {
            ////////// public feeds //////////

            var productIds = new List<string> { "PI_XBTUSD" };
            string feed;

            //// subscribe to trade
            feed = "trade";
            cfWs.SubscribePublic(feed, productIds);

            // subscribe to book
            feed = "book";
            cfWs.SubscribePublic(feed, productIds);

            // subscribe to ticker
            feed = "ticker";
            cfWs.SubscribePublic(feed, productIds);

            // subscribe to ticker lite
            feed = "ticker_lite";
            cfWs.SubscribePublic(feed, productIds);

            // subscribe to heartbeat
            feed = "heartbeat";
            cfWs.SubscribePublic(feed);
        }

        private static void PublicUnsubscribeExample(CfWebSocketApi cfWs)
        {
            ////////// public feeds //////////

            var productIds = new List<string> { "PI_XBTUSD" };
            string feed;

            //// subscribe to trade
            feed = "trade";
            cfWs.UnsubscribePublic(feed, productIds);

            // subscribe to book
            feed = "book";
            cfWs.UnsubscribePublic(feed, productIds);

            // subscribe to ticker
            feed = "ticker";
            cfWs.UnsubscribePublic(feed, productIds);

            // subscribe to ticker lite
            feed = "ticker_lite";
            cfWs.UnsubscribePublic(feed, productIds);

            // subscribe to heartbeat
            feed = "heartbeat";
            cfWs.UnsubscribePublic(feed);
        }

        private static void PrivateSubscribeExample(CfWebSocketApi cfWs)
        {
            ////////// private feeds //////////
            string feed;
            // subscribe to account balances and margis
            feed = "account_balances_and_margins";
            cfWs.SubscribePrivate(feed);
            //
            // subscribe to account log
            feed = "account_log";
            cfWs.SubscribePrivate(feed);
            //
            // subscribe to deposits withdrawals
            feed = "deposits_withdrawals";
            cfWs.SubscribePrivate(feed);
            //
            // subscribe to fills
            feed = "fills";
            cfWs.SubscribePrivate(feed);

            // subscribe to open positions
            feed = "open_positions";
            cfWs.SubscribePrivate(feed);

            // subscribe to open orders
            feed = "open_orders";
            cfWs.SubscribePrivate(feed);
            //
            // subscribe to notifications
            feed = "notifications_auth";
            cfWs.SubscribePrivate(feed);
        }

        private static void PrivateUnsubscribeExample(CfWebSocketApi cfWs)
        {
            ////////// private feeds //////////
            string feed;
            // subscribe to account balances and margis
            feed = "account_balances_and_margins";
            cfWs.UnsubscribePrivate(feed);
            //
            // subscribe to account log
            feed = "account_log";
            cfWs.UnsubscribePrivate(feed);
            //
            // subscribe to deposits withdrawals
            feed = "deposits_withdrawals";
            cfWs.UnsubscribePrivate(feed);
            //
            // subscribe to fills
            feed = "fills";
            cfWs.UnsubscribePrivate(feed);

            // subscribe to open positions
            feed = "open_positions";
            cfWs.UnsubscribePrivate(feed);

            // subscribe to open orders
            feed = "open_orders";
            cfWs.UnsubscribePrivate(feed);
            //
            // subscribe to notifications
            feed = "notifications_auth";
            cfWs.UnsubscribePrivate(feed);
        }
    }
}
