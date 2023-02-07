using PreEntrega_1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreEntrega_1.Handlers
{
    internal class ManejadorVenta
    {
    public static string cadenaConexion = "Data Source=DESKTOP-G4PMQO5;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static List<Venta> ObtenerVentas(long IdUsuario)
        {
            List<Venta> productosVentas = new List<Venta>();
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(" SELECT IdVenta, Comentarios FROM ProductoVendido  INNER JOIN Venta ON ProductoVendido.IdVenta= Venta.Id WHERE IdUsuario = @idUsuario", conn);
                comando.Parameters.AddWithValue("IdUsuario", IdUsuario);
                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Venta productoTemporal = new Venta();
                        productoTemporal.Id = reader.GetInt64(0);
                        productoTemporal.Comentarios= reader.GetString(1);

                        productosVentas.Add(productoTemporal);
                    }
                }
                return productosVentas;
            }
        }
    }
}
