using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace WebAppTests.Setup;

public class ApiHost : WebApplicationFactory<Program>
{
    private readonly string _dbConnectionString;

    public ApiHost(string dbConnectionString)
    {
        _dbConnectionString = dbConnectionString;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, conf) =>
        {
            conf.AddInMemoryCollection(new Dictionary<string, string>
            {
                ["dbConnectionString"] = _dbConnectionString
            }!);
        });
        
        return base.CreateHost(builder);
    }
}