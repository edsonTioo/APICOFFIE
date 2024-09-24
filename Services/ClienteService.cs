using ApiMSCOFFIE.Data;
using ApiMSCOFFIE.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiMSCOFFIE.Services
{
    public class ClienteService
    {
        private readonly IMongoCollection<Cliente> _coleccionCliente;

        public ClienteService(IOptions<MSCOFFIEDBSettings> ConfiguracionBD)
        {
            var clienteMongo = new MongoClient(ConfiguracionBD.Value.CadenaConexion);
            var baseDatos = clienteMongo.GetDatabase(ConfiguracionBD.Value.NombreBaseDatos);
            _coleccionCliente = baseDatos.GetCollection<Cliente>(ConfiguracionBD.Value.ColeccionClientes);
        }

        // Obtener todos los clientes
        public async Task<List<Cliente>> ObtenerAsync() => await _coleccionCliente.Find(_ => true).ToListAsync();

        // Obtener un cliente por ID
        public async Task<Cliente?> ObtenerAsync(string id) => await _coleccionCliente.Find(x => x.Id == id).FirstOrDefaultAsync();

        // Crear nuevo cliente
        public async Task CrearAsync(Cliente nuevoCliente)
        {
            await _coleccionCliente.InsertOneAsync(nuevoCliente);
        }

        // Actualizar cliente existente
        public async Task ActualizarAsync(string id, Cliente clienteActualizado) => await _coleccionCliente.ReplaceOneAsync(x => x.Id == id, clienteActualizado);

        // Eliminar cliente
        public async Task EliminarAsync(string id) => await _coleccionCliente.DeleteOneAsync(x => x.Id == id);

        // Buscar clientes por nombre
        public async Task<List<Cliente>> BuscarPorNombre(string nombreCliente)
        {
            return await _coleccionCliente.Find(c => c.Nombre.ToLower().Contains(nombreCliente.ToLower())).ToListAsync();
        }
    }
}
