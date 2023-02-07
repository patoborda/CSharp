using Microsoft.SqlServer.Server;
using PreEntrega_1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreEntrega_1.Handlers
{
    internal class ManejadorUsuario
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

        public static Usuario LogIn(string nombreUsuario, string contraseña)
        {
            Usuario usuario = new Usuario();

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(" SELECT * FROM Usuario WHERE NombreUsuario=@NombreUsuario AND Contraseña=@Contraseña", conn);
                
                    comando.Parameters.AddWithValue("NombreUsuario", nombreUsuario);
                    comando.Parameters.AddWithValue("Contraseña", contraseña);
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
    }
}
