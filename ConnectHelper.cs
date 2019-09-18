using KiteConnect;
using KiteConnectWeb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace KiteConnectWeb
{
    public class ConnectHelper : IConnectHelper
    {
        private readonly IConfigSettings _configSettings;
        private readonly IConnectLogger _logger;
        public static string Message;
        static Kite kite;
        static string _apiKey;
        static string _secretKey;        
        public ConnectHelper(IConfigSettings configSettings, IConnectLogger logger)
        {
            _configSettings = configSettings;
            _logger = logger;
        }
        public string GetLoginURL()
        {
            _apiKey = _configSettings.ApiKey;
            _secretKey = _configSettings.SecretKey;
            var loginUrl = _configSettings.LoginUrl;
            return String.Format("{0}?api_key={1}&v=3", loginUrl, _apiKey);
        }

        public User InitializeSession(string requestToken)
        {            
            kite = new Kite(_apiKey, Debug: true);

            // For handling 403 errors
            kite.SetSessionExpiryHook(() => { Message = _logger.WriteMessage("Need to login again"); });

            User user = kite.GenerateSession(requestToken, _secretKey);

            Message = _logger.WriteMessage(ConnectConstants.SESSION_INITIALIZED);

            kite.SetAccessToken(user.AccessToken);

            Message = _logger.WriteMessage(ConnectConstants.ACCESSTOKEN_SET);

            return user;
        }

        public Ticker InitializeTicker(string accessToken)
        {
            var ticker = new Ticker(_apiKey, accessToken);

            Message = _logger.WriteMessage(ConnectConstants.TICKER_INITIALIZED);

            ticker.OnTick += OnTick;
            ticker.OnConnect += OnConnect;
            ticker.OnReconnect += OnReconnect;
            ticker.OnNoReconnect += OnNoReconnect;
            ticker.OnError += OnError;
            ticker.OnClose += OnClose;
            ticker.OnOrderUpdate += OnOrderUpdate;

            ticker.EnableReconnect(Interval: 5, Retries: 50);
            ticker.Connect();

            // Subscribing to NIFTY50 and setting mode to LTP
            ticker.Subscribe(Tokens: new UInt32[] { 256265 });
            ticker.SetMode(Tokens: new UInt32[] { 256265 }, Mode: Constants.MODE_LTP);
            
            return ticker;
        }

        public PositionResponse GetPositions()
        {
            if(kite == null)
            {
                Message = _logger.WriteMessage(ConnectConstants.POSITIONS_NOLOGIN);
                return new PositionResponse();
            }
            PositionResponse positions = kite.GetPositions();
            if (positions.Net.Count > 0)
            {
                Message = _logger.WriteMessage(Utils.JsonSerialize(positions.Net[0]));
            }
            else
            {
                Message = _logger.WriteMessage(ConnectConstants.POSITIONS_NOTAVAILABLE);
            }            
            return positions;
        }

        public List<Holding> GetHoldings()
        {
            if (kite == null)
            {
                Message = _logger.WriteMessage(ConnectConstants.HOLDINGS_NOLOGIN);
                return new List<Holding>();
            }
            List<Holding> holdings = kite.GetHoldings();
            if (holdings != null && holdings.Count > 0)
            {
                Message = _logger.WriteMessage(Utils.JsonSerialize(holdings[0]));
            }
            else
            {
                Message = _logger.WriteMessage(ConnectConstants.HOLDINGS_NOTAVAILABLE);
            }
            return holdings;
        }

        public List<Instrument> GetInstruments()
        {
            if (kite == null)
            {
                Message = _logger.WriteMessage(ConnectConstants.INSTRUMENTS_NOLOGIN);
                return new List<Instrument>();
            }
            List<Instrument> instruments = kite.GetInstruments();
            if (instruments != null && instruments.Count > 0)
            {
                Message = _logger.WriteMessage(Utils.JsonSerialize(instruments[0]));
            }
            else
            {
                Message = _logger.WriteMessage(ConnectConstants.INSTRUMENTS_NOTAVAILABLE);
            }
            return instruments;
        }

        public Dictionary<string, Quote> GetQuotes(string[] instrumentId)
        {
            if (kite == null)
            {
                Message = _logger.WriteMessage(ConnectConstants.QUOTES_NOLOGIN);
                return new Dictionary<string, Quote>();
            }
            Dictionary<string, Quote> quotes = kite.GetQuote(instrumentId);
            if (quotes != null && quotes.Count > 0)
            {
                Message = _logger.WriteMessage(Utils.JsonSerialize(quotes));
            }
            else
            {
                Message = _logger.WriteMessage(ConnectConstants.QUOTES_NOTAVAILABLE);
            }
            return quotes;
        }

        public Dictionary<string, OHLC> GetOHLC(string[] instrumentId)
        {
            if (kite == null)
            {
                Message = _logger.WriteMessage(ConnectConstants.OHLC_NOLOGIN);
                return new Dictionary<string, OHLC>();
            }
            Dictionary<string, OHLC> ohlcs = kite.GetOHLC(instrumentId);
            if (ohlcs != null && ohlcs.Count > 0)
            {
                Message = _logger.WriteMessage(Utils.JsonSerialize(ohlcs));
            }
            else
            {
                Message = _logger.WriteMessage(ConnectConstants.OHLC_NOTAVAILABLE);
            }
            return ohlcs;
        }

        public Dictionary<string, LTP> GetLTP(string[] instrumentId)
        {
            if (kite == null)
            {
                Message = _logger.WriteMessage(ConnectConstants.LTP_NOLOGIN);
                return new Dictionary<string, LTP>();
            }
            Dictionary<string, LTP> ltps = kite.GetLTP(instrumentId);
            if (ltps != null && ltps.Count > 0)
            {
                Message = _logger.WriteMessage(Utils.JsonSerialize(ltps));
            }
            else
            {
                Message = _logger.WriteMessage(ConnectConstants.LTP_NOTAVAILABLE);
            }
            return ltps;
        }

        public Dictionary<string, TrigerRange> GetTriggerRange(string[] instrumentId,string transactionType)
        {
            if (kite == null)
            {
                Message = _logger.WriteMessage(ConnectConstants.TRIGGERRANGE_NOLOGIN);
                return new Dictionary<string, TrigerRange>();
            }
            Dictionary<string, TrigerRange> triggerRange = kite.GetTriggerRange(instrumentId, transactionType);
            if (triggerRange != null && triggerRange.Count > 0)
            {
                Message = _logger.WriteMessage(Utils.JsonSerialize(triggerRange));
            }
            else
            {
                Message = _logger.WriteMessage(ConnectConstants.TRIGGERRANGE_NOTAVAILABLE);
            }
            return triggerRange;
        }

        public List<Order> GetAllOrders()
        {
            if (kite == null)
            {
                Message = _logger.WriteMessage(ConnectConstants.ORDERS_NOLOGIN);
                return new List<Order>();
            }
            List<Order> orders = kite.GetOrders();
            if (orders != null && orders.Count > 0)
            {
                Message = _logger.WriteMessage(Utils.JsonSerialize(orders[0]));
            }
            else
            {
                Message = _logger.WriteMessage(ConnectConstants.ORDERS_NOTAVAILABLE);
            }
            return orders;
        }

        public List<Order> GetOrderById(string orderId)
        {
            if (kite == null)
            {
                Message = _logger.WriteMessage(ConnectConstants.ORDER_NOLOGIN);
                return new List<Order>();
            }
            List<Order> orderinfo = kite.GetOrderHistory(orderId);
            if (orderinfo != null && orderinfo.Count > 0)
            {
                Message = _logger.WriteMessage(Utils.JsonSerialize(orderinfo[0]));
            }
            else
            {
                Message = _logger.WriteMessage(ConnectConstants.ORDER_NOTAVAILABLE);
            }
            return orderinfo;
        }

        private void OnTick(Tick TickData)
        {
            Message = _logger.WriteMessage(ConnectConstants.TICKER_DATA + ":" + Utils.JsonSerialize(TickData));
        }        
        private void OnConnect()
        {
            Message = _logger.WriteMessage(ConnectConstants.TICKER_CONNECTED);
        }
        private void OnReconnect()
        {
            Message = _logger.WriteMessage(ConnectConstants.TICKER_RECONNECTING);
        }
        private void OnNoReconnect()
        {
            Message = _logger.WriteMessage(ConnectConstants.TICKER_NOTRECONNECTING);
        }
        private void OnError(string message)
        {
            Message = _logger.WriteMessage(ConnectConstants.TICKER_ERROR + ":" + message);
        }
        private void OnClose()
        {
            Message = _logger.WriteMessage(ConnectConstants.TICKER_CLOSED);
        }
        private void OnOrderUpdate(Order OrderData)
        {
            Message = _logger.WriteMessage(ConnectConstants.TICKER_ORDERUPDATE + ":" + Utils.JsonSerialize(OrderData));
        }
    }
}