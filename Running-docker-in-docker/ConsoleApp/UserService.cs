using ConsoleApp.DataAccess;
using ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return null;
        }

        return await _context.Users.FirstOrDefaultAsync(x =>
            x.Name.Equals("GOR", StringComparison.InvariantCultureIgnoreCase));
    }
}