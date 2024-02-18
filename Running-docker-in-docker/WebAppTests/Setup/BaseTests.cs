using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp.DataAccess;
using Xunit;

namespace WebAppTests.Setup;

public abstract class BaseTests : IAsyncLifetime
{
    protected readonly IServiceScope _scope;
    protected readonly AppFixture _appFixture;
    protected readonly AppDbContext _dbContext;

    protected BaseTests(AppFixture appFixture)
    {
        _appFixture = appFixture;
        _scope = appFixture.GetApiServiceProvider().CreateScope();
        _dbContext = Resolve<AppDbContext>();
    }
    
    protected T Resolve<T>() where T : notnull
    {
        return _scope.ServiceProvider.GetRequiredService<T>();
    }

    protected HttpClient CreateClient()
    {
        return _appFixture.CreateApiClient();
    }
    
    public async Task InitializeAsync()
    {
        var context = Resolve<AppDbContext>();

        await context.Database.MigrateAsync();
    }

    public Task DisposeAsync()
    {
        _scope.Dispose();
        
        return Task.CompletedTask;
    }
}