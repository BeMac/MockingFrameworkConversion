using System;
using System.Net;
using MockingFrameworkConversion;
using Moq;

namespace UnitTests
{
    class MoqHelper
    {
        public static IWebRequestHelper GetMockedHttpCall(Mock<IWebRequestHelper> mockWrapper, HttpWebRequest httpRequest, string request, string response)
        {
            mockWrapper.Setup(x => x.GetResponse(httpRequest, request)).Returns(response);
            return mockWrapper.Object;
        }

        public static void AddDisposeExpectation(Mock<IWebRequestHelper> mockWrapper)
        {
            mockWrapper.Verify(x => x.Dispose(), Times.Once);
        }

        public static void AddMultipleLoginExpectation(Mock<IWebRequestHelper> mockWrapper, int count)
        {
            mockWrapper.Verify(x => x.Login(It.IsAny<Configuration>()), Times.Exactly(count));
        }

        public static void AddRequestExpectation(Mock<IWebRequestHelper> mockWrapper, HttpWebRequest httpRequest, string request, string response)
        {
            mockWrapper.Setup(x => x.GetResponse(httpRequest, request)).Returns(response);
        }

        public static IWebServiceHelper GetMockedServiceCall(Mock<IWebServiceHelper> mockWrapper, string response)
        {
            mockWrapper.Setup(x => x.MakeWebServiceRequest()).Returns(response);
            return mockWrapper.Object;
        }

        public static void AddSingleLoginExpectation(Mock<IWebRequestHelper> mockWrapper)
        {
            mockWrapper.Verify(x => x.Login(It.IsAny<Configuration>()), Times.Once);
        }

        public static void AddLoginExceptionExpectation(Mock<IWebRequestHelper> mockWrapper, Exception exception)
        {
            mockWrapper.SetupSequence(x => x.Login(It.IsAny<Configuration>())).Throws(exception);
        }

        public static void AddTestExpectation(Mock<IWebRequestHelper> mockWrapper)
        {
            mockWrapper.Verify(x => x.Test(), Times.Once);
        }

        public static void AddSearchExpectation(Mock<IWebRequestHelper> mockWrapper)
        {
            mockWrapper.Verify(x => x.Search(null, It.IsAny<Configuration>()), Times.Once);
        }
    }
}
