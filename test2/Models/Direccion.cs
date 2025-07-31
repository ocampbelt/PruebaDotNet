using System.ComponentModel.DataAnnotations;

namespace test2.Models
{
    public class Direccion
    {
        public int Id { get; set; }

        [Required]
        public string Ciudad { get; set; } = string.Empty;

        [Required]
        public string Pais { get; set; } = string.Empty;

        public int PersonaId { get; set; }

        public Persona? Persona { get; set; }

    }
}
