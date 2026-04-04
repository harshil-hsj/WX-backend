namespace WeoponX.DTO
{
    public class OtpVerificationResponseDto
    {
        public string Message { get; set; }
        public string AuthToken { get; set; }
        public DateTime ExpiresAtUtc { get; set; }
        public bool UserExists { get; set; }
    }
}