using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ApiMSCOFFIE.Models
{
    public class Productos
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Producto")]
        public string Producto { get; set; } = null!;


        [BsonElement("Descripcion")]
        public string Descripcion { get; set; } = null!;

        [BsonElement("Precio_venta")]
        public int PrecioVenta { get; set; }

        [BsonElement("Foto")]
        public string Foto { get; set; }

        [BsonElement("Categoria")]
        public string Categoria { get; set; } = null!;
    }
}
