using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiteConnectWeb.Interfaces
{
    public interface IConfigSettings
    {
        string ApiKey { get; }
        string SecretKey { get; }
        string LoginUrl { get; }
    }
}
