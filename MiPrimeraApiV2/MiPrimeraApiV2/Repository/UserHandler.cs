using Microsoft.SqlServer.Server;
using MiPrimeraApiV2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiPrimeraApiV2.Repository
{
    internal class UserHandler
    {
        public static string cadenaConexion = "Data Source=DESKTOP-G4PMQO5;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        /****************************************************** Traer Usuario (recibe un int) ******************************************************/
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


        /****************************************************** BUSCAR USUARIO POR USERNAME Y MAIL - SIRVE PARA VERIFICAR QUE NO SE REPITAN USER Y CORREO AL REGISTRARSE Y/O AL MODIFICAR UN USUARIO ******************************************************/
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

        /****************************************************** OBTENGO USUARIO SEGUN SU USERNAME ******************************************************/
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


        /****************************************************** CREACION DE USUARIO ******************************************************/
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

                /*Uso de condiciones para verificar por consola si se creo con exito el usuario al no repetirse*/
                if (UsuarioEncontrado.Mail == usuario.Mail || UsuarioEncontrado.NombreUsuario == usuario.NombreUsuario) {
                    Console.WriteLine("USUARIO Y/O MAIL YA EN USO, REGISTRESE DE NUEVO");
                }
                else
                {
                    Console.WriteLine(usuario.NombreUsuario + " TE HAS REGISTRADO CON EXITO");
                }
                    conn.Open();
                    return comando.ExecuteNonQuery();
            }           
         }



        /****************************************************** MODIFICACION DE USUARIO ******************************************************/
        public static int ModificarUsuario(Usuario usuario)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                Usuario UsuarioEncontrado = BuscarUsuario(usuario.NombreUsuario, usuario.Mail);

                SqlCommand comando = new SqlCommand(
                    " UPDATE Usuario " +
                    " SET Nombre = @nombre, Apellido = @apellido, NombreUsuario = @nombreUsuario, Contraseña = @contraseña, Mail = @mail " +
                    " WHERE Id = @identificador", conn);

                comando.Parameters.AddWithValue("@identificador", usuario.Id);
                comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
                comando.Parameters.AddWithValue("@nombreUsuario", usuario.NombreUsuario);
                comando.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
                comando.Parameters.AddWithValue("@mail", usuario.Mail);

                /*Uso de condiciones para verificar por consola si se modifico con exito el usuario al no repetirse*/
                if (UsuarioEncontrado.Mail == usuario.Mail || UsuarioEncontrado.NombreUsuario == usuario.NombreUsuario)
                {
                    Console.WriteLine("ADVERTENCIA: MAIL Y/O USUARIO YA EN USO VERIFIQUE ANTES DE CONTINUAR");
                }
                else
                {
                    Console.WriteLine(usuario.NombreUsuario + " HAS MODIFICADO CON EXITO!");
                }
                    conn.Open();
                    return comando.ExecuteNonQuery();
            }
        }



        /****************************************************** LOGIN DE USUARIO ******************************************************/
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


        /****************************************************** ELIMINAR USUARIO Y TODOS SUS NODOS "HIJOS" ******************************************************/
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
