using System.ComponentModel.DataAnnotations;

namespace test2.Dtos
{
    public class DireccionesCreateDto
    {
        [Required]
        public string Ciudad { get; set; } = string.Empty;

        [Required]
        public string Pais { get; set; } = string.Empty;

        [Required]
        public int PersonaId { get; set; }

    }
}
