using KiteConnectWeb.Interfaces;
using System;
using System.Configuration;

namespace KiteConnectWeb
{
    public class ConfigSettings : IConfigSettings
    {
        public string ApiKey { get { return ConfigurationManager.AppSettings["apikey"]; } }

        public string SecretKey { get { return ConfigurationManager.AppSettings["secret"]; } }

        public string LoginUrl { get { return ConfigurationManager.AppSettings["loginurl"]; } }
    }
}