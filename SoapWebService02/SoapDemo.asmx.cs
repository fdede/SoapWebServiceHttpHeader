using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Web.Services;

namespace SoapWebService02
{
    /// <summary>
    /// Summary description for SoapDemo
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SoapDemo : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld(string message)
        {
            HttpRequest request = HttpContext.Current.Request;
            return message;
        }

        [WebMethod]
        public string HelloWorldWithHeader(string message)
        {
            HttpApplication httpApp = HttpContext.Current.ApplicationInstance;
            AuthenticateRequestHttpModule requestHttpModule = new AuthenticateRequestHttpModule();
            requestHttpModule.Init(httpApp);

            


            return message;
        }

    }
}
