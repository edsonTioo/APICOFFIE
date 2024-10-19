using MongoDB.Bson.Serialization.Attributes;

namespace ApiMSCOFFIE.Models
{
    public class ReportePedido
    {
        [BsonElement("Producto")]
        public string Producto { get; set; }

        [BsonElement("PrecioVenta")]
        public decimal PrecioVenta { get; set; }

        [BsonElement("Cantidad")]
        public int Cantidad { get; set; }

        [BsonElement("Subtotal")]
        public decimal Subtotal { get; set; }
    }
}
