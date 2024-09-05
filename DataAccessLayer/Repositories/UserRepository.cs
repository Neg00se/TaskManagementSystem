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

    public async Task CreateUser(User user)
    {
        if (user is not null)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<User> GetUser(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        return user;
    }
}
