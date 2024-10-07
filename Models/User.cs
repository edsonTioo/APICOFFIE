using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ApiMSCOFFIE.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Correo")]
        public string Correo { get; set; } = null!;
        [BsonElement("Password")]
        public string Password { get; set; } = null!;
    }
}
