using DataAccessLayer.Entities;
using Task = System.Threading.Tasks.Task;

namespace DataAccessLayer.Interfaces;
public interface IUserRepository
{

    Task CreateUserAsync(User user);
    Task<User> GetUserAsync(string username);
}
