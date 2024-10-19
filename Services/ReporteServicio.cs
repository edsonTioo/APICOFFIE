using ApiMSCOFFIE.Data;
using ApiMSCOFFIE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ApiMSCOFFIE.Services
{
    public class ReporteServicio
    { 
        private readonly IMongoCollection<ReporteMesas> _mesasCollection;
        private readonly IMongoCollection<Productos> _productosCollection;
        public ReporteServicio(IOptions<MSCOFFIEDBSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.CadenaConexion);
            var mongoDatabase = mongoClient.GetDatabase(settings.Value.NombreBaseDatos);
            _mesasCollection = mongoDatabase.GetCollection<ReporteMesas>(settings.Value.ColeccionReporte);
        }
        [Authorize]
        public async Task<List<ProductoVenta>> ObtenerProductosMasVendidosAsync(int topN)
        {
            if (_mesasCollection == null)
            {
                throw new InvalidOperationException("La colección de mesas no se ha inicializado.");
            }
            var mesas = await _mesasCollection.Find(Builders<ReporteMesas>.Filter.Empty).ToListAsync();
            var productosContados = new Dictionary<string, int>();
            foreach (var mesa in mesas)
            {
                foreach (var pedido in mesa.Pedidos)
                {
                    if (productosContados.ContainsKey(pedido.Producto))
                    {
                        productosContados[pedido.Producto] += pedido.Cantidad;
                    }
                    else
                    {
                        productosContados[pedido.Producto] = pedido.Cantidad;
                    }
                }
            }
            var productosMasVendidos = productosContados
                .OrderByDescending(p => p.Value)
                .Take(topN)
                .Select(p => new ProductoVenta
                {
                    Producto = p.Key, // Aquí asignamos solo el nombre del producto como string
                    TotalVendidos = p.Value
                })
                .ToList();

            return productosMasVendidos;
        }
        [Authorize]
        public async Task<List<ReporteMesas>> ObtenerGananciasPorMes()
        {
            var pipeline = new[]
            {
        new BsonDocument
        {
            { "$group", new BsonDocument
                {
                    { "_id", new BsonDocument
                        {
                            { "mes", new BsonDocument { { "$month", "$Fecha" } } },
                            { "anio", new BsonDocument { { "$year", "$Fecha" } } }
                        }
                    },
                    { "totalGanancias", new BsonDocument { { "$sum", "$Total" } } }
                }
            }
        },
        // Fase de ordenamiento
        new BsonDocument
        {
            { "$sort", new BsonDocument { { "_id.anio", 1 }, { "_id.mes", 1 } } }
        }
         };

            var result = await _mesasCollection.Aggregate<ReporteMesas>(pipeline).ToListAsync();
            return result;
        }
        [Authorize]
        public async Task<List<ReporteMesas>> ObtenerReporteMensualAsync(int year, int month)
        {
            var inicioMes = new DateTime(year, month, 1, 0, 0, 0, DateTimeKind.Utc);
            var finMes = inicioMes.AddMonths(1).AddSeconds(-1);

            Console.WriteLine($"Filtrando desde {inicioMes} hasta {finMes} en la colección Mesas");

            var filtro = Builders<ReporteMesas>.Filter.And(
                Builders<ReporteMesas>.Filter.Gte(m => m.Fecha, inicioMes),
                Builders<ReporteMesas>.Filter.Lte(m => m.Fecha, finMes)
            );

            var resultado = await _mesasCollection.Find(filtro).ToListAsync();

            Console.WriteLine($"Número de registros encontrados: {resultado.Count}");
            return resultado;
        }
        [Authorize]
        public async Task<List<ReporteMesas>> ObtenerReporteAnualAsync(int year)
        {
            var inicioAnio = new DateTime(year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var finAnio = new DateTime(year, 12, 31, 23, 59, 59, DateTimeKind.Utc);

            Console.WriteLine($"Filtrando desde {inicioAnio} hasta {finAnio} en la colección Mesas");

            var filtro = Builders<ReporteMesas>.Filter.And(
                Builders<ReporteMesas>.Filter.Gte(m => m.Fecha, inicioAnio),
                Builders<ReporteMesas>.Filter.Lte(m => m.Fecha, finAnio)
            );

            var resultado = await _mesasCollection.Find(filtro).ToListAsync();

            Console.WriteLine($"Número de registros encontrados: {resultado.Count}");
            return resultado;
        }
        [Authorize]
        public async Task<List<ReporteMesas>> ObtenerReporteSemanalAsync(DateTime inicioSemana)
        {
            var finSemana = inicioSemana.AddDays(6).AddSeconds(86399); // Último segundo del séptimo día.

            Console.WriteLine($"Filtrando desde {inicioSemana} hasta {finSemana} en la colección Mesas");

            var filtro = Builders<ReporteMesas>.Filter.And(
                Builders<ReporteMesas>.Filter.Gte(m => m.Fecha, inicioSemana),
                Builders<ReporteMesas>.Filter.Lte(m => m.Fecha, finSemana)
            );

            var resultado = await _mesasCollection.Find(filtro).ToListAsync();

            Console.WriteLine($"Número de registros encontrados: {resultado.Count}");
            return resultado;
        }
        [Authorize]
        public async Task<List<ReporteMesas>> ObtenerReporteDiarioAsync(DateTime fecha)
        {
            var inicioDia = fecha.Date; // Inicio del día
            var finDia = inicioDia.AddDays(1).AddSeconds(-1); // Fin del día

            Console.WriteLine($"Filtrando desde {inicioDia} hasta {finDia} en la colección Mesas");

            var filtro = Builders<ReporteMesas>.Filter.And(
                Builders<ReporteMesas>.Filter.Gte(m => m.Fecha, inicioDia),
                Builders<ReporteMesas>.Filter.Lte(m => m.Fecha, finDia)
            );

            var resultado = await _mesasCollection.Find(filtro).ToListAsync();

            Console.WriteLine($"Número de registros encontrados: {resultado.Count}");
            return resultado;
        }

       
    }
}

