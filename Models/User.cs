using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace WeoponX.Models
{
    public enum Gender
    {
        [Description("Male")] Male,
        [Description("Female")] Female,
        [Description("Other")] Other
    }

    public enum HeightUnit
    {
        [Description("Centimeters")] Cm,
        [Description("Feet")] Ft
    }

    public enum WeightUnit
    {
        [Description("Kilograms")] Kg,
        [Description("Pounds")] Lbs
    }

    public enum FitnessGoal
    {
        [Description("Lose Weight")] LoseWeight,
        [Description("Gain Muscle")] GainMuscle,
        [Description("Maintain")] Maintain,
        [Description("General Fitness")] GeneralFitness
    }

    public enum ExperienceLevel
    {
        [Description("Beginner")] Beginner,
        [Description("Intermediate")] Intermediate,
        [Description("Advanced")] Advanced
    }

    public enum WorkoutPreference
    {
        [Description("Gym")] Gym,
        [Description("Home")] Home,
        [Description("Both")] Both
    }

    public enum ActivityLevel
    {
        [Description("Sedentary")] Sedentary,
        [Description("Lightly Active")] LightlyActive,
        [Description("Active")] Active,
        [Description("Very Active")] VeryActive
    }

    public enum DietPreference
    {
        [Description("Vegetarian")] Veg,
        [Description("Non-Vegetarian")] NonVeg,
        [Description("Vegan")] Vegan
    }

    public enum SubscriptionPlan
    {
        [Description("Free")] Free,
        [Description("Premium")] Premium
    }

    public enum Units
    {
        [Description("Metric")] Metric,
        [Description("Imperial")] Imperial
    }

    public class User
    {
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        [MongoDB.Bson.Serialization.Attributes.BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
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

    public class AuthInfo
    {
        public string? Provider { get; set; } // e.g., "local"
        public bool IsEmailVerified { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }

    public class ProfileInfo
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public HeightInfo? Height { get; set; }
        public WeightInfo? Weight { get; set; }
        public string? ProfileImage { get; set; }
    }

    public class HeightInfo
    {
        public int Value { get; set; }
        public HeightUnit Unit { get; set; }
    }

    public class WeightInfo
    {
        public int Value { get; set; }
        public WeightUnit Unit { get; set; }
    }

    public class FitnessInfo
    {
        public FitnessGoal Goal { get; set; }
        public ExperienceLevel ExperienceLevel { get; set; }
        public WorkoutPreference WorkoutPreference { get; set; }
        public int TargetWeight { get; set; }
        public WorkoutDaysPerWeekInfo? WorkoutDaysPerWeek { get; set; }
        public ActivityLevel ActivityLevel { get; set; }
    }

    public class WorkoutDaysPerWeekInfo
    {
        public int Value { get; set; }
        public List<int>? Options { get; set; }
    }

    public class HealthInfo
    {
        public List<string>? Injuries { get; set; }
        public List<string>? MedicalConditions { get; set; }
        public DietPreference DietPreference { get; set; }
        public List<string>? Allergies { get; set; }
    }

    public class SubscriptionInfo
    {
        public SubscriptionPlan Plan { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }

    public class ProgressInfo
    {
        public int StartingWeight { get; set; }
        public int CurrentWeight { get; set; }
        public int GoalWeight { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class SettingsInfo
    {
        public Units Units { get; set; }
        public bool NotificationsEnabled { get; set; }
    }

    public class MetaInfo
    {
        public bool IsOnboardingComplete { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
