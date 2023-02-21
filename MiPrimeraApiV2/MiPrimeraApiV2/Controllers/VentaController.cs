using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiPrimeraApiV2.Models;
using MiPrimeraApiV2.Repository;

namespace MiPrimeraApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        /****************************************************** CARGAR VENTA ******************************************************/
        [HttpPost("{idUsuario}")]
        public void CargarVenta(long idUsuario, List<Producto> productosVendidos)
        {
            SaleHandler.CargarVenta(idUsuario, productosVendidos);    
        }
        /****************************************************** TRAER VENTAS ******************************************************/
        [HttpGet("{idUsuario}")]
        public List<Venta> TraerVentas(long idUsuario)
        {
           return SaleHandler.ObtenerVentas(idUsuario);
        }
    }
}
