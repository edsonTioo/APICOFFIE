
using ApiMSCOFFIE.Data;
using ApiMSCOFFIE.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiMSCOFFIE.Services
{
    public class ProductosService
    {
        private readonly IMongoCollection<Productos> _coleccionProducto;

        public ProductosService(IOptions<MSCOFFIEDBSettings> ConfiguracionBD)
        {
            var clienteMongo = new MongoClient(ConfiguracionBD.Value.CadenaConexion);
            var baseDatos = clienteMongo.GetDatabase(ConfiguracionBD.Value.NombreBaseDatos);
            _coleccionProducto = baseDatos.GetCollection<Productos>(ConfiguracionBD.Value.ColeccionProductos);
        }
        public async Task <List<Productos>> ObtenerAsync() => await _coleccionProducto.Find(_ => true).ToListAsync();
        public async Task<Productos?> ObtenerAsync(string id) => await _coleccionProducto.Find(x => x.Id == id).FirstOrDefaultAsync();

        //crear empleados
        public async Task CrearAsync(Productos nuevoproducto)
        {
            await _coleccionProducto.InsertOneAsync(nuevoproducto);
        }
        //Actualizar empleado
        public async Task ActualizarAsync(string id, Productos productoActualizado) => await _coleccionProducto.ReplaceOneAsync(x => x.Id == id, productoActualizado);
        //Eliminar
        public async Task EliminarAsync(string id) => await _coleccionProducto.DeleteManyAsync(x => x.Id == id);
        //Buscar empleado
        public async Task<List<Productos>> BuscarPorNombre(string nombre)
        {
            return await    _coleccionProducto.Find(e => e.Producto.ToLower().Contains(nombre.ToLower())).ToListAsync();
        }
    }
}
