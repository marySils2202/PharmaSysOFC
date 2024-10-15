using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using PharmaSysAPI.Models;
using System.Collections.Generic;
using System.Collections;
using Microsoft.AspNetCore.Cors;

namespace PharmaSysAPI.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly string cadenaSQL;

        public ProveedoresController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Proveedores> proveedores = new List<Proveedores>();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_Proveedores", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                proveedores.Add(new Proveedores()
                                {
                                    IdProveedor = reader.GetInt32("IdProveedor"),
                                    NombreProveedor = reader.GetString("NombreProveedor"),
                                    DireccionProveedor = reader.GetString("DirecciónProveedor"),
                                    RUC = reader.GetString("RUC"),
                                    TelefonoProveedor = reader.GetInt32("TelefonoProveedor"),
                                    Email = reader.GetString("EmailProveedor")
                                  
                                  

                                });
                            }



                        }
                    }
                    return Ok(new { mensaje = "OK", response = proveedores });
                }


            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpGet]
        [Route("Obtener/{idProveedor:int}")]

        public IActionResult Obtener(int idProveedor)
        {
            List<Proveedores> proveedores = new List<Proveedores>();
            Proveedores proveedor = new Proveedores();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_Proveedores", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                proveedores.Add(new Proveedores()
                                {
                                    IdProveedor = reader.GetInt32("IdProveedor"),
                                    NombreProveedor = reader.GetString("NombreProveedor"),
                                    DireccionProveedor = reader.GetString(reader.GetOrdinal("DirecciónProveedor")),
                                    TelefonoProveedor = reader.GetInt32("TelefonoProveedor"),
                                    RUC = reader.GetString("RUC"),
                                    Email = reader.GetString("EmailProveedor")
                                    
                                });
                            }
                        }
                    }
                }
                proveedor = proveedores.Where(item => item.IdProveedor == idProveedor).FirstOrDefault();
                return Ok(new { mensaje = "OK", response = proveedor });
            }

            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Proveedores objeto)
        {


            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_guardar_Proveedores", connection))
                    {
                        cmd.Parameters.AddWithValue("nombre", objeto.NombreProveedor);
                        cmd.Parameters.AddWithValue("direccion", objeto.DireccionProveedor);
                        cmd.Parameters.AddWithValue("ruc", objeto.RUC);
                        cmd.Parameters.AddWithValue("telefono", objeto.TelefonoProveedor);
                        cmd.Parameters.AddWithValue("email", objeto.Email);
                        

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

        public IActionResult Editar([FromBody] Empleado objeto)
        {


            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_editar_producto", connection))
                    {
                        cmd.Parameters.AddWithValue("idProducto", objeto.IdEmpleado == 0 ? DBNull.Value : objeto.IdEmpleado);
                        cmd.Parameters.AddWithValue("nombre", objeto.NombreEmpleado is null ? DBNull.Value : objeto.NombreEmpleado);
                        cmd.Parameters.AddWithValue("cedula", objeto.CédulaEmpleado is null ? DBNull.Value : objeto.CédulaEmpleado);
                        cmd.Parameters.AddWithValue("telefono", objeto.TeléfonoEmpleado == 0 ? DBNull.Value : objeto.TeléfonoEmpleado);
                        cmd.Parameters.AddWithValue("email", objeto.Email is null ? DBNull.Value : objeto.Email);
                        cmd.Parameters.AddWithValue("direccion", objeto.DirecciónEmpleado is null ? DBNull.Value : objeto.DirecciónEmpleado);
                        cmd.Parameters.AddWithValue("genero", objeto.Genero is null ? DBNull.Value : objeto.Genero);
                        cmd.Parameters.AddWithValue("usuario", objeto.Usuario is null ? DBNull.Value : objeto.Usuario);
                        cmd.Parameters.AddWithValue("contraseña", objeto.Contraseña is null ? DBNull.Value : objeto.Contraseña);
                        cmd.Parameters.AddWithValue("IdCargo", objeto.IdCargo == 0 ? DBNull.Value : objeto.IdCargo);



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
        [Route("Eliminar/{idEmpleado:int}")]

        public IActionResult Eliminar(int idEmpleado)
        {


            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_eliminar_Empleado", connection))
                    {
                        cmd.Parameters.AddWithValue("idEmpleado", idEmpleado);


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
