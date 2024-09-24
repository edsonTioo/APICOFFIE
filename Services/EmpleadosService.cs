using ApiMSCOFFIE.Data;
using ApiMSCOFFIE.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiMSCOFFIE.Services
{
    public class EmpleadosService
    {
        private readonly IMongoCollection<Empleados> _coleccionEmpleados;

        //Conexion ala base de datosss
        public EmpleadosService(IOptions<MSCOFFIEDBSettings>configuracionBD)
        {
            var clienteMongo = new MongoClient(configuracionBD.Value.CadenaConexion);
            var baseDatos = clienteMongo.GetDatabase(configuracionBD.Value.NombreBaseDatos);
            _coleccionEmpleados = baseDatos.GetCollection<Empleados>(configuracionBD.Value.ColeccionEmpleados);
        }

        //GETT
        public async Task<List<Empleados>> ObtenerAsync() => await _coleccionEmpleados.Find(_ => true).ToListAsync();

        public async Task<Empleados?> ObtenerAsync(string id) => await _coleccionEmpleados.Find(x=> x.Id == id).FirstOrDefaultAsync();

        //crear empleados
        public async Task CrearAsync(Empleados nuevoempleado)
        {
            await _coleccionEmpleados.InsertOneAsync(nuevoempleado);
        }
        //Actualizar empleado
        public async Task ActualizarAsync(string id , Empleados empleadoActualizado)=> await _coleccionEmpleados.ReplaceOneAsync(x => x.Id== id, empleadoActualizado);
        //Eliminar
        public async Task EliminarAsync(string id)=> await _coleccionEmpleados.DeleteManyAsync(x  => x.Id== id);
        //Buscar empleado
        public async Task <List<Empleados>>BuscarPorNombre(string nombre)
        {
            return await _coleccionEmpleados.Find(e => e.Nombre.ToLower().Contains(nombre.ToLower())).ToListAsync();
        }
    }
}
