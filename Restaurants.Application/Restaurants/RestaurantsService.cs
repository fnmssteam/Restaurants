﻿using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;
internal class RestaurantsService(IRestaurantRepository restaurantsRepository, ILogger<RestaurantsService> logger) : IRestaurantsService
{
	public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
	{
		logger.LogInformation("Getting all restaurants");

		var restaurants = await restaurantsRepository.GetAllAsync();

		var restaurantsDto = restaurants.Select(RestaurantDto.FromEntity);

		return restaurantsDto!;
	}

	public async Task<RestaurantDto?> GetRestaurant(int id)
	{
		logger.LogInformation($"Getting restaurant {id}");

		var restaurant = await restaurantsRepository.GetAsync(id);

		var restaurantDto = RestaurantDto.FromEntity(restaurant);

		return restaurantDto;
	}
}
