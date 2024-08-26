using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;
internal class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
{
	public async Task<int> Create(Dish dish)
	{
		dbContext.Dishes.Add(dish);

		await dbContext.SaveChangesAsync();

		// EF will automatically set the Id property of the Dish
		return dish.Id;
	}

	public async Task Delete(IEnumerable<Dish> dishes)
	{
		dbContext.Dishes.RemoveRange(dishes);

		await dbContext.SaveChangesAsync();
	}
}
