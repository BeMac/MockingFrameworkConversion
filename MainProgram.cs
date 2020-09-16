
using System;
using System.Net;

namespace MockingFrameworkConversion
{
    public class MainProgram
    {
        private readonly Configuration _config;

        public MainProgram(Configuration config)
        {
            _config = config;
        }

        static void Main(string[] args)
        {
        }

        public string LoginHttp(IWebRequestHelper webRequestHelper)
        {
            try
            {
                webRequestHelper.Login(_config);
                return "success";
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public string MakeWebRequestCall(IWebRequestHelper webServiceHelper, HttpWebRequest webRequest, string request)
        {
            string returnString = webServiceHelper.GetResponse(webRequest, request);

            return returnString;
        }

        public string MakeWebServiceCall(IWebServiceHelper webServiceHelper)
        {
            string returnString = webServiceHelper.MakeWebServiceRequest();

            return returnString;
        }

        public string LoginAndTest(IWebRequestHelper webRequestHelper)
        {
            try
            {
                webRequestHelper.Login(_config);
                webRequestHelper.Test();
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Search(IWebRequestHelper webRequestHelper, string licenseNumber)
        {
            try
            {
                webRequestHelper.Search(licenseNumber, _config);
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void Test(IWebRequestHelper webRequestHelper)
        {
            webRequestHelper.Test();
        }

        public void Dispose(IWebRequestHelper webRequestHelper)
        {
            webRequestHelper.Dispose();
        }
    }
}
