using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.DeleteDishesForRestaurant;
public class DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommandHandler> logger,
	IRestaurantsRepository restaurantsRepository,
	IDishesRepository dishesRepository) : IRequestHandler<DeleteDishesForRestaurantCommand>
{
	public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
	{
		logger.LogWarning("Deleting all dishes for restaurant with id: {RestaurantId}", request.RestaurantId);

		var restaurant = await restaurantsRepository.GetAsync(request.RestaurantId);

		if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

		await dishesRepository.Delete(restaurant.Dishes);
	}
}
