using MiPrimeraApiV2;
using MiPrimeraApiV2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiPrimeraApiV2.Repository
{
    internal class SaleHandler
    {
        public static string cadenaConexion = "Data Source=DESKTOP-G4PMQO5;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        /****************************************************** OBTENER VENTAS ******************************************************/
        public static List<Venta> ObtenerVentas(long IdUsuario)
        {
            List<Venta> productosVentas = new List<Venta>();
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {

                SqlCommand comando = new SqlCommand(" SELECT * FROM Venta WHERE IdUsuario = @idUsuario", conn);
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
                        productoTemporal.IdUsuario = reader.GetInt64(2);

                        productosVentas.Add(productoTemporal);
                    }
                }
                return productosVentas;
            }
        }
        /****************************************************** INSERTAR VENTA ******************************************************/
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
        /****************************************************** CARGARVENTA ******************************************************/
        public static void CargarVenta(long idUsuario, List<Producto> productosVendidos)
        {
          Venta venta = new Venta();
            venta.Comentarios = "";
            venta.IdUsuario= idUsuario;
            long idVentaInsertada = InsertarVenta(venta);

            foreach (var item in productosVendidos)
            {
                ProductoVenta pv = new ProductoVenta();
                pv.Stock= item.Stock;
                pv.IdProducto = item.Id;
                pv.IdVenta = idVentaInsertada;
                SoldProductsHandler.InsertarProductoVendido(pv);
            }
            foreach (var producto in productosVendidos)
            {
                ProductHandler.UpdateStockProducto(producto.Id, producto.Stock);     
            }
        }
    }
}