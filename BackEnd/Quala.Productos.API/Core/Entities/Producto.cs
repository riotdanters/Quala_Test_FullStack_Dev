namespace Quala.Productos.API.Core.Entities
{
    public class Producto
    {
        public int CodigoProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ReferenciaInterna { get; set; }
        public decimal PrecioUnitario { get; set; }
        public bool Estado { get; set; }
        public string UnidadMedida { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
