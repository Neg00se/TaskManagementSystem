using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace DataAccessLayer.Repositories;
public class UserRepository : IUserRepository
{
    private readonly TaskSystemDbContext _context;

    public UserRepository(TaskSystemDbContext context)
    {
        _context = context;
    }

    public async Task CreateUserAsync(User user)
    {
        if (user is not null)
        {
            await _context.Users.AddAsync(user);
        }
    }

    public async Task<User> GetUserAsync(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        return user;
    }
}
