using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiPrimeraApiV2.Models;
using MiPrimeraApiV2.Repository;

namespace MiPrimeraApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {
        /****************************************************** TRAER LISTA DE PRODUCTOS VENDIDOS ******************************************************/
        [HttpGet("{idUsuario}")]
        public List<Producto> TraerProductosVendidos(long idUsuario)
        {
           return SoldProductsHandler.ObtenerProductosVendidos(idUsuario);
            
        }

    }
}
