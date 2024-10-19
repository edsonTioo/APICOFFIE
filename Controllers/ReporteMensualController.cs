using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiMSCOFFIE.Data;  // Reemplaza con el nombre de tu contexto de datos
using ApiMSCOFFIE.Models;
using ApiMSCOFFIE.Services;
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.Authorization;

namespace ApiMSCOFFIE.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteMensualController : ControllerBase
    {
        private readonly ReporteServicio _reporteservicio;

        public ReporteMensualController(ReporteServicio reporteservicio)
        {
            _reporteservicio = reporteservicio;
        }
        [HttpGet("reporte-mensual/{year:int}/{month:int}")]
        public async Task<ActionResult<IEnumerable<ReporteMesas>>> ObtenerReporteMensual(int year, int month)
        {
            try
            {
                var mesas = await _reporteservicio.ObtenerReporteMensualAsync(year, month);
                return Ok(mesas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el reporte mensual: {ex.Message}");
            }
        }
        [HttpGet("reporte-anual/{year:int}")]
        public async Task<ActionResult<IEnumerable<ReporteMesas>>> ObtenerReporteAnual(int year)
        {
            try
            {
                var mesas = await _reporteservicio.ObtenerReporteAnualAsync(year);
                return Ok(mesas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el reporte anual: {ex.Message}");
            }
        }
        [HttpGet("reporte-semanal/{inicioSemana:datetime}")]
        public async Task<ActionResult<IEnumerable<ReporteMesas>>> ObtenerReporteSemanal(DateTime inicioSemana)
        {
            try
            {
                var mesas = await _reporteservicio.ObtenerReporteSemanalAsync(inicioSemana);
                return Ok(mesas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el reporte semanal: {ex.Message}");
            }
        } 
        [HttpGet("reporte-diario/{fecha:datetime}")]
        public async Task<ActionResult<IEnumerable<ReporteMesas>>> ObtenerReporteDiario(DateTime fecha)
        {
            try
            {
                var mesas = await _reporteservicio.ObtenerReporteDiarioAsync(fecha);
                return Ok(mesas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener el reporte diario: {ex.Message}");
            }
        }
        [HttpGet("productos-mas-vendidos/{topN:int}")]
        public async Task<ActionResult<IEnumerable<ProductoVenta>>> ObtenerProductosMasVendidos(int topN)
        {
            try
            {
                var productos = await _reporteservicio.ObtenerProductosMasVendidosAsync(topN);
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener los productos más vendidos: {ex.Message}");
            }
        }
  
    }
   
    

}

