﻿using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;
public interface IRestaurantRepository
{
	Task<IEnumerable<Restaurant>> GetAllAsync();
	Task<Restaurant?> GetAsync(int id);
}
