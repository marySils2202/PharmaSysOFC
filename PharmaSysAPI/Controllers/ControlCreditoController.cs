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
    public class ControlCreditoController : ControllerBase
    {
        private readonly string cadenaSQL;

        public ControlCreditoController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<ControlCredito> Controlescredito = new List<ControlCredito>();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_ControlesCredito", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Controlescredito.Add(new ControlCredito()
                                {
                                    IdControlCrédito = reader.GetInt32("IdControlCrédito"),
                                    IdVenta = reader.GetInt32("IdVenta"),
                                    MontoAbono = reader.GetDecimal("MontoAbono"),
                                    NumeroAbono = reader.GetInt32("NumeroAbono"),
                                    IdCrédito = reader.GetInt32("IdCrédito")
                                });
                            }



                        }
                    }
                    return Ok(new { mensaje = "OK", response = Controlescredito });
                }


            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpGet]
        [Route("Obtener/{IdControlCredito:int}")]

        public IActionResult Obtener(int IdControlCredito)
        {
            List<ControlCredito> Controles = new List<ControlCredito>();
            ControlCredito control = new ControlCredito();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_ControlesCredito", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Controles.Add(new ControlCredito()
                                {
                                    IdControlCrédito = reader.GetInt32("IdControlCrédito"),
                                    IdVenta = reader.GetInt32("IdVenta"),
                                    MontoAbono = reader.GetDecimal("MontoAbono"),
                                    NumeroAbono = reader.GetInt32("NumeroAbono"),
                                    IdCrédito = reader.GetInt32("IdCrédito")

                                });
                            }
                        }
                    }
                }
                control = Controles.Where(item => item.IdControlCrédito == IdControlCredito).FirstOrDefault();
                return Ok(new { mensaje = "OK", response = control });
            }

            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }

        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] ControlCredito objeto)
        {


            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_guardar_ControlCredito", connection))
                    {
                        cmd.Parameters.AddWithValue("IdVenta", objeto.IdVenta);
                        cmd.Parameters.AddWithValue("MontoAbono", objeto.MontoAbono);
                        cmd.Parameters.AddWithValue("FechaAbono", objeto.FechaAbono);
                        cmd.Parameters.AddWithValue("NumeroAbono", objeto.NumeroAbono);
                        cmd.Parameters.AddWithValue("idCredito", objeto.IdCrédito);
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

