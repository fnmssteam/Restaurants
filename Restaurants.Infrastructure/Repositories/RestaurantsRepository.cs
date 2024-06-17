using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;
internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
	public async Task<int> Create(Restaurant restaurant)
	{
		dbContext.Restaurants.Add(restaurant);

		await dbContext.SaveChangesAsync();

		// EF will automatically set the Id property of the restaurant
		return restaurant.Id;
	}

	public async Task Delete(Restaurant restaurant)
	{
		dbContext.Remove(restaurant);

		await dbContext.SaveChangesAsync();
	}

	public async Task<IEnumerable<Restaurant>> GetAllAsync()
	{
		var restaurants = await dbContext.Restaurants.ToListAsync();

		return restaurants;
	}

	public async Task<Restaurant?> GetAsync(int id)
	{
		var restaurant = await dbContext.Restaurants
			.Include(r => r.Dishes)
			.FirstOrDefaultAsync(x => x.Id == id);

		return restaurant;
	}

	public async Task SaveChanges() =>
		await dbContext.SaveChangesAsync();
}
