using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ApiMSCOFFIE.Models
{
    public class Inventario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Nombre")]
        public string Nombreinv { get; set; } = null!;

        [BsonElement("Cantidad")]
        public string Cantidadinv { get; set; }=null!;

        [BsonElement("Descripcion")]
        public string Descripcion { get; set; } = null!;
    }
}
