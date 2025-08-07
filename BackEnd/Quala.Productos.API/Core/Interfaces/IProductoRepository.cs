using Quala.Productos.API.Core.Entities;

namespace Quala.Productos.API.Core.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<Producto?> GetByIdAsync(int codigoProducto);
        Task<int> CreateAsync(Producto producto);
        Task<bool> UpdateAsync(Producto producto);
        Task<bool> DeleteAsync(int codigoProducto);
        Task<bool> ExistsAsync(int codigoProducto);
    }
}
