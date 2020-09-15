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
        [TestMethod]
        public void Login()
        {
            Configuration config = new Configuration();
            With.Mocks(
                delegate
                {
                    IWebRequestHelper mockedRequestHelper;
                    
                    using (Mocker.Current.Record())
                    {
                        mockedRequestHelper = Mocker.Current.StrictMock<IWebRequestHelper>();
                        
                        RhinoHelper.AddLoginExpectation(mockedRequestHelper, config);
                    }

                    MainProgram testThread = new MainProgram(config);

                    string success = testThread.LoginHttp(mockedRequestHelper);
                    Assert.That(success == "success");
                });
        }
    }
}
