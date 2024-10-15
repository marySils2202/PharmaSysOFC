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
    public class DetalleVentaController : ControllerBase
    {
        private readonly string cadenaSQL;

        public DetalleVentaController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<DetalleVenta> Detalleventas = new List<DetalleVenta>();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_DetalleVentas", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Detalleventas.Add(new DetalleVenta()
                                {
                                   IdDetalleVenta = reader.GetInt32("IdDetalleVenta"),
                                   idVenta = reader.GetInt32("idVenta"),
                                    CantidadVendida = reader.GetInt32("CantidadVendida"),
                                    PrecioVenta = reader.GetDecimal("PrecioVenta"),
                                    SubtotalFactura = reader.GetDecimal("SubtotalFactura")



                                });
                            }



                        }
                    }
                    return Ok(new { mensaje = "OK", response = Detalleventas });
                }


            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpGet]
        [Route("Obtener/{ IdDetalleVenta:int}")]

        public IActionResult Obtener(int IdDetalleVenta)
        {
            List<DetalleVenta> Detalleventas = new List<DetalleVenta>();
            DetalleVenta detalle = new DetalleVenta();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_DetalleVentas", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Detalleventas.Add(new DetalleVenta()
                                {

                                    IdDetalleVenta = reader.GetInt32("IdDetalleVenta"),
                                    idVenta = reader.GetInt32("idVenta"),
                                    idProducto = reader.GetInt32("idProducto"),
                                    CantidadVendida = reader.GetInt32("CantidadVendida"),
                                    PrecioVenta = reader.GetDecimal("PrecioVenta"),
                                    SubtotalFactura = reader.GetDecimal("SubtotalFactura")



                                });
                            }
                        }
                    }
                }
                detalle = Detalleventas.Where(item => item.IdDetalleVenta== IdDetalleVenta).FirstOrDefault();
                return Ok(new { mensaje = "OK", response = detalle });
            }

            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] DetalleVenta objeto)
        {


            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_guardar_DetalleVentas", connection))
                    {
                        cmd.Parameters.AddWithValue("idVenta", objeto.idVenta);
                        cmd.Parameters.AddWithValue("IdProducto", objeto.idProducto);
                        cmd.Parameters.AddWithValue("CantidadVendida", objeto.CantidadVendida);
                        cmd.Parameters.AddWithValue("PrecioVenta", objeto.PrecioVenta);
                        cmd.Parameters.AddWithValue("SubtotalFactura", objeto.SubtotalFactura);

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
