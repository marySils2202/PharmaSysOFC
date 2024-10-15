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
    public class CargoController : ControllerBase
    {
        private readonly string cadenaSQL;

        public CargoController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Cargo> cargos = new List<Cargo>();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_Cargos", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cargos.Add(new Cargo()
                                {
                                    IdCargo = reader.GetInt32(reader.GetOrdinal("IdCargo")),
                                    NombreCargo = reader.GetString(reader.GetOrdinal("NombreCargo"))


                                });
                            }



                        }
                    }
                    return Ok(new { mensaje = "OK", response = cargos });
                }


            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpGet]
        [Route("Obtener/{idCargo:int}")]

        public IActionResult Obtener(int idCargo)
        {
            List<Cargo> cargos = new List<Cargo>();
            Cargo cargo = new Cargo();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_Cargos", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cargos.Add(new Cargo()
                                {

                                    IdCargo = reader.GetInt32(reader.GetOrdinal("IdCargo")),
                                    NombreCargo = reader.GetString(reader.GetOrdinal("NombreCargo"))


                                });
                            }
                        }
                    }
                }
                cargo = cargos.Where(item => item.IdCargo == idCargo).FirstOrDefault();
                return Ok(new { mensaje = "OK", response = cargo });
            }

            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Cargo objeto)
        {


            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_guardar_Cargo", connection))
                    {
                        cmd.Parameters.AddWithValue("nombre", objeto.NombreCargo);

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

        public IActionResult Editar([FromBody] Cargo objeto)
        {


            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_editar_Marca", connection))
                    {
                        cmd.Parameters.AddWithValue("idProducto", objeto.IdCargo == 0 ? DBNull.Value : objeto.IdCargo);
                        cmd.Parameters.AddWithValue("nombre", objeto.NombreCargo is null ? DBNull.Value : objeto.NombreCargo);



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
        [Route("Eliminar/{idCargo:int}")]

        public IActionResult Eliminar(int idCargo)
        {


            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_eliminar_Cargo", connection))
                    {
                        cmd.Parameters.AddWithValue("idCargo", idCargo);


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

