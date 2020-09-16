using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MockingFrameworkConversion
{
    public interface IWebServiceHelper : IDisposable
    {
        string MakeWebServiceRequest();
    }

    public class WebServiceHelper : IWebServiceHelper
    {
        private bool _disposed;

        public string MakeWebServiceRequest()
        {
            return "This Was Real";
        }

        #region IDisposable
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
        #endregion
    }
}
