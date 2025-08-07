using Quala.Productos.API.Core.DTOs;
using Quala.Productos.API.Core.Entities;
using Quala.Productos.API.Core.Interfaces;

namespace Quala.Productos.API.Infrastructure.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<(bool Success, string ErrorMessage)> CreateProductoAsync(ProductoDto productoDto)
        {
            if (productoDto.FechaCreacion.Date < DateTime.UtcNow.Date)
            {
                return (false, "La fecha de creación no puede ser anterior a la fecha actual.");
            }

            if (await _repository.ExistsAsync(productoDto.CodigoProducto))
            {
                return (false, $"Ya existe un producto con el código {productoDto.CodigoProducto}.");
            }

            var producto = new Producto {
                CodigoProducto = productoDto.CodigoProducto,
                Nombre = productoDto.Nombre,
                Descripcion = productoDto.Descripcion,
                ReferenciaInterna = productoDto.ReferenciaInterna,
                PrecioUnitario = productoDto.PrecioUnitario,
                Estado = productoDto.Estado,
                UnidadMedida = productoDto.UnidadMedida,
                FechaCreacion = productoDto.FechaCreacion
            };

            await _repository.CreateAsync(producto);
            return (true, string.Empty);
        }

        public async Task<IEnumerable<Producto>> GetAllProductosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Producto?> GetProductoByIdAsync(int codigoProducto)
        {
            return await _repository.GetByIdAsync(codigoProducto);
        }

        public async Task<(bool Success, string ErrorMessage)> UpdateProductoAsync(ProductoDto productoDto)
        {
            if (productoDto.FechaCreacion.Date < DateTime.UtcNow.Date)
            {
                return (false, "La fecha de creación no puede ser anterior a la fecha actual.");
            }
            if (!await _repository.ExistsAsync(productoDto.CodigoProducto))
            {
                return (false, $"No existe un producto con el código {productoDto.CodigoProducto}.");
            }
            var producto = new Producto {
                CodigoProducto = productoDto.CodigoProducto,
                Nombre = productoDto.Nombre,
                Descripcion = productoDto.Descripcion,
                ReferenciaInterna = productoDto.ReferenciaInterna,
                PrecioUnitario = productoDto.PrecioUnitario,
                Estado = productoDto.Estado,
                UnidadMedida = productoDto.UnidadMedida,
                FechaCreacion = productoDto.FechaCreacion
            };
            await _repository.UpdateAsync(producto);
            return (true, string.Empty);
        }
        public async Task<bool> DeleteProductoAsync(int codigoProducto)
        {
            if (!await _repository.ExistsAsync(codigoProducto))
            {
                return false;
            }
            return await _repository.DeleteAsync(codigoProducto);
        }

        public async Task<bool> ProductoExistsAsync(int codigoProducto)
        {
            return await _repository.ExistsAsync(codigoProducto);
        }
    }
}
