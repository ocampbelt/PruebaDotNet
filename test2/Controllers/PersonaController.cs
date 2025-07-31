using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using test2.Data;
using test2.Dtos;
using test2.Models;
using test2.Responses;

namespace test2.Controllers
{
    /// <summary>
    /// Controlador API para gestionar operaciones CRUD sobre la entidad Persona.
    /// Incluye integración con APIs externas para estimar edad y nacionalidad.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PersonasController : ControllerBase
    {
        /// <summary>
        /// Contexto de base de datos para acceder a las entidades.
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Cliente HTTP para consumir APIs externas.
        /// </summary>
        private readonly HttpClient _http;

        /// <summary>
        /// Inicializa una nueva instancia del controlador <see cref="PersonasController"/>.
        /// </summary>
        /// <param name="context">Contexto de base de datos inyectado.</param>
        public PersonasController(ApplicationDbContext context)
        {
            _context = context;
            _http = new HttpClient();
        }

        /// <summary>
        /// Crea una nueva persona en la base de datos, obteniendo edad estimada y nacionalidad probable
        /// desde APIs externas (Agify y Nationalize).
        /// </summary>
        /// <param name="dto">Datos de la persona a crear.</param>
        /// <returns>
        /// <see cref="IActionResult"/> con el resultado de la operación:
        /// - 400 si el modelo no es válido o el correo ya existe.
        /// - 200 si la persona se crea correctamente.
        /// </returns>
        /// <remarks>
        /// Este método realiza validaciones sobre el modelo recibido y consulta dos APIs externas:
        /// <list type="bullet">
        /// <item>Agify: Estima la edad según el nombre.</item>
        /// <item>Nationalize: Estima la nacionalidad según el nombre.</item>
        /// </list>
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] PersonaCreateDto dto)
        {
            // Validación de modelo
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Validación de correo único
            if (await _context.Personas.AnyAsync(p => p.Correo == dto.Correo))
                return BadRequest(new { mensaje = "El correo ya existe" });

            // Consulta API Agify para estimar edad
            var agify = await _http.GetFromJsonAsync<AgiFyResponse>($"https://api.agify.io/?name={dto.Nombre}");

            // Consulta API Nationalize para estimar nacionalidad
            var national = await _http.GetFromJsonAsync<NationalizeResponse>($"https://api.nationalize.io/?name={dto.Nombre}");

            // Creación de la entidad Persona
            var persona = new Persona
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                EdadEstimada = agify?.Age,
                NacionalidadProbable = national?.Country.FirstOrDefault()?.Country_Id
            };

            // Persistencia en base de datos
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Datos insertados correctamente" });
        }

        /// <summary>
        /// Obtiene todas las personas registradas en la base de datos.
        /// </summary>
        /// <returns>
        /// <see cref="ActionResult{IEnumerable{Persona}}"/> con la lista de personas.
        /// </returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> ObtenerTodos()
        {
            var personas = await _context.Personas.ToListAsync();
            return Ok(personas);
        }

        /// <summary>
        /// Obtiene una persona por su identificador único.
        /// </summary>
        /// <param name="id">Identificador de la persona.</param>
        /// <returns>
        /// <see cref="ActionResult{Persona}"/> con la persona encontrada o mensaje de error si no existe.
        /// </returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Persona>> ObtenerPorId(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
                return NotFound(new { mensaje = "Persona no encontrada" });
            return Ok(persona);
        }
    }
}
