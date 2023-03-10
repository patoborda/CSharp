using Microsoft.SqlServer.Server;
using MiPrimeraApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiPrimeraApi.Repository
{
    internal class UserHandler
    {
        public static string cadenaConexion = "Data Source=DESKTOP-G4PMQO5;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        //********************** Traer Usuario (recibe un int) **********************//
        public static Usuario ObtenerUsuario(long Id)
        {
            Usuario usuario = new Usuario();

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuario WHERE Id = @Id", conn);
                comando.Parameters.AddWithValue("Id", Id);
                conn.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    usuario.Id = (int)reader.GetInt64(0);
                    usuario.Nombre = reader.GetString(1);
                    usuario.Apellido = reader.GetString(2);
                    usuario.NombreUsuario = reader.GetString(3);
                    usuario.Contraseña = reader.GetString(4);
                    usuario.Mail = reader.GetString(5);

                }
            }
            return usuario;
        }

        public static Usuario BuscarUsuario(string nombreUsuario, string mail)
        {
            Usuario usuario = new Usuario();

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuario OR Mail = @mail", conn);
                         
                comando.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                comando.Parameters.AddWithValue("@mail", mail);
              
                conn.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    usuario.Id = (int)reader.GetInt64(0);
                    usuario.Nombre = reader.GetString(1);
                    usuario.Apellido = reader.GetString(2);
                    usuario.NombreUsuario = reader.GetString(3);
                    usuario.Contraseña = reader.GetString(4);
                    usuario.Mail = reader.GetString(5);

                }
            }
            return usuario;
        }

        public static Usuario GetUser(string nombreUsuario)
        {
            Usuario usuario = new Usuario();

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuario", conn);

                comando.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
                conn.Open();
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    usuario.Id = (int)reader.GetInt64(0);
                    usuario.Nombre = reader.GetString(1);
                    usuario.Apellido = reader.GetString(2);
                    usuario.NombreUsuario = reader.GetString(3);
                    usuario.Contraseña = reader.GetString(4);
                    usuario.Mail = reader.GetString(5);

                }
            }
            return usuario;
        }

        public static int CrearUsuario(Usuario usuario)
        {

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                Usuario UsuarioEncontrado = BuscarUsuario(usuario.NombreUsuario, usuario.Mail);
                
                SqlCommand comando = new SqlCommand(" if(not exists (SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuario OR Mail = @mail))" +
                    " begin" +
                    " INSERT INTO Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail) " +
                    " VALUES (@nombre, @apellido, @nombreUsuario, @contraseña, @mail)" +
                    " end", conn);
                comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
                comando.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario);
                comando.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
                comando.Parameters.AddWithValue("@mail", usuario.Mail);


                if(UsuarioEncontrado.Mail == usuario.Mail || UsuarioEncontrado.NombreUsuario == usuario.NombreUsuario) {
                    Console.WriteLine("Usuario y/o Mail en uso");
                }
                else
                {
                    Console.WriteLine(usuario.NombreUsuario + " Te has registrado con exito");
                }
                    conn.Open();
                    return comando.ExecuteNonQuery();
            }           
         }
        
        


        public static int ModificarUsuario(Usuario usuario)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                Usuario UsuarioEncontrado = BuscarUsuario(usuario.NombreUsuario, usuario.Mail);

                SqlCommand comando = new SqlCommand(" if(not exists(SELECT * FROM Usuario WHERE NombreUsuario = @nombreUsuario OR Mail = @mail))" +
                    " begin " +
                    " UPDATE Usuario " +
                    " SET Nombre = @nombre, Apellido = @apellido, NombreUsuario = @nombreUsuario, Contraseña = @contraseña, Mail = @mail " +
                    " WHERE Id = @identificador" +
                    " end", conn);

                comando.Parameters.AddWithValue("@identificador", usuario.Id);
                comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
                comando.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario);
                comando.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
                comando.Parameters.AddWithValue("@mail", usuario.Mail);

                if (UsuarioEncontrado.Mail == usuario.Mail || UsuarioEncontrado.NombreUsuario == usuario.NombreUsuario)
                {
                    Console.WriteLine("Usuario y/o Mail en uso");
                }
                else
                {
                    Console.WriteLine(usuario.NombreUsuario + " Has modificado con exito");
                }
                    conn.Open();
                    return comando.ExecuteNonQuery();
            }
        }




        public static Usuario LogIn(string username, string password)
        {
            Usuario usuario = new Usuario();

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(" SELECT * FROM Usuario WHERE NombreUsuario=@NombreUsuario AND Contraseña=@Contraseña", conn);

                comando.Parameters.AddWithValue("@NombreUsuario", username);
                comando.Parameters.AddWithValue("@Contraseña", password);
                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    usuario.Id = (int)reader.GetInt64(0);
                    usuario.Nombre += reader.GetString(1);
                    usuario.Apellido += reader.GetString(2);
                    usuario.NombreUsuario += reader.GetString(3);
                    usuario.Contraseña += reader.GetString(4);
                    usuario.Mail += reader.GetString(5);
                }

                return usuario;


            }
        }



        public static int EliminarUsuario(long id) {

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    SqlCommand comando = new SqlCommand(" DELETE FROM dbo.ProductoVendido WHERE IdVenta = @identificador  DELETE FROM dbo.Producto   WHERE IdUsuario =@identificador   DELETE FROM dbo.Venta   WHERE IdUsuario = @identificador  DELETE FROM dbo.Usuario    WHERE Id = @identificador", conn);
                    comando.Parameters.AddWithValue("@identificador", id);
                    conn.Open();
                    return comando.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("" + ex.Message);
                    return -1;
                }
            }


        }
    }
}
