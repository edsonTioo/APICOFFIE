using ApiMSCOFFIE.Models;
using ApiMSCOFFIE.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiMSCOFFIE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControllerEstadoMesa : ControllerBase
    {
        private readonly MesasServicio _serviceMesas;

        public ControllerEstadoMesa(MesasServicio serviceMesas)
        {
            _serviceMesas = serviceMesas;
        }
        [HttpGet("{idMesa}/pedidos")]
        public async Task<ActionResult<List<Pedido>>> ObtenerPedidosDeMesa(string idMesa)
        {
            var pedidos = await _serviceMesas.ObtenerPedidosAsync(idMesa);

            if (pedidos == null || pedidos.Count == 0)
            {
                return NotFound("No se encontraron pedidos para la mesa especificada.");
            }

            return Ok(pedidos);
        }
    }
}
