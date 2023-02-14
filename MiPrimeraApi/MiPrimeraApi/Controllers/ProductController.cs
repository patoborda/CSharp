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

        [HttpPost]
        public void CreateProduct(Producto producto)
        {
            ProductHandler.InsertarProducto(producto);
        }

        [HttpPut]
        public void UpdateProduct(Producto producto)
        {
            ProductHandler.ModificarProducto(producto);
        }
        [HttpDelete("{id}")]
        public void DeleteProduct(int id)
        {
            ProductHandler.EliminarProducto(id);
        }

    }
}
