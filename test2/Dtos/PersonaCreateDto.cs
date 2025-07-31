using System.ComponentModel.DataAnnotations;

namespace test2.Dtos
{
    public class PersonaCreateDto
    {
        [Required]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Solo letras")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        public string Correo { get; set; } = string.Empty;
    }
}
