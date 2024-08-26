using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;
public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,
	IMapper mapper,
	IRestaurantsRepository restaurantsRepository,
	IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
{
	public Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
	{
		var currentUser = userContext.GetCurrentUser();

		// Log the serialized version of the request, labelled as Restaurant
		logger.LogInformation("{UserEmail} [{UserId}] is creating a new restaurant {@Restaurant}.",
			currentUser.Email,
			currentUser.Id,
			request);

		var r = mapper.Map<Restaurant>(request);

		r.OwnerId = currentUser.Id;

		return restaurantsRepository.Create(r);
	}
}
