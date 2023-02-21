using Microsoft.AspNetCore.Mvc;
using MiPrimeraApiV2.Models;
using MiPrimeraApiV2.Repository;

namespace MiPrimeraApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        /****************************************************** CARGAR VENTA ******************************************************/
        [HttpGet("{nombreUsuario}/{contraseña}")]
        public Usuario Login(string nombreUsuario, string contraseña)
        {
            return UserHandler.LogIn(nombreUsuario, contraseña);
        }
        /****************************************************** TRAER USUARIO ******************************************************/
        [HttpGet("{nombreUsuario}")]
        public Usuario TraerUsuario(string nombreUsuario) {
            return UserHandler.GetUser(nombreUsuario);
        }
        /****************************************************** CREAR USUARIO - REGISTRO ******************************************************/
        [HttpPost]
        public int Registro(Usuario usuario)
        {
            return UserHandler.CrearUsuario(usuario);         
        }
        /****************************************************** MODIFICAR USUARIO ******************************************************/
        [HttpPut]
        public int Update(Usuario usuario)
        {
            return UserHandler.ModificarUsuario(usuario);        
        }
        /****************************************************** ELIMINAR USUARIO ******************************************************/
        [HttpDelete("{idUsuario}")]
        public int Delete(long idUsuario)
        {
            return UserHandler.EliminarUsuario(idUsuario);
        }

    }
}
