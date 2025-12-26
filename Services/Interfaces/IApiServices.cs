using WeoponX.Models;
namespace WeoponX.Services;
public interface IApiServices
{
    Task<List<User>> GetAllUsersAsync();
}
