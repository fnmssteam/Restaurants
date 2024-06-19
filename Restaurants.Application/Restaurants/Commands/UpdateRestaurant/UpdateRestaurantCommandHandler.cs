using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
public class UpdateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<UpdateRestaurantCommand, bool>
{
	public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Updating restaurant with id: {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);

		var r = await restaurantsRepository.GetAsync(request.Id);

		if (r == null)
			return false;

		//r.Name = request.Name;
		//r.Description = request.Description;
		//r.HasDelivery = request.HasDelivery;

		mapper.Map(request, r);

		await restaurantsRepository.SaveChanges();

		return true;
	}
}
