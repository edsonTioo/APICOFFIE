﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ApiMSCOFFIE.Models
{
    public class Mesas
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("NumeroMesa")]
        public int NumeroMesa { get; set; }

        [BsonElement("Fecha")]
        public DateTime? Fecha {  get; set; }

        [BsonElement("Estado")]
        public string Estado { get; set; } = "Cargando";

        [BsonElement("Cliente")]
        public string Cliente { get; set; } = null!;

        [BsonElement("Reservacion")]
        public int Reservacion { get; set; }

        [BsonElement("Total")]
        public decimal Total { get; set; }

        [BsonElement("Pedidos")]
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();

    }
}