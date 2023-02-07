using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreEntrega_1.Handlers;
using PreEntrega_1.Models;


namespace PreEntrega_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Traer usuario

            Console.WriteLine("\n ********** FUNCION OBTENER USUARIO ********** \n");
            Usuario usuario = ManejadorUsuario.ObtenerUsuario(1);
            Console.WriteLine("-Nombre- \t -Apellido- \t -Username-");
            Console.WriteLine(usuario.Nombre + "\t \t" + usuario.Apellido + "\t \t" + usuario.NombreUsuario);


            //Traer Productos (recibe un id de usuario y, devuelve una lista con todos los productos cargado por ese usuario)

            Console.WriteLine("\n ********** FUNCION OBTENER TODOS LOS PRODUCTOS QUE CARGO EL USUARIO CON ID 1 ********** \n");
            List<Producto> productos = ManejadorProducto.ObtenerProductos(1);
            Console.WriteLine("-Id- \t -Descripcion-");
            foreach (Producto item in productos)
            {
                Console.WriteLine(item.Id + "\t" + item.Descripciones);
            }

            //Traer ProductosVendidos (recibe el id del usuario y devuelve una lista de productos vendidos por ese usuario)

            Console.WriteLine("\n ********** FUNCION OBTENER TODOS LOS PRODUCTOS VENDIDOS QUE CARGO EL USUARIO CON ID 1 ********** \n");
            List<Producto> productos2= ManejadorProductosVendidos.ObtenerProductosVendidos(1);
            Console.WriteLine("-IdProducto- \t -Descripcion-");
            foreach (var item in productos2) {

                Console.WriteLine(item.Id + "\t\t" + item.Descripciones);
                    
             }

            //Traer Ventas (recibe el id del usuario y devuelve un a lista de Ventas realizadas por ese usuario)

            Console.WriteLine("\n ********** FUNCION OBTENER TODOS LAS VENTAS REALIZADAS POR EL USUARIO 2 ********** \n");
            List<Venta> listaVenta = ManejadorVenta.ObtenerVentas(2);
            Console.WriteLine("-Nro de Venta- \t -Comentarios-");
            foreach (var item in listaVenta)
            {
                Console.WriteLine(item.Id + "\t" + item.Comentarios);

            }


            //Inicio de sesión (recibe un usuario y contraseña y devuelve un objeto Usuario)

            Console.WriteLine("\n ********** FUNCION LOGIN ********** \n");
            Usuario usuarioLogIn = ManejadorUsuario.LogIn("eperez", "SoyErnestoPerez");
            Console.WriteLine(usuarioLogIn.NombreUsuario);
            if(usuarioLogIn.NombreUsuario != null)
            {
                Console.WriteLine("Ha ingresado con exito!");
            }
            else
            {
                Console.WriteLine("Usuario o contraseña incorrecto. Intente nuevamente"); 
            }
        }

    }
}  
