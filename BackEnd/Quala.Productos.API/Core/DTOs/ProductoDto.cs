using System.ComponentModel.DataAnnotations;

namespace Quala.Productos.API.Core.DTOs
{
    public class ProductoDto
    {
        [Required]
        public int CodigoProducto { get; set; }

        [Required]
        [StringLength(250)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(250)]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ReferenciaInterna { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio unitario debe ser un valor positivo.")]
        public decimal PrecioUnitario { get; set; }

        [Required]
        public bool Estado { get; set; }

        [Required]
        [StringLength(50)]
        public string UnidadMedida { get; set; } = string.Empty;

        [Required]
        public DateTime FechaCreacion { get; set; }
    }
}
