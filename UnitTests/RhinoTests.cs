using System;
using System.Net;
using NUnit.Framework;
using Rhino.Mocks;
using MockingFrameworkConversion;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;

namespace UnitTests
{
    [TestClass]
    public class RhinoTests
    {
        Configuration _config = new Configuration();

        [TestMethod]
        public void Dispose()
        {
            With.Mocks(
                delegate
                {
                    IWebRequestHelper mockedWebRequestHelper;
                    using (Mocker.Current.Record())
                    {
                        mockedWebRequestHelper = Mocker.Current.CreateMock<IWebRequestHelper>();
                        //RhinoHelper.AddDisposeExpectation(mockedWebRequestHelper);
                    }

                    MainProgram testThread = new MainProgram(_config);
                    testThread.Dispose(mockedWebRequestHelper);
                });
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
        public void LoginException()
        {
            With.Mocks(
                delegate
                {
                    IWebRequestHelper mockedRequestHelper;
                    string success;
                    using (Mocker.Current.Record())
                    {
                        mockedRequestHelper = Mocker.Current.StrictMock<IWebRequestHelper>();
                        RhinoHelper.AddLoginExceptionExpectation(mockedRequestHelper, new ArgumentException("A generic exception"));
                    }

                    MainProgram testThread = new MainProgram(_config);
                    Assert.Throws<ArgumentException>(() => testThread.LoginHttp(mockedRequestHelper));
                });
        }

        [TestMethod]
        public void Search()
        {
            With.Mocks(
                delegate
                {
                    IWebRequestHelper mockedRequestHelper;
                    string success;
                    string licenseNumber = "1234";
                    string expectedResponse = "1234";
                    using (Mocker.Current.Record())
                    {
                        mockedRequestHelper = Mocker.Current.StrictMock<IWebRequestHelper>();
                        RhinoHelper.AddSearchExpectation(mockedRequestHelper, licenseNumber, expectedResponse);
                    }

                    MainProgram testThread = new MainProgram(_config);
                    success = testThread.Search(mockedRequestHelper, licenseNumber);
                    Assert.That(success == "success");
                });
        }

        [TestMethod]
        public void ServiceRequest()
        {
            With.Mocks(
                    delegate
                    {
                        IWebServiceHelper mockedServiceHelper;
                        using (Mocker.Current.Record())
                        {
                            mockedServiceHelper = Mocker.Current.StrictMock<IWebServiceHelper>();
                            RhinoHelper.AddServiceRequestExpectation(mockedServiceHelper);
                        }

                        MainProgram testThread = new MainProgram(_config);
                        var returnString = testThread.MakeWebServiceCall(mockedServiceHelper);
                        Assert.That(returnString == "This Was Mocked");
                    });
        }
    }
}
