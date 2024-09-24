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

        [BsonElement("Cedula")]
        public string Cedula { get; set; } = null!;

        [BsonElement("Direccion")]
        public string Direccion { get; set; } = null!;

        [BsonElement("Estado")]
        public int Estado { get; set; }

        [BsonElement("Telefono")]
        public int Telefono { get; set; }

        [BsonElement("Fecha")]
        public DateTime Fecha { get; set; }

        [BsonElement("HoraReservacion")]
        public String HoraReservacion { get; set; }

        [BsonElement("HoraFinanReservacion")]
        public string HoraFinanReservacion { get; set; }
    }
}
