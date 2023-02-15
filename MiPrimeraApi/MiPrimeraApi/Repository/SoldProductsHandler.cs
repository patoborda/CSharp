using MiPrimeraApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiPrimeraApi.Repository
{
    internal class SoldProductsHandler
    {
        public static string cadenaConexion = "Data Source=DESKTOP-G4PMQO5;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //*********************************** Traer ProductosVendidos (recibe el id del usuario y devuelve una lista de productos vendidos por ese usuario) ***********************************//
        public static List<Producto> ObtenerProductosVendidos(long IdUsuario)
        {
            List<long> ListaIdProductos = new List<long>();

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand(
                    " SELECT IdProducto FROM Venta INNER JOIN ProductoVendido  ON Venta.Id = ProductoVendido.IdVenta WHERE IdUsuario= @idUsuario", conn);

                comando.Parameters.AddWithValue("idUsuario", IdUsuario);
                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ListaIdProductos.Add(reader.GetInt64(0));
                    }
                }

                List<Producto> productos = new List<Producto>();
                foreach (var id in ListaIdProductos)
                {
                    Producto prodTemp = ProductHandler.ObtenerProducto(id);
                    productos.Add(prodTemp);
                }

                return productos;

            }
        }



        public static int InsertarProductoVendido(ProductoVenta productoVenta)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("INSERT INTO ProductoVendido (Stock, IdProducto, IdVenta) VALUES(@stock, @idProducto, @idVenta)", conn);
                comando.Parameters.AddWithValue("@stock",productoVenta.Stock);
                comando.Parameters.AddWithValue("@idProducto", productoVenta.IdProducto);
                comando.Parameters.AddWithValue("@idVenta", productoVenta.IdVenta);

                conn.Open();
                return comando.ExecuteNonQuery();
            }
        }
    }
}
