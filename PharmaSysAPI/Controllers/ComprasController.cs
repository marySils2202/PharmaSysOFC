using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using PharmaSysAPI.Models;
using System.Collections.Generic;
using System.Collections;
using Azure;
using Microsoft.AspNetCore.Cors;
namespace PharmaSysAPI.Controllers
{
  [EnableCors("ReglasCors")]
     [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly string cadenaSQL;

        public ComprasController (IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Compras> compras = new List<Compras>();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_Compras", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                compras.Add(new Compras()
                                {
                                      IdCompra = reader.GetInt32("IdCompra"),
                                    idProveedor = reader.GetInt32("IdProveedor"),
                                    fechaCompra = reader.GetDateTime("FechaCompra"),
                                    totalCompra = reader.GetDecimal("TotalCompra"),




                                });
                            }



                        }
                    }
                    return Ok(new { mensaje = "OK", response = compras });
                }


            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpGet]
        [Route("Obtener/{ IdCompra:int}")]

        public IActionResult Obtener(int IdCompra)
        {
            List<Compras> compras = new List<Compras>();
            Compras compra = new Compras();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_Compras", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                compras.Add(new Compras()
                                {

                                    IdCompra = reader.GetInt32("IdCompra"),
                                    idProveedor = reader.GetInt32("IdProveedor"),
                                    fechaCompra = reader.GetDateTime("fechaCompra"),
                                    totalCompra = reader.GetDecimal("totalCompra"),


                                });
                            }
                        }
                    }
                }
                compra = compras.Where(item => item.IdCompra == IdCompra).FirstOrDefault();
                return Ok(new { mensaje = "OK", response = compra });
            }

            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }

        [HttpGet]
        [Route("ObtenerUltimoIdCompra")]
        public IActionResult ObtenerUltimoIdCompra()
        {
            int ultimoIdCompra;

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_obtener_ultimo_IdCompra", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        ultimoIdCompra = Convert.ToInt32(cmd.ExecuteScalar());
                    } 
                }

                return Ok(new { idCompra = ultimoIdCompra });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Compras objeto)
        {


            try
            {
                int idCompraGenerada;
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_guardar_Compras", connection))
                    {
                        cmd.Parameters.AddWithValue("IdProveedor", objeto.idProveedor);
                        cmd.Parameters.AddWithValue("FechaCompra", objeto.fechaCompra);
                        cmd.Parameters.AddWithValue("TotalCompra", objeto.totalCompra);
                     
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                        idCompraGenerada = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }

                return Ok((new { idCompra = idCompraGenerada }));

            }

            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
    }
}
