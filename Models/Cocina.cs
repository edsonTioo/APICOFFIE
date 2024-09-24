using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ApiMSCOFFIE.Models
{
    public class Cocina
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Numero_Mesa")]
        public int NumeroMesa { get; set; }

        [BsonElement("Estado")]
        public int Estado { get; set; }

        [BsonElement("Cliente")]
        public string Cliente { get; set; } = null!;

        [BsonElement("Pedidos")]
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
