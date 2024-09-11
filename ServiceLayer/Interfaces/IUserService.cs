using ServiceLayer.Models;

namespace ServiceLayer.Interfaces;
public interface IUserService
{
    Task<string> LoginAsync(UserLoginModel creds);
    Task RegisterAsync(UserRegisterModel creds);
}