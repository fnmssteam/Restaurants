
namespace Restaurants.API.Controllers;

public interface IWeatherForecastService
{
	IEnumerable<WeatherForecast> Get(int take, double minTemp, double maxTemp);
}