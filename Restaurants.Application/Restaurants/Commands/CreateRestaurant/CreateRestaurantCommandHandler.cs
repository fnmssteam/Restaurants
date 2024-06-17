using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;
public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<CreateRestaurantCommand, int>
{
	public Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Creating a new restaurant.");

		var r = mapper.Map<Restaurant>(request);

		return restaurantsRepository.Create(r);
	}
}
