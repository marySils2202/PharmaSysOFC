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
    public class CreditoController : ControllerBase
    {
        private readonly string cadenaSQL;

        public CreditoController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Credito> creditos = new List<Credito>();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_Creditos", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                creditos.Add(new Credito()
                                {
                                    IdCredito = reader.GetInt32("IdCrédito"),
                                    Plazo = reader.GetInt32("Plazo"),
                                    MontoTotal = reader.GetDecimal("MontoTotal"),
                                    Estado = reader.GetString("Estado")
                                });
                            }



                        }
                    }
                    return Ok(new { mensaje = "OK", response = creditos });
                }


            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpGet]
        [Route("Obtener/{IdCredito:int}")]

        public IActionResult Obtener(int IdCredito)
        {
            List<Credito> creditos = new List<Credito>();
            Credito credito = new Credito();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_Creditos", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                creditos.Add(new Credito()
                                {
                                    IdCredito = reader.GetInt32("IdCrédito"),
                                    Plazo = reader.GetInt32("Plazo"),
                                    MontoTotal = reader.GetDecimal("MontoTotal"),
                                    Estado = reader.GetString("Estado")


                                });
                            }
                        }
                    }
                }
                credito = creditos.Where(item => item.IdCredito == IdCredito).FirstOrDefault();
                return Ok(new { mensaje = "OK", response = credito });
            }

            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }

        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Credito objeto)
        {


            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_guardar_Credito", connection))
                    {
                        cmd.Parameters.AddWithValue("plazo", objeto.Plazo);
                        cmd.Parameters.AddWithValue("montoTotal", objeto.MontoTotal);
                        cmd.Parameters.AddWithValue("estado", objeto.Estado);
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
    }
}