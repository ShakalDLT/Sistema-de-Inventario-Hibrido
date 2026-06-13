using Backend.Desktop.Data;
using Backend.Desktop.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Desktop.Services
{
    public class InventarioService
    {
        private readonly AppDbContext _db = new AppDbContext();

        public InventarioService() => _db.Database.EnsureCreated();

        // --- Gestión de Productos ---
        public void RegistrarProducto(Producto p)
        {
            _db.Productos.Add(p);
            _db.SaveChanges();
        }

        public List<Producto> ObtenerTodos() => _db.Productos.ToList();

        public decimal CalcularMargen(decimal costo, decimal venta) 
            => venta == 0 ? 0 : ((venta - costo) / venta) * 100;

        // --- Gestión de Ventas (Punto de Venta) ---
        public Venta RegistrarVenta(List<DetalleVenta> items, decimal pagoRecibido)
        {
            decimal total = items.Sum(i => i.PrecioUnitario * i.Cantidad);
            
            // 1. Validar y Descontar Stock
            foreach (var item in items)
            {
                var prod = _db.Productos.Find(item.ProductoId);
                if (prod == null || prod.Stock < item.Cantidad)
                    throw new Exception($"Stock insuficiente para: {prod?.Nombre ?? "Producto desconocido"}");
                
                prod.Stock -= item.Cantidad;
            }

            // 2. Crear registro de venta
            var venta = new Venta { 
                Total = total, 
                PagoRecibido = pagoRecibido, 
                Vuelto = pagoRecibido - total,
                Detalles = items 
            };
            
            _db.Ventas.Add(venta);
            _db.SaveChanges();
            
            return venta;
        }
    }
}