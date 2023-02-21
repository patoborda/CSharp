using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Models;
using MiPrimeraApi.Repository;

namespace MiPrimeraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        [HttpPost("{idUsuario}")]
        public void CargarVenta(long idUsuario, List<Producto> productosVendidos)
        {
            SaleHandler.CargarVenta(idUsuario, productosVendidos);    
        }

        [HttpGet("{idUsuario}")]
        public List<Venta> TraerVentas(long idUsuario)
        {
           return SaleHandler.ObtenerVentas(idUsuario);
        }
    }
}
