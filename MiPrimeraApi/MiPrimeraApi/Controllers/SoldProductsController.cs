using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Models;
using MiPrimeraApi.Repository;

namespace MiPrimeraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoldProductsController : ControllerBase
    {
        [HttpGet("{idUsuario}")]
        public List<ProductoVenta> TraerProductosVendidos(long idUsuario)
        {
           return SoldProductsHandler.ObtenerProductosVendidos(idUsuario);
            
        }

    }
}
