using Microsoft.AspNetCore.Mvc;
using WeoponX.Services;
using WeoponX.DTO;

namespace WeoponX.Controllers;
[ApiController]
[Route("users/")]

public class UsersController : ControllerBase
{
    private readonly IApiServices _apiService;
    public UsersController(IApiServices apiServices)
    {
        _apiService = apiServices;
    }

    [HttpGet]
    [Route("getAllUsers")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _apiService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPost]
    [Route("addUser")]
    public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
    {
        try
        {
            UserDto createdUser = await _apiService.CreateUserFromDtoAsync(userDto);
            return Ok(createdUser);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("already exists"))
        {
            return Conflict(ApiResponse<UserDto>.Fail(ex.Message, 409));
        }
        catch (Exception ex)
        {
            return BadRequest(ApiResponse<UserDto>.Fail(ex.Message));
        }
    }
}

// using Microsoft.AspNetCore.Mvc;
// using WeoponX.Services;

// namespace WeoponX.Controllers;

// [ApiController]
// [Route("users/")]
// public class UsersController : ControllerBase
// {
//     private readonly IApiServices _userService;
//     public UsersController(IApiServices userService)
//     {
//         _userService = userService;
//     }

//     [HttpGet]
//     [Route("getAllUsers")]
//     public async Task<IActionResult> GetAll()
//     {
//         var users = await _userService.GetAllUsersAsync();
//         return Ok(users);
//     }

// }