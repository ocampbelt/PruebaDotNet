using System.ComponentModel.DataAnnotations;

namespace test2.Models
{
    public class Persona
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "Solo letras")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Correo electrónico no válido")]
        public string Correo { get; set; } = string.Empty;

        public int? EdadEstimada { get; set; }
        public string? NacionalidadProbable { get; set; }
    }
}
