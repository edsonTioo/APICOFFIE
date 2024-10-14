using ApiMSCOFFIE.Data;
using ApiMSCOFFIE.Models;
using ApiMSCOFFIE.Services;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ApiMSCOFFIE.Services
{
    public class InventarioService
    {
        private readonly IMongoCollection<Inventario> _collecioninventario;

        public InventarioService(IOptions<MSCOFFIEDBSettings> ConfiguracionBD)
        {
            var clienteMongo = new MongoClient(ConfiguracionBD.Value.CadenaConexion);
            var baseDatos = clienteMongo.GetDatabase(ConfiguracionBD.Value.NombreBaseDatos);
            _collecioninventario = baseDatos.GetCollection<Inventario>(ConfiguracionBD.Value.ColeccionInventario);
        }
        public async Task<List<Inventario>> ObtenerAsync() => await _collecioninventario.Find(_ => true).ToListAsync();
        public async Task<Inventario?> ObtenerAsync(string id) => await _collecioninventario.Find(x => x.Id == id).FirstOrDefaultAsync();

        //crear inventario
        public async Task CrearAsync(Inventario nuevoinventario)
        {
            // Asignar un ObjectId a cada producto si no tiene uno
            foreach (var inventario in nuevoinventario.Nombreinv)
            {
                nuevoinventario.Id = ObjectId.GenerateNewId().ToString();
            }

            await _collecioninventario.InsertOneAsync(nuevoinventario);
        }
        //Actualizar inventario
        public async Task ActualizarAsync(string id, Inventario inventarioactualizado) => await _collecioninventario.ReplaceOneAsync(x => x.Id == id, inventarioactualizado);
        //Eliminar
        public async Task EliminarAsync(string id) => await _collecioninventario.DeleteManyAsync(x => x.Id == id);
        //Buscar prodinventario
        public async Task<List<Inventario>> buscarpornombre(string nombre)
        {
            return await _collecioninventario.Find(i => i.Nombreinv.ToLower().Contains(nombre.ToLower())).ToListAsync();
        }
    }
}

