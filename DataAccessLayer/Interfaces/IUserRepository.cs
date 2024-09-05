using DataAccessLayer.Entities;
using Task = System.Threading.Tasks.Task;

namespace DataAccessLayer.Interfaces;
public interface IUserRepository
{

    public Task CreateUser(User user);
    public Task<User> GetUser(string username);
}
