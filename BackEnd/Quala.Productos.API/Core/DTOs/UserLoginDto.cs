using System.ComponentModel.DataAnnotations;

namespace Quala.Productos.API.Core.DTOs
{
    public class UserLoginDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder los 50 caracteres.")]
        public string Username { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "La contraseña no puede exceder los 50 caracteres.")]
        public string Password { get; set; }
    }
}
