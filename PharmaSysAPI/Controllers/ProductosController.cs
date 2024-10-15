using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using PharmaSysAPI.Models;
using System.Collections.Generic;
using System.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Cors;

namespace
     PharmaSysAPI.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly string cadenaSQL;

        public ProductosController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Producto> productos = new List<Producto>();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_Productos", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new Producto()
                                {


                                    IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                                    Nombre = reader.GetString(reader.GetOrdinal("NombreProducto")),
                                    Stock = reader.GetInt32(reader.GetOrdinal("Stock")),
                                    IdCategoria = reader.GetInt32(reader.GetOrdinal("IdCategoría")),
                                    IdMarca = reader.GetInt32(reader.GetOrdinal("IdMarca")),
                                    Precio = reader.GetDecimal(reader.GetOrdinal("PrecioVenta")),
                                    Descripcion = reader.GetString(reader.GetOrdinal("Descripción")),
                                  
                            } );
                        }

                        }
                    }
                    return Ok(new { mensaje = "OK", response =  productos});
                }


            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpGet]
        [Route("Obtener/{idProducto:int}")]

        public IActionResult Obtener(int idProducto)
        {
            List<Producto> productos =new List<Producto>();
            Producto producto = new Producto();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_productos", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(new Producto()
                                {

                                    IdProducto = reader.GetInt32("IdProducto"),
                                    Nombre = reader.GetString("NombreProducto"),
                                    Stock = reader.GetInt32("Stock"),
                                    IdCategoria = reader.GetInt32("IdCategoría"),
                                    IdMarca = reader.GetInt32("IdMarca"),
                                    Precio = reader.GetDecimal("PrecioVenta"),
                                    Descripcion = reader.GetString("Descripción")
                               

                                });
                            }
                        }
                    }
                    producto = productos.Where(item => item.IdProducto == idProducto).FirstOrDefault();
                    return Ok(new { mensaje = "OK", response = producto });
                }
              
            }

            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Producto objeto)
        {


            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_guardar_producto", connection))
                    {
                        cmd.Parameters.AddWithValue("nombre", objeto.Nombre);
                        cmd.Parameters.AddWithValue("stock", objeto.Stock);
                        cmd.Parameters.AddWithValue("idcategoria", objeto.IdCategoria);
                        cmd.Parameters.AddWithValue("idmarca", objeto.IdMarca);
                        cmd.Parameters.AddWithValue("precio", objeto.Precio);
                        cmd.Parameters.AddWithValue("descripcion", objeto.Descripcion);
                  
                       
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }

                return Ok(new { mensaje = "OK" });


            }

            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpPut]
        [Route("Editar")]

        public IActionResult Editar([FromBody] Producto objeto)
        {


            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_editar_producto", connection))
                    {
                        cmd.Parameters.AddWithValue("IdProducto", objeto.IdProducto == 0 ? DBNull.Value : objeto.IdProducto);
                        cmd.Parameters.AddWithValue("NombreProducto", objeto.Nombre is null ? DBNull.Value : objeto.Nombre);
                        cmd.Parameters.AddWithValue("Stock", objeto.Stock == 0 ? DBNull.Value : objeto.Stock);
                        cmd.Parameters.AddWithValue("IdCategoria", objeto.IdCategoria == 0 ? DBNull.Value : objeto.IdCategoria);
                        cmd.Parameters.AddWithValue("IdMarca", objeto.IdMarca == 0 ? DBNull.Value : objeto.IdMarca);
                        cmd.Parameters.AddWithValue("Precio", objeto.Precio == 0 ? DBNull.Value : objeto.Precio);
                        cmd.Parameters.AddWithValue("Descripción", objeto.Descripcion is null ? DBNull.Value : objeto.Descripcion);
                       
                      



                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();



                    }
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Editado" });

            }

            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpDelete]
        [Route("Eliminar/{idProducto:int}")]

        public IActionResult Eliminar(int idProducto)
        {


            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_eliminar_producto", connection))
                    {
                        cmd.Parameters.AddWithValue("IdProducto", idProducto);


                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();



                    }
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Eliminado" });

            }

            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
      
        
    }
}

