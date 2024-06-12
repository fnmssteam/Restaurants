namespace Restaurants.API.Controllers;

public class WeatherForecastService : IWeatherForecastService
{

	private static readonly string[] Summaries = new[]
	{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

	public IEnumerable<WeatherForecast> Get(int take, double minTemp, double maxTemp)
	{
		return Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		})
		.Where(w => w.TemperatureC >= minTemp && w.TemperatureC <= maxTemp)
		.Take(take)
		.ToArray();
	}
}
