using Xunit;

namespace IntegrationTests.Infrastructure
{
    public class IntegrationTest : IClassFixture<SelfHostedApi>
    {
        public SelfHostedApi Api { get; }

        public IntegrationTest(SelfHostedApi api)
        {
            Api = api;
        }
    }
}