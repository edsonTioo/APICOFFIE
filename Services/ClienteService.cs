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
        public async Task<List<Cliente>> ObtenerAsync() => await _coleccionCliente.Find(_ => true).ToListAsync();
        public async Task<Cliente?> ObtenerAsync(string id) => await _coleccionCliente.Find(x => x.Id == id).FirstOrDefaultAsync();
        public async Task CrearAsync(Cliente nuevoCliente)
        {
            await _coleccionCliente.InsertOneAsync(nuevoCliente);
        }
        public async Task ActualizarAsync(string id, Cliente clienteActualizado) => await _coleccionCliente.ReplaceOneAsync(x => x.Id == id, clienteActualizado);
        public async Task EliminarAsync(string id) => await _coleccionCliente.DeleteOneAsync(x => x.Id == id);
        public async Task<List<Cliente>> BuscarCorreo(string nombreCliente)
        {
            return await _coleccionCliente.Find(c => c.Nombre.ToLower().Contains(nombreCliente.ToLower())).ToListAsync();
        }
        public async Task<Cliente?> ObtenerClienteAsync(string correo) => await _coleccionCliente.Find(u => u.Correo == correo).FirstOrDefaultAsync();
        public async Task<List<Cliente>> BuscarPorNombre(string nombre)
        {
            return await _coleccionCliente.Find(e => e.Nombre.ToLower().Contains(nombre.ToLower())).ToListAsync();
        }
        public async Task<bool> ActualizarPasswordPorCorreoAsync(string correo, string nuevaPassword)
        {
            var filtro = Builders<Cliente>.Filter.Eq(c => c.Correo, correo);
            var actualizacion = Builders<Cliente>.Update.Set(c => c.Password, nuevaPassword);
            var resultado = await _coleccionCliente.UpdateOneAsync(filtro, actualizacion);
            return resultado.ModifiedCount > 0;
        }
        public async Task<bool> ActualizarReservacionAsync(string id, DateTime fecha, string horaReservacion, string horaFinanReservacion)
        {
            // Filtro para encontrar el cliente por su ID
            var filtro = Builders<Cliente>.Filter.Eq(c => c.Id, id);

            // Actualización de los campos específicos
            var actualizacion = Builders<Cliente>.Update
                .Set(c => c.Fecha, fecha)
                .Set(c => c.HoraReservacion, horaReservacion)
                .Set(c => c.HoraFinanReservacion, horaFinanReservacion);
            var resultado = await _coleccionCliente.UpdateOneAsync(filtro, actualizacion);
            return resultado.ModifiedCount > 0;
        }


    }
}
