using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;
internal class RestaurantsService(IRestaurantRepository restaurantsRepository, ILogger<RestaurantsService> logger, IMapper mapper) : IRestaurantsService
{
	public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
	{
		logger.LogInformation("Getting all restaurants");

		var restaurants = await restaurantsRepository.GetAllAsync();

		var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

		return restaurantsDto!;
	}

	public async Task<RestaurantDto?> GetRestaurant(int id)
	{
		logger.LogInformation($"Getting restaurant {id}");

		var restaurant = await restaurantsRepository.GetAsync(id);

		var restaurantDto = mapper.Map<RestaurantDto?>(restaurant);

		return restaurantDto;
	}

	public Task<int> Create(CreateRestaurantDto dto)
	{
		logger.LogInformation("Creating a new restaurant.");

		var r = mapper.Map<Restaurant>(dto);

		return restaurantsRepository.Create(r);
	}
}
