using System;
using System.Net;
using Rhino.Mocks;
using MockingFrameworkConversion;
using Rhino.Mocks.Constraints;

namespace UnitTests
{
    class RhinoHelper
    {
        public static void AddServiceRequestExpectation(IWebServiceHelper webServiceHelper)
        {
            Expect.On(webServiceHelper).Call(webServiceHelper.MakeWebServiceRequest()).Return("This Was Mocked");
        }
        
        public static void AddHttpRequestExpectation(IWebRequestHelper webRequestHelper, HttpWebRequest httpRequest, string request, string response)
        {
            Expect.On(webRequestHelper).Call(webRequestHelper.GetResponse(httpRequest, request)).Return(response);
        }

        public static void AddTestExpectation(IWebRequestHelper webRequestHelper)
        {
            Expect.Call(() => webRequestHelper.Test()).IgnoreArguments();
        }

        public static void AddLoginExpectation(IWebRequestHelper webRequestHelper, Configuration config)
        {
            Expect.Call(() => webRequestHelper.Login(config))
                .IgnoreArguments()
                .Constraints(Is.TypeOf<Configuration>());
        }

        public static void AddLoginExceptionExpectation(IWebRequestHelper webRequestHelper, Exception exceptionToThrow)
        {
            Expect.Call(delegate { webRequestHelper.Login(null); })
                .IgnoreArguments()
                .Constraints(Is.TypeOf<Configuration>())
                .Throw(exceptionToThrow);
        }

        public static void AddSearchExpectation(IWebRequestHelper webRequestHelper, string licenseExpected, string rawResponse)
        {
            Expect.On(webRequestHelper)
                .Call(webRequestHelper.Search(null, null))
                .IgnoreArguments()
                .Constraints(Is.Equal(licenseExpected), Is.TypeOf<Configuration>())
                .Return(rawResponse);
        }

        public static void AddDisposeExpectation(IWebRequestHelper webRequestHelper)
        {
            Expect.Call(delegate { webRequestHelper.Dispose(); }) //delegate needed since it is a void method
                .IgnoreArguments().Constraints();
        }
    }
}
