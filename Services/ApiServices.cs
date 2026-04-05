using MongoDB.Driver;
using WeoponX.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using  Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WeoponX.DTO;
namespace WeoponX.Services;

public class ApiServices : IApiServices 
{
    private readonly MongoDbContext _db;

    private readonly IConfiguration _configuration;

    public ApiServices(MongoDbContext db, IConfiguration configuration)
    {
        _db = db;
        _configuration = configuration;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _db.Users
            .Find(_ => true)
            .Project<User>(Builders<User>.Projection.Exclude("_id"))
            .ToListAsync();
    }

    public async Task SaveEmailOtpAsync(EmailOtp emailOtp)
    {
        await _db.EmailOtps.InsertOneAsync(emailOtp);
    }

    public async Task<EmailOtp> GetLatestEmailOtpAsync(string email)
    {
        var filter = Builders<EmailOtp>.Filter.Eq(e => e.Email, email);
        var sort = Builders<EmailOtp>.Sort.Descending(e => e.SentAtUtc);
        return await _db.EmailOtps.Find(filter).Sort(sort).FirstOrDefaultAsync();
    }

    public string GenerateJwtToken(string email, DateTime expiresAtUtc)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secret = _configuration["Jwt:Secret"] ?? throw new InvalidOperationException("JWT secret is not configured.");
        var key = Encoding.ASCII.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = expiresAtUtc,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
               SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public async Task<bool> UserExistsAsync(string email)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Email, email);
        return await _db.Users.Find(filter).AnyAsync();
    }

    public async Task SaveEmailLogAsync(EmailLog emailLog)
    {
        await _db.EmailLogs.InsertOneAsync(emailLog);
    }

     public async Task<UserDto> CreateUserFromDtoAsync(UserDto userDto)
    {
        // Check if user with the same email exists (generic helper)
        var filter = Builders<User>.Filter.Eq(u => u.Email, userDto.Email);
        bool exists = await MongoHelper.ExistsAsync(_db, filter);
        if (exists)
        {
            throw new InvalidOperationException("A user with this email already exists.");
        }

        // Convert DTO to Model
        var user = new User
        {
            Email = userDto.Email,
            Username = userDto.Username,
            Auth = userDto.Auth,
            Profile = userDto.Profile,
            Fitness = userDto.Fitness,
            Health = userDto.Health,
            Subscription = userDto.Subscription,
            Progress = userDto.Progress,
            Settings = userDto.Settings,
            Meta = userDto.Meta
        };
        await MongoHelper.InsertAsync(_db, user);

        var resultDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            Auth = user.Auth,
            Profile = user.Profile,
            Fitness = user.Fitness,
            Health = user.Health,
            Subscription = user.Subscription,
            Progress = user.Progress,
            Settings = user.Settings,
            Meta = user.Meta
        };
        return resultDto;
    }

}
