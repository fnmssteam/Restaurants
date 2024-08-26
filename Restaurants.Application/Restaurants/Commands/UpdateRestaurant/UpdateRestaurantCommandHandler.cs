using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
	IMapper mapper,
	IRestaurantsRepository restaurantsRepository,
	IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<UpdateRestaurantCommand>
{
	public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Updating restaurant with id: {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);

		var r = await restaurantsRepository.GetAsync(request.Id);

		if (r == null)
			throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

		if (!restaurantAuthorizationService.Authorize(r, ResourceOperation.Update))
		{
			throw new ForbidException();
		}

		//r.Name = request.Name;
		//r.Description = request.Description;
		//r.HasDelivery = request.HasDelivery;

		mapper.Map(request, r);

		await restaurantsRepository.SaveChanges();
	}
}
