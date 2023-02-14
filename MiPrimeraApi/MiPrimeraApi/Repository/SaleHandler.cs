using MiPrimeraApi;
using MiPrimeraApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiPrimeraApi.Repository
{
    internal class SaleHandler
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
                        productoTemporal.Comentarios = reader.GetString(1);

                        productosVentas.Add(productoTemporal);
                    }
                }
                return productosVentas;
            }
        }

        public static long InsertarVenta(Venta venta)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("INSERT INTO Venta(Comentarios, IdUsuario) VALUES(@comentarios, @idUsuario); SELECT @@IDENTITY", conn);
                comando.Parameters.AddWithValue("@comentarios", venta.Comentarios);
                comando.Parameters.AddWithValue("@idUsuario", venta.IdUsuario);

                conn.Open();
                return Convert.ToInt64(comando.ExecuteScalar());
            }
        }

        public static void CargarVenta(long idUsuario, List<Producto> productosVendidos)
        {
          Venta venta = new Venta();
            venta.Comentarios = "";
            venta.IdUsuario= idUsuario;
            long idVentaInsertada = InsertarVenta(venta);

            List<Producto> producto = new List<Producto>();
            foreach (var item in productosVendidos)
            {
                Producto prodTemp = ProductHandler.ObtenerProducto(item.Id);
                producto.Add(prodTemp);
                
            }   

        }
    }

}

/*

                item.Id = productosVendidos[0].Id;
                item.Descripciones = productosVendidos[1].Descripciones;
                item.Costo = productosVendidos[2].Costo;
                item.PrecioVenta = productosVendidos[3].PrecioVenta;
                item.Stock = productosVendidos[4].Stock;
                item.IdUsuario = idUsuario;
                producto.Add(item);
*/