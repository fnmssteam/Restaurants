﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;
public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
	IRestaurantsRepository restaurantsRepository,
	IDishesRepository dishesRepository,
	IMapper mapper) : IRequestHandler<CreateDishCommand, int>
{
	public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Create a new dish: {@DishRequest}", request);

		var restaurant = await restaurantsRepository.GetAsync(request.RestaurantId);

		if (restaurant == null) throw new NotFoundException(nameof(restaurant), request.RestaurantId.ToString());

		var dish = mapper.Map<Dish>(request);

		return await dishesRepository.Create(dish);
	}
}
