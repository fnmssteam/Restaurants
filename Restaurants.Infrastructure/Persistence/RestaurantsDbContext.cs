using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence;
internal class RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options) : IdentityDbContext<User>(options)
{
	internal DbSet<Restaurant> Restaurants { get; set; }
	internal DbSet<Dish> Dishes { get; set; }


	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Restaurant>()
			.OwnsOne(r => r.Address);

		modelBuilder.Entity<Restaurant>()
			.HasMany(r => r.Dishes)
			.WithOne()
			.HasForeignKey(d => d.RestaurantId);

		modelBuilder.Entity<User>()
			// A user has many owned restaurants.
			.HasMany(o => o.OwnedRestaurants)
			// A restaurant, in turn, belongs to only one owner.
			.WithOne(r => r.Owner)
			// A restaurant entity should refer to the OwnerId to know which owner it belongs to.
			.HasForeignKey(r => r.OwnerId);
	}
}
