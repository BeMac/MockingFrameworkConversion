using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MockingFrameworkConversion
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class MainProgram
    {
        private readonly Configuration _config;

        public MainProgram(Configuration config)
        {
            _config = config;
        }

        public string LoginHttp(IWebRequestHelper webRequestHelper)
        {
            try
            {
                webRequestHelper.Login(_config);
                return "success";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
