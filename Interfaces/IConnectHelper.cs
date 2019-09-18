using KiteConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiteConnectWeb.Interfaces
{
    public interface IConnectHelper
    {
        string GetLoginURL();
        User InitializeSession(string requestToken);
        Ticker InitializeTicker(string accessToken);
    }
}
