namespace WeoponX.Models
{
        public class SendOtpResponse
    {
        public bool Success { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime SentAtUtc { get; set; }
        public string? RequestId { get; set; }
    }
}