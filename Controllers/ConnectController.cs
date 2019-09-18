using KiteConnect;
using KiteConnectWeb.Interfaces;
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
            string loginUrl = _helper.GetLoginURL();            
            var result = Redirect(loginUrl);

            return result;
        }
        
        public void Initialize()
        {
            var requestToken = Request.QueryString["request_token"];
            User user = _helper.InitializeSession(requestToken);
            _accessToken = user.AccessToken;
            _publicToken = user.PublicToken;

            _helper.InitializeTicker(_accessToken);
        }
    }
}
