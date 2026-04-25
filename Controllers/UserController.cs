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
    public async Task<IActionResult>
 GetAll()
    {
        var users = await _apiService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPost]
    [Route("addUser")]
    public async Task<IActionResult>
 AddUser([FromBody] UserDto userDto)
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

    [HttpPatch]
    [Route("patchUser/{id}")]
    public async Task<IActionResult>
 PatchUser(string id, [FromBody] DTO.UserDto patchDto)
    {
        try
        {
            var updatedUser = await _apiService.PatchUserAsync(id, patchDto);
            return Ok(DTO.ApiResponse<DTO.UserDto>.Ok(updatedUser, "User updated successfully."));
        }
        catch (KeyNotFoundException ex)
        {
            // User not found scenario
            return NotFound(DTO.ApiResponse<DTO.UserDto>.Fail(ex.Message, 404));
        }
        catch (Exception ex)
        {
            // Catch any other unexpected exceptions and return a generic error
            // In a real application, you might want more specific error handling or logging here.
            return StatusCode(StatusCodes.Status500InternalServerError, DTO.ApiResponse<DTO.UserDto>.Fail("An unexpected error occurred while updating the user.", 500));
        }
    }
}
