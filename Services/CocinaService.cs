using ApiMSCOFFIE.Data;
using ApiMSCOFFIE.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiMSCOFFIE.Services
{
    public class CocinaService
    {
        private readonly IMongoCollection<Cocina> _coleccionCocina;

        public CocinaService(IOptions<MSCOFFIEDBSettings> configuracionBD)
        {
            var clienteMongo = new MongoClient(configuracionBD.Value.CadenaConexion);
            var baseDatos = clienteMongo.GetDatabase(configuracionBD.Value.NombreBaseDatos);
            _coleccionCocina = baseDatos.GetCollection<Cocina>(configuracionBD.Value.ColeccionCocina);
        }

        public async Task<List<Cocina>> ObtenerAsync() => await _coleccionCocina.Find(_ => true).ToListAsync();
        public async Task<Cocina?> ObtenerAsync(string id) => await _coleccionCocina.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CrearAsync(Cocina nuevoPedido) => await _coleccionCocina.InsertOneAsync(nuevoPedido);
        public async Task ActualizarAsync(string id, Cocina cocinaActualizada) => await _coleccionCocina.ReplaceOneAsync(x => x.Id == id, cocinaActualizada);
        public async Task EliminarAsync(string id) => await _coleccionCocina.DeleteManyAsync(x => x.Id == id);
    }
}
}
