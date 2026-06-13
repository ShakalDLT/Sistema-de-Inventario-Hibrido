using System.ComponentModel.DataAnnotations;

namespace Backend.Desktop.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required] public string SKU { get; set; } = null!;
        [Required] public string Nombre { get; set; } = null!;
        public decimal CostoActual { get; set; }
        public decimal PrecioVentaActual { get; set; }
        public int Stock { get; set; }

        // Datos sincronizados (Resumen desde Web)
        public decimal PorcentajeAumentoMensual { get; set; }
        public decimal GananciaTotalMensual { get; set; }
    }
}