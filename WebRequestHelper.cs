using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MockingFrameworkConversion
{
    public interface IWebRequestHelper : IDisposable
    {
        string GetResponse(HttpWebRequest oWebRequest, string szRequest);
        void Test();
        void Login(Configuration config);
    }

    public class WebRequestHelper : IWebRequestHelper
    {
        private bool _disposed;

        public string GetResponse(HttpWebRequest webRequest, string request)
        {
            string response = string.Empty;

            return response;
        }

        public void Test()
        {

        }

        public void Login(Configuration config)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Stop and dispose timer here.
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
