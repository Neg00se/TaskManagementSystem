using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using ServiceLayer.Validation;

namespace TaskManagementSystem.Controllers;
[Route("/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpPost("register")]
    public async Task<ActionResult> Create(UserRegisterModel model)
    {
        try
        {
            await _userService.RegisterAsync(model);
            return Ok();
        }
        catch (EmailOrUsernameAlreadyExistException ex)
        {

            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult> Get(UserLoginModel model)
    {
        try
        {
            var token = await _userService.LoginAsync(model);
            return Ok(token);
        }
        catch (IncorrectPasswordException ex)
        {

            return BadRequest(ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }
}
