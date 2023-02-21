using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Models;
using MiPrimeraApi.Repository;

namespace MiPrimeraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("{username}/{password}")]
        public Usuario Login(string username, string password)
        {
            return UserHandler.LogIn(username, password);
        }

        [HttpGet("{username}")]
        public Usuario TraerUsuario(string username) {
            return UserHandler.GetUser(username);
        }

        [HttpPost]
        public int Registro(Usuario usuario)
        {
            return UserHandler.CrearUsuario(usuario);         
        }
        [HttpPut]
        public int Update(Usuario usuario)
        {
            return UserHandler.ModificarUsuario(usuario);        
        }
        [HttpDelete("{idUsuario}")]
        public int Delete(long idUsuario)
        {
            return UserHandler.EliminarUsuario(idUsuario);
        }

    }
}
