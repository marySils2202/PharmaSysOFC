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
    public class CompraDetalleController : ControllerBase
    {

        private readonly string cadenaSQL;

        public CompraDetalleController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<DetalleCompra> Detallecompras = new List<DetalleCompra>();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_DetalleCompras", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Detallecompras.Add(new DetalleCompra()
                                {
                                    idDetalleCompra = reader.GetInt32("IdDetalleCompra"),
                                    idCompra = reader.GetInt32("idCompra"),
                                    idProducto = reader.GetInt32("IdProducto"),
                                    CantidadCompra = reader.GetInt32("CantidadCompra"),
                                    PrecioCompra = reader.GetDecimal("PrecioCompra"),
                                    SubtotalCompra = reader.GetDecimal("SubtotalCompra")



                                });
                            }



                        }
                    }
                    return Ok(new { mensaje = "OK", response = Detallecompras });
                }


            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpGet]
        [Route("Obtener/{IdDetalleCompra:int}")]

        public IActionResult Obtener(int IdDetalleCompra)
        {
            List<DetalleCompra> Detallecompras = new List<DetalleCompra>();
            DetalleCompra detalle = new DetalleCompra();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_DetalleCompras", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Detallecompras.Add(new DetalleCompra()
                                {

                                    idDetalleCompra = reader.GetInt32("IdDetalleCompra"),
                                    idCompra = reader.GetInt32("idCompra"),
                                    idProducto = reader.GetInt32("IdProducto"),
                                    CantidadCompra = reader.GetInt32("CantidadCompra"),
                                    PrecioCompra = reader.GetDecimal("PrecioCompra"),
                                    SubtotalCompra = reader.GetDecimal("SubtotalCompra")


                                });
                            }
                        }
                    }
                }
                detalle = Detallecompras.Where(item => item.idDetalleCompra == IdDetalleCompra).FirstOrDefault();
                return Ok(new { mensaje = "OK", response = detalle });
            }

            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] DetalleCompra objeto)
        {


            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                { 
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_DetalleCompra_guardar", connection))
                    {
                        cmd.Parameters.AddWithValue("IdCompra", objeto.idCompra);
                        cmd.Parameters.AddWithValue("IdProducto", objeto.idProducto);
                        cmd.Parameters.AddWithValue("CantidadCompra", objeto.CantidadCompra);
                        cmd.Parameters.AddWithValue("PrecioCompra", objeto.PrecioCompra);
                        cmd.Parameters.AddWithValue("SubtotalCompra", objeto.SubtotalCompra);
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

