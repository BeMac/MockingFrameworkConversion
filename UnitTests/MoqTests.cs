using System;
using System.Net;
using NUnit.Framework;
using Rhino.Mocks;
using MockingFrameworkConversion;
using Moq;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace UnitTests
{
    [TestClass]
    public class MoqTests
    {
        Configuration _config = new Configuration();

        [TestMethod]
        public void Dispose()
        {
            try
            {
                Mock<IWebRequestHelper> mockWrapper = new Mock<IWebRequestHelper>();
                MainProgram testThread = new MainProgram(_config);
                testThread.Dispose(mockWrapper.Object);

                MoqHelper.AddDisposeExpectation(mockWrapper);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [TestMethod]
        public void HttpRequest()
        {
            With.Mocks(
                delegate
                {
                    HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri("http://www.myserver.com"));
                    IWebRequestHelper mockedRequestHelper;
                    string request = "This is a request";
                    string response = "This was Mocked";
                    using (Mocker.Current.Record())
                    {
                        mockedRequestHelper = Mocker.Current.StrictMock<IWebRequestHelper>();
                        RhinoHelper.AddHttpRequestExpectation(mockedRequestHelper, httpRequest, request, response);
                    }

                    MainProgram testThread = new MainProgram(_config);
                    var returnString = testThread.MakeWebRequestCall(mockedRequestHelper, httpRequest, request);
                    Assert.That(returnString == "This was Mocked");
                });
        }

        [TestMethod]
        public void Login()
        {
            With.Mocks(
                delegate
                {
                    IWebRequestHelper mockedRequestHelper;
                    
                    using (Mocker.Current.Record())
                    {
                        mockedRequestHelper = Mocker.Current.StrictMock<IWebRequestHelper>();
                        
                        RhinoHelper.AddLoginExpectation(mockedRequestHelper, _config);
                    }

                    MainProgram testThread = new MainProgram(_config);

                    string success = testThread.LoginHttp(mockedRequestHelper);
                    Assert.That(success == "success");
                });
        }

        [TestMethod]
        public void LoginAndTest()
        {
            With.Mocks(
                delegate
                {
                    IWebRequestHelper mockedRequestHelper;
                    string success;
                    using (Mocker.Current.Record())
                    {
                        mockedRequestHelper = Mocker.Current.StrictMock<IWebRequestHelper>();
                        RhinoHelper.AddLoginExpectation(mockedRequestHelper, _config);
                        RhinoHelper.AddTestExpectation(mockedRequestHelper);
                    }

                    MainProgram testThread = new MainProgram(_config);
                    success = testThread.LoginAndTest(mockedRequestHelper);
                    Assert.That(success == "success");
                });
        }

                [TestMethod]
        public void Logins()
        {
            Mock<IWebRequestHelper> mockWrapper = new Mock<IWebRequestHelper>();
            MainProgram testThread = new MainProgram(_config);
            testThread.LoginHttp(mockWrapper.Object);
            testThread.LoginHttp(mockWrapper.Object);
            testThread.LoginHttp(mockWrapper.Object);

            MoqHelper.AddMultipleLoginExpectation(mockWrapper, 3);
        }

        [TestMethod]
        public void Search()
        {
            try
            {
                Mock<IWebRequestHelper> mockWrapper = new Mock<IWebRequestHelper>();
                MainProgram testThread = new MainProgram(_config);
                var returnString = testThread.Search(mockWrapper.Object, null);

                MoqHelper.AddSearchExpectation(mockWrapper);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [TestMethod]
        public void ServiceRequest()
        {
            Mock<IWebServiceHelper> moqWrapper = new Mock<IWebServiceHelper>();
            string response = "This Was Moq'ed";
            IWebServiceHelper mockedWebServiceHelper = MoqHelper.GetMockedServiceCall(moqWrapper, response);
            MainProgram testThread = new MainProgram(_config);
            var returnString = testThread.MakeWebServiceCall(mockedWebServiceHelper);
            Assert.That(returnString == "This Was Moq'ed");
        }

        [TestMethod]
        public void SimpleVoid()
        {
            Mock<IWebRequestHelper> mockWrapper = new Mock<IWebRequestHelper>();
            MainProgram testThread = new MainProgram(_config);
            testThread.Test(mockWrapper.Object);

            MoqHelper.AddTestExpectation(mockWrapper);
        }
    }
}
