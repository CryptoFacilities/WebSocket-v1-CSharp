Crypto Facilities Websocket API v1
==================================

This is a sample web socket application for [Crypto Facilities Ltd](https://www.cryptofacilities.com/), to demonstrate
the WS API.


Requirements 
---------------

.NETFramework v4.5

Functionality Overview
----------------------

* This application subscribes to all available feeds


Application Sample Output
-------------------------

The following is some of what you can expect when running this application:

```
Connected to wss://www.cryptofacilities.com/ws/v1
Received => {"event":"info","version":1}
public subscribe: {"event":"subscribe","feed":"trade","product_ids":["PI_XBTUSD"]}
public subscribe: {"event":"subscribe","feed":"book","product_ids":["PI_XBTUSD"]}
public subscribe: {"event":"subscribe","feed":"ticker","product_ids":["PI_XBTUSD"]}
public subscribe: {"event":"subscribe","feed":"ticker_lite","product_ids":["PI_XBTUSD"]}
public subscribe: {"event":"subscribe","feed":"heartbeat"}
requesting challenge: {"event":"challenge","api_key":"..."}
Received => {"event":"subscribed","feed":"trade","product_ids":["PI_XBTUSD"]}
Received => {"event":"subscribed","feed":"book","product_ids":["PI_XBTUSD"]}
Received => {"event":"subscribed","feed":"ticker","product_ids":["PI_XBTUSD"]}
Received => {"event":"subscribed","feed":"ticker_lite","product_ids":["PI_XBTUSD"]}
Received => {"event":"subscribed","feed":"heartbeat"}
```