using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;

namespace SoapWebService02
{
    public class AuthenticateRequestHttpModule : IHttpModule
    {
        private HttpApplication mHttpApp;

        public void Dispose() { }

        public void Init(HttpApplication httpApp)
        {
            this.mHttpApp = httpApp;
            mHttpApp.AuthenticateRequest += new EventHandler(OnAuthentication);
        }

        void OnAuthentication(object sender, EventArgs a)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpRequest request = application.Context.Request;

            //WindowsIdentity identity = (WindowsIdentity)application.Context.User.Identity;

            StreamWriter sw = new StreamWriter(request.MapPath("TestSoapWebService.txt"), true);

            sw.WriteLine(application.Context.Request.HttpMethod.ToString());
            sw.WriteLine(application.Context.Request.Headers.Count.ToString());
            NameValueCollection coll;
            coll = application.Context.Request.Headers;
            string[] arr1 = coll.AllKeys;
            for (int loop1 = 0; loop1 < arr1.Length; loop1++)
{
                sw.WriteLine(arr1[loop1].ToString());

                string[] arr2 = coll.GetValues(arr1[loop1]);
                for (int loop2 = 0; loop2 < arr2.Length; loop2++)
{
                    sw.WriteLine(arr2[loop2].ToString());
                }
            }

            sw.Flush();
            sw.Close();
        }

    }
}