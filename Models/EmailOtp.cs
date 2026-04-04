using System;

namespace WeoponX.Models
{
    public class EmailOtp
    {
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        [MongoDB.Bson.Serialization.Attributes.BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Email { get; set; }
        public int Otp { get; set; }
        public DateTime SentAtUtc { get; set; }
    }
}