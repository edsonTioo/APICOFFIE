using ApiMSCOFFIE.Data;
using ApiMSCOFFIE.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiMSCOFFIE.Services
{
    public class MesasServicio
    {
        private readonly IMongoCollection<Mesas> _coleccionMesas;

        public MesasServicio(IOptions<MSCOFFIEDBSettings> configuracionBD)
        {
            var clienteMongo = new MongoClient(configuracionBD.Value.CadenaConexion);
            var baseDatos = clienteMongo.GetDatabase(configuracionBD.Value.NombreBaseDatos);
            _coleccionMesas = baseDatos.GetCollection<Mesas>(configuracionBD.Value.ColeccionMesas);
        }

        // Obtener todas las mesas
        public async Task<List<Mesas>> ObtenerAsync() => await _coleccionMesas.Find(_ => true).ToListAsync();

        // Obtener mesa por id
        public async Task<Mesas?> ObtenerAsync(string id) => await _coleccionMesas.Find(x => x.Id == id).FirstOrDefaultAsync();

        // Crear nueva mesa
        public async Task CrearAsync(Mesas nuevaMesa) => await _coleccionMesas.InsertOneAsync(nuevaMesa);

        // Actualizar mesa
        public async Task ActualizarAsync(string id, Mesas mesaActualizada) =>
            await _coleccionMesas.ReplaceOneAsync(x => x.Id == id, mesaActualizada);

        // Eliminar mesa
        public async Task EliminarAsync(string id) =>
            await _coleccionMesas.DeleteOneAsync(x => x.Id == id);

        // Agregar pedido a una mesa
        public async Task AgregarPedidoAsync(string idMesa, Pedido nuevoPedido)
        {
            var mesa = await ObtenerAsync(idMesa);
            if (mesa == null) return;

            mesa.Pedidos.Add(nuevoPedido);
            mesa.Total += nuevoPedido.Subtotal; // Sumar el subtotal al total de la mesa

            await ActualizarAsync(idMesa, mesa);
        }

        // Actualizar pedido en una mesa
        public async Task ActualizarPedidoAsync(string idMesa, int indicePedido, Pedido pedidoActualizado)
        {
            var mesa = await ObtenerAsync(idMesa);
            if (mesa == null || indicePedido < 0 || indicePedido >= mesa.Pedidos.Count) return;

            var pedidoActual = mesa.Pedidos[indicePedido];
            mesa.Total -= pedidoActual.Subtotal; // Restar el subtotal anterior del total

            mesa.Pedidos[indicePedido] = pedidoActualizado;
            mesa.Total += pedidoActualizado.Subtotal; // Sumar el nuevo subtotal al total

            await ActualizarAsync(idMesa, mesa);
        }

        // Eliminar pedido de una mesa
        public async Task EliminarPedidoAsync(string idMesa, int indicePedido)
        {
            var mesa = await ObtenerAsync(idMesa);
            if (mesa == null || indicePedido < 0 || indicePedido >= mesa.Pedidos.Count) return;

            var pedido = mesa.Pedidos[indicePedido];
            mesa.Total -= pedido.Subtotal; // Restar el subtotal del pedido eliminado

            mesa.Pedidos.RemoveAt(indicePedido);
            await ActualizarAsync(idMesa, mesa);
        }
    }
}