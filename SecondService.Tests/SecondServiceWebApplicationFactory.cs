using DAL;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace SecondService.Tests;

internal class SecondServiceWebApplicationFactory: WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var cb = new ConfigurationBuilder();
        cb.AddInMemoryCollection(new []
        {
            new KeyValuePair<string, string?>("DB_CONNECTION_STRING", "Host=localhost;Port=5432;Database=usersdb_test;Username=postgres;Password=superPass")
        });
        
        builder.ConfigureServices(collection =>
        {
            collection.RemoveAll<Context>();
            collection.AddScoped<Context>(_ => new Context(cb.Build()));
        });
        return base.CreateHost(builder);
    }
}