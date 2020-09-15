using Rhino.Mocks;
using MockingFrameworkConversion;
using Rhino.Mocks.Constraints;

namespace UnitTests
{
    class RhinoHelper
    {
        public static void AddLoginExpectation(IWebRequestHelper webRequestHelper, Configuration config)
        {
            Expect.Call(() => webRequestHelper.Login(config))
                .IgnoreArguments()
                .Constraints(Is.TypeOf<Configuration>());
        }
    }
}
