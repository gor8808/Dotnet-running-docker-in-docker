using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApp.DataAccess;
using WebApp.Models;
using WebAppTests.Setup;
using Xunit;

namespace WebAppTests;

[Collection(nameof(UsersTestCollection))]
public class UserTests : BaseTests
{
    public UserTests(AppFixture fixture) : base(fixture)
    { }
    
    [Fact]
    public async Task Can_Get_User_By_Name()
    {
        var context = Resolve<AppDbContext>();
        await context.Users.AddAsync(new User { Name = "Gor" });
        await context.SaveChangesAsync();
        
        var response = await CreateClient().GetAsync($"Users");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        Assert.NotEmpty(json);
        
        var user = JsonConvert.DeserializeObject<User[]>(json);
        Assert.NotNull(user);
        Assert.Single(user);
    }
    
    [Theory]
    [InlineData("GOR")]
    public async Task Can_Get_User_By_Name_Key_Insensitive(string name)
    {
        var context = Resolve<AppDbContext>();
        await context.Users.AddAsync(new User { Name = "Gor" });
        await context.SaveChangesAsync();
        
        var response = await CreateClient().GetAsync($"Users/{name}");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        Assert.NotEmpty(json);
        
        var user = JsonConvert.DeserializeObject<User>(json);
        Assert.NotNull(user);
    }
}