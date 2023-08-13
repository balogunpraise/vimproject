using Core.Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddInfrastructureConfiguration(this IServiceCollection service, IConfiguration config)
		{
			var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
			var connectionString = isDevelopment ? config.GetConnectionString("DefaultConnection") : Environment.GetEnvironmentVariable("CONNECTION_STRING");
			service.AddDbContext<ApplicationDbContext>(option =>
			option.UseNpgsql(connectionString));
			service.AddTransient<TokenService>();
			service.AddScoped<ICourseRepository, CourseRepository>();
			service.AddScoped<IStudentRepository, StudentRepository>();
			service.AddScoped<IMusicScoreRepository, MusicScoreRepository>();
			service.AddScoped<IVideoRepository, VideoRepository>();
			return service;
		}

    }
}
