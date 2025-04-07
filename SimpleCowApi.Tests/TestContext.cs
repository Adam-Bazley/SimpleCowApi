using System.Net.Http;
using SimpleCowApi.Tests.TestSetup;

namespace SimpleCowApi.Tests
{
    public class TestContext
    {
        public HttpClient Client { get; }

        public TestContext()
        {
            var factory = new TestApplicationFactory();
            Client = factory.CreateClient();
        }
    }
}
