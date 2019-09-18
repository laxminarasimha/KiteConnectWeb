using KiteConnectWeb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace KiteConnectWeb
{
    public class ConnectLogger : IConnectLogger
    {
        public static StringBuilder Message;
        public ConnectLogger()
        {
            Message = new StringBuilder();
        }
        public string WriteMessage(string message)
        {
            Message.Append(message);
            Message.Append(Environment.NewLine);
            return Message.ToString();
        }
    }
}