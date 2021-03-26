using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using IntegrationTests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace IntegrationTests
{
    public class HelloTests : IntegrationTest
    {
        public HelloTests(SelfHostedApi api) : base(api)
        {
        }

        [Fact]
        public async Task HttpClient()
        {
            var client = Api.CreateClient();

            var response = await client.SendAsync(new(HttpMethod.Post, "/graphql")
            {
                Content = new StringContent("{\"query\":\"query Hello {\\n  hello\\n}\",\"operationName\":\"Hello\"}")
                {
                    Headers =
                    {
                        ContentType = new("application/json")
                    }
                }
            });

            var body = await response.Content.ReadFromJsonAsync<JsonElement>();
            Assert.Equal("world", body.GetProperty("data").GetProperty("hello").GetString());
        }

        [Fact]
        public async Task StrawberryShake()
        {
            var client = Api.GetGraphQLClient();

            var response = await client.Hello.ExecuteAsync();

            Assert.Equal("world",response.Data?.Hello);
        }
    }
}