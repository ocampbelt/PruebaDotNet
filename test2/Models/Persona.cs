using System.ComponentModel.DataAnnotations;

namespace test2.Models
{
    public class Persona
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public int? EdadEstimada { get; set; }
        public string? NacionalidadProbable { get; set; }

        public List<Direccion> Direcciones { get; set; } = new();
    }
}
