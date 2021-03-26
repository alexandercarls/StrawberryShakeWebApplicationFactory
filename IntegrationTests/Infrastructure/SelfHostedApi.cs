using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using StrawberryShakeWebApplicationFactory;

namespace IntegrationTests.Infrastructure
{
    public class SelfHostedApi : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services =>
            {
                services.AddConferenceClient()
                    .ConfigureHttpClient(c => c.BaseAddress = new("/graphql", UriKind.RelativeOrAbsolute));
            });

        }
        
    }
}