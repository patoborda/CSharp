using Microsoft.OpenApi.Models;
using MiPrimeraApiV2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MiPrimeraApiV2.Repository
{
    internal static class ProductHandler
    {
        public static string cadenaConexion = "Data Source=DESKTOP-G4PMQO5;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //*********************************** Traer Productos (recibe un id de usuario y, devuelve una lista con todos los productos cargado por ese usuario) ***********************************//

        public static List<Producto> ObtenerProductos(long IdUsuario)
        {
            List<Producto> productos = new List<Producto>();
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Producto WHERE IdUsuario =@IdUsuario", conn);
                comando.Parameters.AddWithValue("IdUsuario", IdUsuario);
                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Producto productoTemporal = new Producto();
                        productoTemporal.Id = reader.GetInt64(0);
                        productoTemporal.Descripciones = reader.GetString(1);
                        productoTemporal.Costo = reader.GetDecimal(2);
                        productoTemporal.PrecioVenta = reader.GetDecimal(3);
                        productoTemporal.Stock = reader.GetInt32(4);
                        productoTemporal.IdUsuario = reader.GetInt64(5);

                        productos.Add(productoTemporal);
                    }
                }
                return productos;
            }

        }

        /****************************************************** INSERTAR PRODUCTO - SIRVE PARA AGREGAR UN PRODUCTO NUEVO ******************************************************/
        public static int InsertarProducto (Producto producto)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("INSERT INTO Producto(Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES(@descripciones, @costo, @precioVenta, @stock, @idUsuario)", conn);
                comando.Parameters.AddWithValue("@descripciones", producto.Descripciones);
                comando.Parameters.AddWithValue("@costo", producto.Costo);
                comando.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
                comando.Parameters.AddWithValue("@stock", producto.Stock);
                comando.Parameters.AddWithValue("@idUsuario", producto.IdUsuario);
                
               conn.Open();
               return comando.ExecuteNonQuery();
            }
        }
        /****************************************************** ELIMINAR PRODUCTO - ELIMINA TODOS LOS NODOS "HIJOS" QUE TENGAN QUE VER CON EL PRODUCTO ELEGIDO ******************************************************/
        public static int EliminarProducto(long id)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                try
                {
                    SqlCommand comando = new SqlCommand("DELETE FROM ProductoVendido WHERE IdProducto = @identificador DELETE FROM Producto WHERE id = @identificador", conn);
                    comando.Parameters.AddWithValue("@identificador", id);

                    conn.Open();
                    return comando.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(""+ex.Message);
                    return -1;
                }
            }
        }

        /****************************************************** MODIFICAR PRODUCTOS ******************************************************/
        public static int ModificarProducto(Producto producto)
        {
            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {
                SqlCommand comando = new SqlCommand("UPDATE Producto SET Descripciones = @descripciones, Costo = @costo, PrecioVenta = @precioVenta, Stock = @stock, IdUsuario = @idUsuario WHERE Id = @identificador", conn);
                comando.Parameters.AddWithValue("@identificador", producto.Id);
                comando.Parameters.AddWithValue("@descripciones", producto.Descripciones);
                comando.Parameters.AddWithValue("@costo", producto.Costo);
                comando.Parameters.AddWithValue("@precioVenta", producto.PrecioVenta);
                comando.Parameters.AddWithValue("@stock", producto.Stock);
                comando.Parameters.AddWithValue("@idUsuario", producto.IdUsuario);

                conn.Open();
                return comando.ExecuteNonQuery();
            }
        }

        /******************************************************  OBTENGER PRODUCTOS SEGUN ID ******************************************************/
        public static Producto ObtenerProducto(long id)
        {
            Producto producto = new Producto();

            using (SqlConnection conn = new SqlConnection(cadenaConexion))
            {

                SqlCommand comando = new SqlCommand("SELECT * FROM Producto WHERE Id=@Id", conn);


                comando.Parameters.AddWithValue("Id", id);

                conn.Open();

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    producto.Id = reader.GetInt64(0);
                    producto.Descripciones = reader.GetString(1);
                    producto.Costo = reader.GetDecimal(2);
                    producto.PrecioVenta = reader.GetDecimal(3);
                    producto.Stock = reader.GetInt32(4);
                    producto.IdUsuario = reader.GetInt64(5);

                }
            }
            return producto;
        }


        /****************************************************** ACTUALIZACION DE STOCK AL VENDER PRODUCTO ******************************************************/
        public static int UpdateStockProducto (long id, int cantidadVendidos)
        {
            Producto producto = ObtenerProducto(id);
            producto.Stock -= cantidadVendidos;
            return ModificarProducto(producto);
        }
    }
}