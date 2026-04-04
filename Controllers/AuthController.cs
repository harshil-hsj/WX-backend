using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using WeoponX.Models;
using WeoponX.DTO;
namespace WeoponX.Controllers;

[ApiController]
[Route("auth/")]


public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly Services.IApiServices _apiServices;

    public AuthController(IConfiguration configuration, Services.IApiServices apiServices)
    {
        _configuration = configuration;
        _apiServices = apiServices;
    }

    [HttpPost("sendVerificationOtp")]
    public async Task<IActionResult> SendOtp([FromQuery] string email)
    {
        var otp = Random.Shared.Next(100000, 999999);

        var smtpHost = _configuration["Smtp:Host"];
        var smtpPort = int.Parse(_configuration["Smtp:Port"] ?? "587");
        var smtpUser = _configuration["Smtp:Username"];
        var smtpPass = _configuration["Smtp:Password"];
        var fromEmail = _configuration["Smtp:From"] ?? smtpUser;

        var message = new MailMessage();
        message.From = new MailAddress(fromEmail, "WeoponX");
        message.To.Add(email);
        message.Subject = "Welcome to WeoponX";
        message.Body = $"Your OTP is {otp}";
        message.IsBodyHtml = false;

        using (var smtp = new SmtpClient(smtpHost, smtpPort))
        {
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(smtpUser, smtpPass);
            await smtp.SendMailAsync(message);
        }

        // Prepare EmailOtp model for MongoDB (to be saved later)
        var emailOtp = new EmailOtp
        {
            Email = email,
            Otp = otp,
            SentAtUtc = DateTime.UtcNow
        };
        await _apiServices.SaveEmailOtpAsync(emailOtp);
        

        // Prepare response DTO
        var response = new EmailOtpResponseDto
        {
            Message = "OTP sent",
            Email = email
        };

        return Ok(response);
    }

    [HttpPost("verifyOtp")]
    public async Task<IActionResult> VerifyOtp([FromQuery] string email, [FromQuery] int otp)
    {
        // Check if user exists
        bool userExists = await _apiServices.UserExistsAsync(email);

        var latestOtp = await _apiServices.GetLatestEmailOtpAsync(email);

        if (latestOtp == null)
            return NotFound(new { message = "OTP expired. Try requesting a new one.", userExists });

        // Check OTP match
        if (latestOtp.Otp != otp)
            return StatusCode(403, new { message = "Invalid OTP.", userExists });

        var expiry = DateTime.UtcNow.AddDays(7); // token valid for 7 days
        var token = _apiServices.GenerateJwtToken(email, expiry);
        var response = new OtpVerificationResponseDto
        {
            Message = "OTP verified successfully.",
            AuthToken = token,
            ExpiresAtUtc = expiry,
            UserExists = userExists
        };
        return Ok(response);
    }
}