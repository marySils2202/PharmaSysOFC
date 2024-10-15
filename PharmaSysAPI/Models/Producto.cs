﻿namespace PharmaSysAPI.Models
{
    public class Producto
    {
        public int IdProducto {  get; set; }
        public string Nombre { get; set; }
        public int Stock {  get; set; }
        public int IdCategoria {  get; set; }
        public int IdMarca {  get; set; }
        public decimal Precio {  get; set; }
        public string Descripcion { get; set; }
     


    }
}
