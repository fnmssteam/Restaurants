using Restaurants.API.Extensions;
using Restaurants.API.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;

try
{
	var builder = WebApplication.CreateBuilder(args);

	// Add services to the container.

	builder.AddPresentation();

	builder.Services.AddInfrastructure(builder.Configuration);
	builder.Services.AddApplication();

	var app = builder.Build();

	var scope = app.Services.CreateScope();
	var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

	await seeder.Seed();

	// Configure the HTTP request pipeline. Note: order is important
	app.UseMiddleware<ErrorHandlingMiddleware>();
	app.UseMiddleware<RequestTimeLoggingMiddleware>();

	app.UseSerilogRequestLogging();

	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseHttpsRedirection();

	// Register, login, etc.
	app
		.MapGroup("api/identity") // Add a prefix to Identity routes
		.WithTags("Identity") // Place the endpoints under the Identity section
		.MapIdentityApi<User>();

	app.UseAuthorization();

	app.MapControllers();

	app.Run();
}
catch (Exception ex)
{
	Log.Fatal(ex, "Application startup failed.");
}
finally
{
	Log.CloseAndFlush();
}

public partial class Program { }