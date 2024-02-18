using DotNet.Testcontainers;
using Testcontainers.PostgreSql;
using Xunit;

namespace WebAppTests.Setup;

public class AppFixture : IAsyncLifetime
{
    private PostgreSqlContainer _postgresContainer;
    private ApiHost _apiHost;

    public async Task InitializeAsync()
    {
        ConsoleLogger.Instance.DebugLogLevelEnabled = true;

        _postgresContainer = new PostgreSqlBuilder()
            .WithDatabase("WebAppDatabase")
            .Build();

        await _postgresContainer.StartAsync().ConfigureAwait(false);
        
        _apiHost = new ApiHost(_postgresContainer.GetConnectionString());

    }

    public IServiceProvider GetApiServiceProvider() => _apiHost.Services;
    
    public HttpClient CreateApiClient()
    {
        return _apiHost.CreateClient();
    }

    public async Task DisposeAsync()
    {
        await _postgresContainer.DisposeAsync();
        await _apiHost.DisposeAsync();
    }
}