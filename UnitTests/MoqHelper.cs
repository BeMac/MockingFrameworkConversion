using System.Net;
using MockingFrameworkConversion;
using Moq;

namespace UnitTests
{
    class MoqHelper
    {
        public static IWebRequestHelper GetMoqedHttpCall(Mock<IWebRequestHelper> mockWrapper, HttpWebRequest httpRequest, string request, string response)
        {
            mockWrapper.Setup(x => x.GetResponse(httpRequest, request)).Returns(response);
            return mockWrapper.Object;
        }

        public static void AddDisposeExpectation(Mock<IWebRequestHelper> mockWrapper)
        {
            mockWrapper.Verify(x => x.Dispose(), Times.Once);
        }
    }
}
