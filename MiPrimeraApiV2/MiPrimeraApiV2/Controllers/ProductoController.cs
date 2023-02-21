using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiPrimeraApiV2.Models;
using MiPrimeraApiV2.Repository;

namespace MiPrimeraApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        /****************************************************** TRAER PRODUCTOS CARGADOS POR EL USUARIO ******************************************************/
        [HttpGet("{idUsuario}")]
        public List<Producto> TraerProductos(long idUsuario)
        {
            return ProductHandler.ObtenerProductos(idUsuario);
        }
        /****************************************************** CREAR PRODUCTO ******************************************************/
        [HttpPost]
        public int CreateProduct(Producto producto)
        {
            return ProductHandler.InsertarProducto(producto);
        }
        /****************************************************** MODIFICAR PRODUCTO ******************************************************/
        [HttpPut]
        public int UpdateProduct(Producto producto)
        {
            return ProductHandler.ModificarProducto(producto);
        }
        /****************************************************** ELIMINAR PRODUCTO SEGUN ID INGRESADO ******************************************************/
        [HttpDelete("{id}")]
        public int DeleteProduct(int id)
        {
            return ProductHandler.EliminarProducto(id);
        }

    }
}
