using System.ComponentModel.DataAnnotations;

namespace test2.Dtos
{
    public class PersonaCreateDto
    {
        [Required]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Solo letras")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^[\w\.-]+@[\w\.-]+\.\w{2,4}$", ErrorMessage = "El correo no tiene un formato válido")]
        public string Correo { get; set; } = string.Empty;
    }
}
