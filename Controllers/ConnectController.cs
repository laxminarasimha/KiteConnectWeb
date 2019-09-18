using KiteConnect;
using KiteConnectWeb.Interfaces;
using KiteConnectWeb.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace KiteConnectWeb.Controllers
{
    public class ConnectController : Controller
    {
        private readonly IConnectHelper _helper;
        static string _accessToken;
        static string _publicToken;
        public ConnectController(IConnectHelper helper)
        {
            _helper = helper;
        }
        // GET: Connect
        public ActionResult Index()
        {
            ConnectModel model = new ConnectModel {
                Message = "Welcome to Kite Connect"
            };

            return View(model);
        }

        public ActionResult ClearMessages()
        {
            ConnectHelper.Message = string.Empty;
            ConnectModel model = new ConnectModel
            {
                Message = ConnectHelper.Message
            };

            return View("Index", model);
        }

        public ActionResult Login()
        {
            ConnectModel model = new ConnectModel();
            try
            {               
                string loginUrl = _helper.GetLoginURL();
                var result = Redirect(loginUrl);

                return result;
            }
            catch(Exception e)
            {
                model.Message = e.Message;
                return View("Index",model);
            }
        }

        public ViewResult Initialize()
        {
            ConnectModel model = new ConnectModel();
            var requestToken = Request.QueryString["request_token"];
            try
            {               
                User user = _helper.InitializeSession(requestToken);
                _accessToken = user.AccessToken;
                _publicToken = user.PublicToken;

                _helper.InitializeTicker(_accessToken);
                model = new ConnectModel { Message = ConnectHelper.Message.ToString() };
                return View("Index", model);
            }
            catch(Exception e)
            {
                model.Message = e.Message;
                return View("Index", model);
            }
        }

        public ViewResult Positions()
        {
            ConnectModel model = new ConnectModel();
            try
            {
                PositionResponse positions = _helper.GetPositions();            
                return View("Index", new ConnectModel { Message = ConnectHelper.Message.ToString() });
            }
            catch (Exception e)
            {
                model.Message = e.Message;
                return View("Index", model);
            }
        }

        public ViewResult Holdings()
        {
            ConnectModel model = new ConnectModel();
            try
            {
                List<Holding> holdings = _helper.GetHoldings();
                return View("Index", new ConnectModel { Message = ConnectHelper.Message.ToString() });
            }
            catch (Exception e)
            {
                model.Message = e.Message;
                return View("Index", model);
            }
        }

        public ViewResult Instruments()
        {
            ConnectModel model = new ConnectModel();
            try
            {
                List<Instrument> instruments = _helper.GetInstruments();
                return View("Index", new ConnectModel { Message = ConnectHelper.Message.ToString() });
            }
            catch (Exception e)
            {
                model.Message = e.Message;
                return View("Index", model);
            }
        }

        public ViewResult Quotes()
        {
            ConnectModel model = new ConnectModel();
            string[] instrumentId = new string[] { "NSE:INFY", "NSE:ASHOKLEY" };
            try
            {
                Dictionary<string, Quote> quotes = _helper.GetQuotes(instrumentId);
                return View("Index", new ConnectModel { Message = ConnectHelper.Message.ToString() });
            }
            catch (Exception e)
            {
                model.Message = e.Message;
                return View("Index", model);
            }
        }

        public ViewResult OHLC()
        {
            ConnectModel model = new ConnectModel();
            string[] instrumentId = new string[] { "NSE:INFY", "NSE:ASHOKLEY" };
            try
            {
                Dictionary<string, OHLC> quotes = _helper.GetOHLC(instrumentId);
                return View("Index", new ConnectModel { Message = ConnectHelper.Message.ToString() });
            }
            catch (Exception e)
            {
                model.Message = e.Message;
                return View("Index", model);
            }
        }

        public ViewResult LTP()
        {
            ConnectModel model = new ConnectModel();
            string[] instrumentId = new string[] { "NSE:INFY", "NSE:ASHOKLEY" };
            try
            {
                Dictionary<string, LTP> ltp = _helper.GetLTP(instrumentId);
                return View("Index", new ConnectModel { Message = ConnectHelper.Message.ToString() });
            }
            catch (Exception e)
            {
                model.Message = e.Message;
                return View("Index", model);
            }
        }

        public ViewResult TriggerRange()
        {
            ConnectModel model = new ConnectModel();
            string[] instrumentId = new string[] { "NSE:INFY", "NSE:ASHOKLEY" };
            string transactionType = Constants.TRANSACTION_TYPE_BUY;
            try
            {
                Dictionary<string, TrigerRange> trigerRange = _helper.GetTriggerRange(instrumentId, transactionType);
                return View("Index", new ConnectModel { Message = ConnectHelper.Message.ToString() });
            }
            catch (Exception e)
            {
                model.Message = e.Message;
                return View("Index", model);
            }
        }

        public ViewResult Orders()
        {
            ConnectModel model = new ConnectModel();
            try
            {
                List<Order> orders = _helper.GetAllOrders();
                return View("Index", new ConnectModel { Message = ConnectHelper.Message.ToString() });
            }
            catch (Exception e)
            {
                model.Message = e.Message;
                return View("Index", model);
            }
        }

        public ViewResult OrderById(string orderId)
        {
            ConnectModel model = new ConnectModel();
            try
            {
                List<Order> orderinfo = _helper.GetOrderById(orderId);
                return View("Index", new ConnectModel { Message = ConnectHelper.Message.ToString() });
            }
            catch (Exception e)
            {
                model.Message = e.Message;
                return View("Index", model);
            }
        }
    }
}
