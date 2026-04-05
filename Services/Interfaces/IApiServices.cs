using WeoponX.Models;
using WeoponX.DTO;
namespace WeoponX.Services;
public interface IApiServices
{
    Task<List<User>> GetAllUsersAsync();
    Task SaveEmailOtpAsync(EmailOtp emailOtp);
    Task<EmailOtp> GetLatestEmailOtpAsync(string email);
    string GenerateJwtToken(string email, DateTime expiresAtUtc);
    Task<bool> UserExistsAsync(string email);
    Task SaveEmailLogAsync(EmailLog emailLog);
    Task<UserDto> CreateUserFromDtoAsync(UserDto userDto);
}
