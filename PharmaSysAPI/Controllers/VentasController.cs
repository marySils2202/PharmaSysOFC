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
    public class VentasController : ControllerBase
    {
        private readonly string cadenaSQL;

        public VentasController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Ventas> ventas = new List<Ventas>();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_Ventas", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                             ventas.Add(new Ventas()
                                {
                           IdVenta = reader.GetInt32("IdVenta"),
                                 tipoFactura = reader.GetString("tipoFactura"),
                                 fechaVenta = reader.GetDateTime("fechaVenta"),
                                 idCliente = reader.GetInt32("idCliente"),
                                 idEmpleado = reader.GetInt32("idEmpleado"),
                                 totalVenta = reader.GetDecimal("totalVenta"),
                                 tipoPago = reader.GetString("tipoPago"),
                                 iva = reader.GetDecimal("iva"),
                                 estado = reader.GetString("estado")
                             });
                            }



                        }
                    }
                    return Ok(new { mensaje = "OK", response = ventas});
                }


            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpGet]
        [Route("Obtener/{IdVenta:int}")]

        public IActionResult Obtener(int IdVenta)
        {
            List<Ventas> ventas = new List<Ventas>();
       Ventas venta = new Ventas();

            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_lista_Ventas", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                             ventas .Add(new Ventas()
                                {

                                 IdVenta = reader.GetInt32("IdVenta"),
                                 tipoFactura = reader.GetString("tipoFactura"),
                                 fechaVenta = reader.GetDateTime("fechaVenta"),
                                 idCliente = reader.GetInt32("idCliente"),
                                 idEmpleado = reader.GetInt32("idEmpleado"),
                                 totalVenta = reader.GetDecimal("totalVenta"),
                                 tipoPago = reader.GetString("tipoPago"),
                                 iva = reader.GetDecimal("iva"),
                                 estado = reader.GetString("estado")


                             });
                            }
                        }
                    }
                }
                venta =ventas.Where(item => item.IdVenta == IdVenta).FirstOrDefault();
                return Ok(new { mensaje = "OK", response = venta });
            }

            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
            }
        }
        [HttpPost]
        [Route("Guardar")]

        public IActionResult Guardar([FromBody] Ventas objeto)
        {


            try
            {
                using (var connection = new SqlConnection(cadenaSQL))
                {
                    connection.Open();
                    using (var cmd = new SqlCommand("sp_guardar_Venta", connection))
                    {
                        cmd.Parameters.AddWithValue("tipoFactura", objeto.tipoFactura);
                        cmd.Parameters.AddWithValue("fechaVenta", objeto.fechaVenta);
                        cmd.Parameters.AddWithValue("idCliente", objeto.idCliente);
                        cmd.Parameters.AddWithValue("idEmpleado", objeto.idEmpleado);
                        cmd.Parameters.AddWithValue("totalVenta", objeto.totalVenta);
                        cmd.Parameters.AddWithValue("tipoPago", objeto.tipoPago);
                        cmd.Parameters.AddWithValue("iva", objeto.iva);
                        cmd.Parameters.AddWithValue("estado", objeto.estado);
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
        //[HttpPut]
        //[Route("Editar")]

        //public IActionResult Editar([FromBody] Ventas objeto)
        //{


        //    try
        //    {
        //        using (var connection = new SqlConnection(cadenaSQL))
        //        {
        //            connection.Open();
        //            using (var cmd = new SqlCommand("sp_editar_Marca", connection))
        //            {
        //                cmd.Parameters.AddWithValue("IdVenta", objeto.IdVenta == 0 ? DBNull.Value : objeto.IdVenta);
        //                cmd.Parameters.AddWithValue("tipoFactura", objeto.tipoFactura is null ? DBNull.Value : objeto.tipoFactura);
        //                cmd.Parameters.AddWithValue("fechaVenta", objeto.fechaVenta == default ? DBNull.Value : objeto.fechaVenta);
        //                cmd.Parameters.AddWithValue("idCliente", objeto.idCliente == 0 ? DBNull.Value : objeto.IdVenta);
        //                cmd.Parameters.AddWithValue("idEmpleado", objeto.idEmpleado == 0 ? DBNull.Value : objeto.IdVenta);
        //                cmd.Parameters.AddWithValue("totalVenta", objeto.totalVenta == 0 ? DBNull.Value : objeto.IdVenta);
        //                cmd.Parameters.AddWithValue("tipoPago", objeto.tipoPago is null ? DBNull.Value : objeto.tipoFactura);
        //                cmd.Parameters.AddWithValue("estado", objeto.estado is null ? DBNull.Value : objeto.estado);
        //                cmd.CommandType = CommandType.StoredProcedure;
                       


        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.ExecuteNonQuery();



        //            }
        //        }

        //        return StatusCode(StatusCodes.Status200OK, new { mensaje = "Editado" });

        //    }

        //    catch (Exception error)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
        //    }
        //}
        //[HttpDelete]
        //[Route("Eliminar/{idVenta:int}")]

        //public IActionResult Eliminar(int idCargo)
        //{


        //    try
        //    {
        //        using (var connection = new SqlConnection(cadenaSQL))
        //        {
        //            connection.Open();
        //            using (var cmd = new SqlCommand("sp_eliminar_Cargo", connection))
        //            {
        //                cmd.Parameters.AddWithValue("idCargo", idCargo);


        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.ExecuteNonQuery();



        //            }
        //        }

        //        return StatusCode(StatusCodes.Status200OK, new { mensaje = "Eliminado" });

        //    }

        //    catch (Exception error)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = $"Error: {error.Message}", response = (object)null });
        //    }
        //}
    }
}
