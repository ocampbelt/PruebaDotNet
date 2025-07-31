using Microsoft.AspNetCore.Mvc;

namespace test2.Controllers
{
    /// <summary>
    /// Controlador que expone un endpoint para obtener pronósticos del clima.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        /// <summary>
        /// Lista de descripciones posibles para el clima.
        /// </summary>
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        /// <summary>
        /// Constructor que recibe un logger por inyección de dependencias.
        /// </summary>
        /// <param name="logger">Instancia de logger para el controlador.</param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Obtiene una lista de pronósticos del clima generados aleatoriamente.
        /// </summary>
        /// <returns>Una colección de objetos <see cref="WeatherForecast"/>.</returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
