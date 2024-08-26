using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;
using Serilog;

namespace Restaurants.API.Extensions;

public static class WebApplicationBuilderExtensions
{
	public static void AddPresentation(this WebApplicationBuilder builder)
	{
		builder.Services.AddAuthentication();
		builder.Services.AddControllers();
		builder.Services.AddSwaggerGen(c =>
		{
			// Add security definition for the bearer token in Swagger
			c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
			{
				Type = SecuritySchemeType.Http,
				Scheme = "Bearer"
			});

			// Inject the token to each requests in Swagger
			c.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth"}
					},
					[]
				}
			});
		});

		// Exposes Identity endpoints on Swagger
		builder.Services.AddEndpointsApiExplorer();


		builder.Services.AddScoped<ErrorHandlingMiddleware>();
		builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

		builder.Host.UseSerilog((context, configuration) =>
			configuration.ReadFrom.Configuration(context.Configuration)
		);
	}
}
