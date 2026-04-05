using WeoponX.Models;

namespace WeoponX.DTO
{
    public class UserDto
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public AuthInfo? Auth { get; set; }
        public ProfileInfo? Profile { get; set; }
        public FitnessInfo? Fitness { get; set; }
        public HealthInfo? Health { get; set; }
        public SubscriptionInfo? Subscription { get; set; }
        public ProgressInfo? Progress { get; set; }
        public SettingsInfo? Settings { get; set; }
        public MetaInfo? Meta { get; set; }
    }
}
