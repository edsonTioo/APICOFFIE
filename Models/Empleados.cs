using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiMSCOFFIE.Models
{
    public class Empleados
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Nombre")]
        public string Nombre { get; set; } = null!;

        [BsonElement("Telefono")]
        public int Telefono { get; set; }

        [BsonElement("Correo")]
        public string Correo { get; set; } = null!;

        [BsonElement("Estado")]
        public string Estado { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }= null!;

        [BsonElement("Pago")]
        public int Pago { get; set; }


        [BsonElement("Rol")]
        public string Rol { get; set; }

        [BsonElement("Cedula")]
        public string Cedula { get; set; } = null!;

        [BsonElement("FechaNacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [BsonElement("Direccion")]
        public string Direccion { get; set; } = null!;  
    }
}
