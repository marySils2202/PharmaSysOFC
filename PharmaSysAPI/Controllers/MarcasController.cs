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
    public class MarcasController : ControllerBase
    {

        private readonly string cadenaSQL;

        public MarcasController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Marca> marcas = new List<Marca>();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_Marcas", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                marcas.Add(new Marca()
                                {
                                    IdMarca = reader.GetInt32(reader.GetOrdinal("IdMarca")),
                                    NombreMarca = reader.GetString(reader.GetOrdinal("NombreMarca"))


                                });
                            }



                        }
                    }
                    return Ok(new { mensaje = "OK", response = marcas });
                }


            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpGet]
        [Route("Obtener/{idMarca:int}")]

        public IActionResult Obtener(int idMarca)
        {
            List<Marca> marcas = new List<Marca>();
            Marca marca = new Marca();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_Marcas", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                marcas.Add(new Marca()
                                {

                                    IdMarca = reader.GetInt32(reader.GetOrdinal("IdMarca")),
                                    NombreMarca = reader.GetString(reader.GetOrdinal("NombreMarca"))


                                });
                            }
                        }
                    }
                }
                marca = marcas.Where(item => item.IdMarca == idMarca).FirstOrDefault();
                return Ok(new { mensaje = "OK", response = marca });
            }

            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Marca objeto)
        {


            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_guardar_Marca", connection))
                    {
                        cmd.Parameters.AddWithValue("nombre", objeto.NombreMarca);

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

        public IActionResult Editar([FromBody] Marca objeto)
        {


            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_editar_Marca", connection))
                    {
                        cmd.Parameters.AddWithValue("idProducto", objeto.IdMarca == 0 ? DBNull.Value : objeto.IdMarca);
                        cmd.Parameters.AddWithValue("nombre", objeto.NombreMarca is null ? DBNull.Value : objeto.NombreMarca);



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
        [Route("Eliminar/{idMarca:int}")]

        public IActionResult Eliminar(int idMarca)
        {


            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_eliminar_Marca", connection))
                    {
                        cmd.Parameters.AddWithValue("idMarca", idMarca);


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
