using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quala.Productos.API.Core.DTOs;
using Quala.Productos.API.Core.Interfaces;

namespace Quala.Productos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            try
            {
                var productos = await _productoService.GetAllProductosAsync();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor.", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProducto([FromBody] ProductoDto productoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (success, errorMessage) = await _productoService.CreateProductoAsync(productoDto);

            if (!success)
            {
                return BadRequest(new { message = errorMessage });
            }

            return CreatedAtAction(nameof(GetProductoById), new { id = productoDto.CodigoProducto }, productoDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductoById(int id)
        {
            var producto = await _productoService.GetProductoByIdAsync(id);
            if (producto == null)
            {
                return NotFound(new { message = "Producto no encontrado." });
            }
            return Ok(producto);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] ProductoDto productoDto)
        {
            if (id != productoDto.CodigoProducto)
            {
                return BadRequest(new { message = "El ID del producto no coincide." });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var (success, errorMessage) = await _productoService.UpdateProductoAsync(productoDto);
            if (!success)
            {
                return BadRequest(new { message = errorMessage });
            }
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            if (!await _productoService.ProductoExistsAsync(id))
            {
                return NotFound(new { message = "Producto no encontrado." });
            }
            var success = await _productoService.DeleteProductoAsync(id);
            if (!success)
            {
                return BadRequest(new { message = "Error al eliminar el producto." });
            }
            return NoContent();
        }
    }
}

