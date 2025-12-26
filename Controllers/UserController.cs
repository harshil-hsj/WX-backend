using Microsoft.AspNetCore.Mvc;
using WeoponX.Services;

namespace WeoponX.Controllers;

[ApiController]
[Route("users/")]
public class UsersController : ControllerBase
{
    private readonly IApiServices _userService;
    public UsersController(IApiServices userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Route("getAllUsers")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

}