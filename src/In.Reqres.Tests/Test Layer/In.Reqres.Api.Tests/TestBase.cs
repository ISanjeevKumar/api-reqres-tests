using NUnit.Framework;

namespace In.Reqres.Api.Tests
{
    public class TestBase
    {
        public string BaseAddress { get; set; }

        [SetUp]
        public void TestIntialization()
        {
            BaseAddress = TestContext.Parameters["BaseAddress"];

        }
    }
}
