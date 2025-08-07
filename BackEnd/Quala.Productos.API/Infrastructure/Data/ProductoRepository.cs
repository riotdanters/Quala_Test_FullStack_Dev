using Dapper;
using Quala.Productos.API.Core.Entities;
using Quala.Productos.API.Core.Interfaces;
using System.Data;

namespace Quala.Productos.API.Infrastructure.Data
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly DapperContext _context;

        public ProductoRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            using var connection = _context.CreateConnection();
            return await connection.QueryAsync<Producto>("afar_sp_ObtenerProductos", commandType: CommandType.StoredProcedure);
        }

        public async Task<Producto?> GetByIdAsync(int codigoProducto)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("CodigoProducto", codigoProducto, DbType.Int32);
            return await connection.QuerySingleOrDefaultAsync<Producto>("afar_sp_ObtenerProductoPorCodigo", parameters, commandType: CommandType.StoredProcedure);
        }
        
        public async Task<int> CreateAsync(Producto producto)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("CodigoProducto", producto.CodigoProducto, DbType.Int32);
            parameters.Add("Nombre", producto.Nombre, DbType.String);
            parameters.Add("Descripcion", producto.Descripcion, DbType.String);
            parameters.Add("ReferenciaInterna", producto.ReferenciaInterna, DbType.String);
            parameters.Add("PrecioUnitario", producto.PrecioUnitario, DbType.Decimal);
            parameters.Add("Estado", producto.Estado, DbType.Boolean);
            parameters.Add("UnidadMedida", producto.UnidadMedida, DbType.String);
            parameters.Add("FechaCreacion", producto.FechaCreacion, DbType.DateTime);
            var result = await connection.ExecuteScalarAsync<int>("afar_sp_CrearProducto", parameters, commandType: CommandType.StoredProcedure);
            return result;
        }

        public async Task<bool> UpdateAsync(Producto producto)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("CodigoProducto", producto.CodigoProducto, DbType.Int32);
            parameters.Add("Nombre", producto.Nombre, DbType.String);
            parameters.Add("Descripcion", producto.Descripcion, DbType.String);
            parameters.Add("ReferenciaInterna", producto.ReferenciaInterna, DbType.String);
            parameters.Add("PrecioUnitario", producto.PrecioUnitario, DbType.Decimal);
            parameters.Add("Estado", producto.Estado, DbType.Boolean);
            parameters.Add("UnidadMedida", producto.UnidadMedida, DbType.String);
            var result = await connection.ExecuteAsync("afar_sp_ActualizarProducto", parameters, commandType: CommandType.StoredProcedure);
            return result > 0;
        }
        public async Task<bool> DeleteAsync(int codigoProducto)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("CodigoProducto", codigoProducto, DbType.Int32);
            var result = await connection.ExecuteAsync("afar_sp_EliminarProducto", parameters, commandType: CommandType.StoredProcedure);
            return result > 0;
        }
        public async Task<bool> ExistsAsync(int codigoProducto)
        {
            using var connection = _context.CreateConnection();
            var parameters = new DynamicParameters();
            parameters.Add("CodigoProducto", codigoProducto, DbType.Int32);
            var result = await connection.ExecuteScalarAsync<int>("afar_sp_ObtenerProductoPorCodigo", parameters, commandType: CommandType.StoredProcedure);
            return result > 0;
        }

    }
}
