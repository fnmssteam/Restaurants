using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger,
	IMapper mapper,
	IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
{
	public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Getting all restaurants");

		// Below is a demonstration of an incorrect way to filter results. Filtering is done on the materialized set of results,
		// already in memory, rather than on the database query-side.
		//var searchLower = request.SearchPhrase.ToLower();
		//var restaurants = (await restaurantsRepository.GetAllAsync())
		//		.Where(r => r.Name.ToLower().Contains(searchLower)
		//			|| r.Description.ToLower().Contains(searchLower));

		var (restaurants, totalCount) = await restaurantsRepository.GetAllMatchingAsync(request.SearchPhrase,
			request.PageNumber,
			request.PageSize);

		var restaurantsDto = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

		var result = new PagedResult<RestaurantDto>(restaurantsDto, totalCount, request.PageSize, request.PageNumber);

		return result;
	}
}
