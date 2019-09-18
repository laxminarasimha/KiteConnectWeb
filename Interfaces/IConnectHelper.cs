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
        PositionResponse GetPositions();
        List<Holding> GetHoldings();
        List<Instrument> GetInstruments();
        Dictionary<string, Quote> GetQuotes(string[] instrumentId);
        Dictionary<string, OHLC> GetOHLC(string[] instrumentId);
        Dictionary<string, LTP> GetLTP(string[] instrumentId);
        Dictionary<string, TrigerRange> GetTriggerRange(string[] instrumentId, string transactionType);
        List<Order> GetAllOrders();
        List<Order> GetOrderById(string orderId);
    }
}
