using ApiMSCOFFIE.Data;
using ApiMSCOFFIE.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiMSCOFFIE.Services
{
    public class UserService
    {
        //private readonly IMongoCollection<User> _coleccionuser;

        //public UserService (IOptions<MSCOFFIEDBSettings> ConfiguracionDB)
        //{
        //    var clienteMongo = new MongoClient(ConfiguracionDB.Value.ColeccionEmpleados);
        //    var basedatos = clienteMongo.GetDatabase(ConfiguracionDB.Value.NombreBaseDatos);
        //    _coleccionuser = basedatos.GetCollection<User>(ConfiguracionDB.Value.ColeccionEmpleados);
        //}
        //public async Task<Empleados?> ObtenerUsuariosAsync(string correo) => await _coleccionuser.Find(u => u.Correo == correo).FirstOrDefaultAsync();
        //public async Task CrearUsuarioAsync(User nuevoempleado) => await _coleccionuser.InsertOneAsync(nuevoempleado);
    }
}
