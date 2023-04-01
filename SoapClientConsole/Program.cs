using SoapClientConsole.SoapDemoService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;

namespace SoapClientConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string url = "https://localhost:44331/SoapDemo.asmx";
            string _action = "https://localhost:44331/SoapDemo.asmx?op=HelloWorld";

            FirstWay(url);
        }

        private static void StandartWay(string url)
        {
            using (SoapDemoSoapClient client = new SoapDemoSoapClient("SoapDemoSoap", url))
            {
                var result = client.HelloWorld("furkan");
            };
        }

        private static void FirstWay(string url)
        {
            List<AddressHeader> headers = new List<AddressHeader>();

            AddressHeader MyHttpHeader = AddressHeader.CreateAddressHeader("MyHttpHeader", url, "MyHttpHeaderValue");
            headers.Add(MyHttpHeader);

            EndpointAddress endpoint = new EndpointAddress(new Uri(url), headers.ToArray());
            
            using (SoapDemoSoapClient client = new SoapDemoSoapClient("SoapDemoSoap", endpoint))
            {
                var result = client.HelloWorld("furkan");
            };
        }

        private static void HttpWay(string url)
        {
            SoapDemoSoapClient client = new SoapDemoSoapClient("SoapDemoSoap", url);

            using (new OperationContextScope(client.InnerChannel))
            {
                HeaderObject headerObject = new HeaderObject()
                {
                    Name = "furkan",
                    Value = 1234,
                    VknObj = new VKN() { VKNValue = "1234567890" }
                };

                // // Add a SOAP Header (Header property in the envelope) to an outgoing request. 
                // MessageHeader aMessageHeader = MessageHeader
                //    .CreateHeader("MySOAPHeader", "http://tempuri.org", "MySOAPHeaderValue");
                // OperationContext.Current.OutgoingMessageHeaders.Add(aMessageHeader);

                // Add a HTTP Header to an outgoing request
                HttpRequestMessageProperty requestMessage = new HttpRequestMessageProperty();
                requestMessage.Headers["MyHttpHeader"] = "MyHttpHeaderValue";
                requestMessage.Headers["MyHttpHeader02"] = "MyHttpHeaderValue02";
                //requestMessage.Headers["MyHttpHeaderObj"] = headerObject;
                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name]
                   = requestMessage;

                var result = client.HelloWorld("furkan");
            }

        }

    }

    public class HeaderObject
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public VKN VknObj { get; set; }
    }

    public class VKN
    {
        public string VKNValue { get; set; }
    }

}
