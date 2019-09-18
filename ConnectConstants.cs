using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiteConnectWeb
{
    public class ConnectConstants
    {
        public const string LOGIN_AGAIN = "Need to login again";
        public const string SESSION_INITIALIZED = "Session initialized";
        public const string TICKER_INITIALIZED = "Ticker initialized";
        public const string TICKER_DATA = "Tick";
        public const string TICKER_CONNECTED = "Ticker connected";
        public const string TICKER_RECONNECTING = "Ticker reconnecting";
        public const string TICKER_NOTRECONNECTING = "Ticker not reconnecting";
        public const string TICKER_ERROR = "Ticker error";
        public const string TICKER_CLOSED = "Ticker closed";
        public const string TICKER_ORDERUPDATE = "Ticker order update";
        public const string ACCESSTOKEN_SET = "Access token set";
        public const string POSITIONS_NOLOGIN = "Need to login to get the positions";
        public const string POSITIONS_NOTAVAILABLE = "No available net positions";
        public const string HOLDINGS_NOLOGIN = "Need to login to get the holdings";
        public const string HOLDINGS_NOTAVAILABLE = "No available holdings";
        public const string INSTRUMENTS_NOLOGIN = "Need to login to get the instruments";
        public const string INSTRUMENTS_NOTAVAILABLE = "No available instruments";
        public const string QUOTES_NOLOGIN = "Need to login to get the quotes";
        public const string QUOTES_NOTAVAILABLE = "No available quotes";
        public const string OHLC_NOLOGIN = "Need to login to get the OHLC";
        public const string OHLC_NOTAVAILABLE = "No available OHLC";
        public const string LTP_NOLOGIN = "Need to login to get the LTP";
        public const string LTP_NOTAVAILABLE = "No available LTP";
        public const string TRIGGERRANGE_NOLOGIN = "Need to login to get the trigger range";
        public const string TRIGGERRANGE_NOTAVAILABLE = "No available trigger range";
        public const string ORDERS_NOLOGIN = "Need to login to get the orders";
        public const string ORDERS_NOTAVAILABLE = "No available orders";
        public const string ORDER_NOLOGIN = "Need to login to get the order information";
        public const string ORDER_NOTAVAILABLE = "No available order";
    }
}