using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants;
public interface IRestaurantsService
{
	Task<IEnumerable<RestaurantDto>> GetAllRestaurants();
	Task<RestaurantDto?> GetRestaurant(int id);
	Task<int> Create(CreateRestaurantDto dto);
}