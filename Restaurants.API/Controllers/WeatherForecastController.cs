using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

	private readonly ILogger<WeatherForecastController> _logger;
	private readonly IWeatherForecastService _weatherForecastService;

	public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
	{
		_logger = logger;
		_weatherForecastService = weatherForecastService;
	}

	//[HttpGet]
	//[Route("{take}/currentDay")]
	//public WeatherForecast Get([FromQuery] int max, [FromRoute] int take)
	//{
	//	var result = _weatherForecastService.Get().First();
	//	return result;
	//}

	[HttpPost("generate")]
	public ActionResult<IEnumerable<WeatherForecast>> Generate([FromQuery] int take, [FromBody] GenerateWeatherForecastDto body)
	{
		if (take <= 0 || body.MinTemp > body.MaxTemp) return BadRequest("The request is invalid.");

		var result = _weatherForecastService.Get(take, body.MinTemp, body.MaxTemp);

		return Ok(result);
	}
}
