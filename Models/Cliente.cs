using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ApiMSCOFFIE.Models
{
    public class Cliente
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Nombre")]
        public string Nombre { get; set; } = null!;

        [BsonElement("Correo")]
        public string Correo { get; set; } = null!;
        [BsonElement("Cedula")]
        public string Cedula { get; set; } = null!;
        [BsonElement("Password")]
        public string Password { get; set; } = null!;

        [BsonElement("Direccion")]
        public string Direccion { get; set; } = null!;

        [BsonElement("Estado")]
        public string Estado { get; set; } = "Activo";

        [BsonElement("Telefono")]
        public int Telefono { get; set; }

        [BsonElement("Fecha")]
        public DateTime? Fecha { get; set; } = null;

        [BsonElement("HoraReservacion")]
        public string? HoraReservacion { get; set; } = null;

        [BsonElement("HoraFinanReservacion")]
        public string? HoraFinanReservacion { get; set; } = null;
    }
}
