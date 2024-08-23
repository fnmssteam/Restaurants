namespace Restaurants.Application.Common;
public class PagedResult<T>
{
	public PagedResult(IEnumerable<T> items, int totalCount, int pageSize, int pageNumber)
	{
		Items = items;
		TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
		ItemsFrom = pageSize * (pageNumber - 1) + 1;
		ItemsTo = Math.Min(pageSize * pageNumber, totalCount);
	}

	public IEnumerable<T> Items { get; set; }
	public int TotalPages { get; set; }
	public int TotalItemsCount { get; set; }
	public int ItemsFrom { get; set; }
	public int ItemsTo { get; set; }
}
