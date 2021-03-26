using System;
using System.Net.Http;
using IntegrationTests.ConferenceClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using StrawberryShakeWebApplicationFactory;

namespace IntegrationTests.Infrastructure
{
    public class SelfHostedApi : WebApplicationFactory<Startup>
    {
        public IConferenceClient GetGraphQLClient()
        {
            var sc = new ServiceCollection();
            sc.AddSingleton<IHttpClientFactory>(new MockClientFactory(CreateClient));
            sc.AddConferenceClient();
            return sc.BuildServiceProvider()
                .GetRequiredService<IntegrationTests.ConferenceClient.ConferenceClient>(); 
        }

        class MockClientFactory: IHttpClientFactory
        {
            private readonly Func<HttpClient> _client;

            public MockClientFactory(Func<HttpClient> client)
            {
                _client = client;
            }
            
            public HttpClient CreateClient(String name)
            {
                var client = _client();
                client.BaseAddress = new Uri("http://localhost/graphql");
                return client;
            }
        }
    }
}