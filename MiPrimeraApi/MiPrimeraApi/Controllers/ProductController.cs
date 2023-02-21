using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Models;
using MiPrimeraApi.Repository;

namespace MiPrimeraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("{idUsuario}")]
        public List<Producto> TraerProductos(long idUsuario)
        {
            return ProductHandler.ObtenerProductos(idUsuario);
        }

        [HttpPost]
        public int CreateProduct(Producto producto)
        {
            return ProductHandler.InsertarProducto(producto);
        }

        [HttpPut]
        public int UpdateProduct(Producto producto)
        {
            return ProductHandler.ModificarProducto(producto);
        }
        [HttpDelete("{id}")]
        public int DeleteProduct(int id)
        {
            return ProductHandler.EliminarProducto(id);
        }

    }
}
