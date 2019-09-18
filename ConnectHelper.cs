using KiteConnect;
using KiteConnectWeb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiteConnectWeb
{
    public class ConnectHelper : IConnectHelper
    {
        private readonly IConfigSettings _configSettings;       
        static Kite kite;
        static string _apiKey;
        static string _secretKey;
        public ConnectHelper(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
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

            User user = kite.GenerateSession(requestToken, _secretKey);

            return user;
        }

        public Ticker InitializeTicker(string accessToken)
        {
            var ticker = new Ticker(_apiKey, accessToken);

            //ticker.OnTick += OnTick;
            //ticker.OnReconnect += OnReconnect;
            //ticker.OnNoReconnect += OnNoReconnect;
            //ticker.OnError += OnError;
            //ticker.OnClose += OnClose;
            //ticker.OnConnect += OnConnect;
            //ticker.OnOrderUpdate += OnOrderUpdate;

            ticker.EnableReconnect(Interval: 5, Retries: 50);
            ticker.Connect();

            // Subscribing to NIFTY50 and setting mode to LTP
            ticker.Subscribe(Tokens: new UInt32[] { 256265 });
            ticker.SetMode(Tokens: new UInt32[] { 256265 }, Mode: Constants.MODE_LTP);

            return ticker;
        }
    }
}