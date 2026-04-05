using System;

namespace WeoponX.Models
{
    public class EmailLog
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
        public DateTime SentAtUtc { get; set; }
    }
}