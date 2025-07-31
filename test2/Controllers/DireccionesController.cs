using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test2.Data;
using test2.Dtos;
using test2.Models;

namespace test2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DireccionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DireccionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/direcciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Direccion>>> ObtenerTodos()
        {
            var direcciones = await _context.Direcciones
                .Include(d => d.Persona)
                .ToListAsync();

            return Ok(direcciones);
        }

        // GET: api/direcciones/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Direccion>> ObtenerPorId(int id)
        {
            var direccion = await _context.Direcciones
                .Include(d => d.Persona)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (direccion == null)
                return NotFound(new { mensaje = "Dirección no encontrada" });

            return Ok(direccion);
        }

        // POST: api/direcciones
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] DireccionesCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var persona = await _context.Personas.FindAsync(dto.PersonaId);
            if (persona == null)
                return NotFound(new { mensaje = "Persona no encontrada para asignar dirección" });

            var direccion = new Direccion
            {
                Ciudad = dto.Ciudad,
                Pais = dto.Pais,
                PersonaId = dto.PersonaId
            };

            _context.Direcciones.Add(direccion);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Dirección creada correctamente" });
        }

        // PUT: api/direcciones/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] DireccionesCreateDto dto)
        {
            var direccion = await _context.Direcciones.FindAsync(id);
            if (direccion == null)
                return NotFound(new { mensaje = "Dirección no encontrada" });

            direccion.Ciudad = dto.Ciudad;
            direccion.Pais = dto.Pais;
            direccion.PersonaId = dto.PersonaId;

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Dirección actualizada correctamente" });
        }

        // DELETE: api/direcciones/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var direccion = await _context.Direcciones.FindAsync(id);
            if (direccion == null)
                return NotFound(new { mensaje = "Dirección no encontrada" });

            _context.Direcciones.Remove(direccion);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Dirección eliminada correctamente" });
        }
    }
}
