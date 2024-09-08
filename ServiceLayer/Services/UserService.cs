using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Models;
using ServiceLayer.Validation;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Task = System.Threading.Tasks.Task;


namespace ServiceLayer.Services;

public class UserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }


    public async Task RegisterAsync(UserRegisterModel creds)
    {

        PasswordValidation.ValidateRegistrationPassword(creds.Password);

        await IsRegistrationUserExist(creds.Username, creds.Email);


        string passwordHash = BCrypt.Net.BCrypt.HashPassword(creds.Password);
        User user = new User();
        user.Username = creds.Username;
        user.PasswordHash = passwordHash;
        user.Email = creds.Email;

        await _unitOfWork.UserRepository.CreateUserAsync(user);
        await _unitOfWork.SaveAsync();
    }


    public async Task<string> LoginAsync(UserLoginModel creds)
    {
        var user = await _unitOfWork.UserRepository.GetUserAsync(creds.Username);

        if (user is not null)
        {
            bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(creds.Password, user.PasswordHash);

            if (!isCorrectPassword)
            {
                throw new IncorrectPasswordException("wrong password");
            }

            string token = CreateToken(user);

            return token;
        }
        else
        {
            throw new UserNotFoundException("user is not exist");
        }
    }

    private async Task IsRegistrationUserExist(string username, string email)
    {
        var user = await _unitOfWork.UserRepository.GetUserAsync(username);

        if (user is not null)
        {
            throw new EmailOrUsernameAlreadyExistException("User with this username already exist");
        }

        user = await _unitOfWork.UserRepository.GetUserAsync(email);

        if (user is not null)
        {
            throw new EmailOrUsernameAlreadyExistException("user with this email already exist");
        }
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name,user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value!));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred);


        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
