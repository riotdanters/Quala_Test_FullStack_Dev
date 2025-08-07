using Quala.Productos.API.Core.DTOs;
using Quala.Productos.API.Core.Entities;

namespace Quala.Productos.API.Core.Interfaces
{
    public interface IProductoService
    {
        Task<(bool Success, string ErrorMessage)> CreateProductoAsync(ProductoDto productoDto);
        Task<IEnumerable<Producto>> GetAllProductosAsync();
        Task<Producto?> GetProductoByIdAsync(int codigoProducto);
        Task<(bool Success, string ErrorMessage)> UpdateProductoAsync(ProductoDto productoDto);
        Task<bool> DeleteProductoAsync(int codigoProducto);
        Task<bool> ProductoExistsAsync(int codigoProducto);
    }
}
