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
			//option.UseMySql(connectionString, serverVersion: ServerVersion.AutoDetect(connectionString)));
			option.UseNpgsql(connectionString));
			service.AddTransient<TokenService>();
			service.AddScoped<ICourseRepository, CourseRepository>();
			service.AddScoped<IStudentRepository, StudentRepository>();
			service.AddScoped<IMusicScoreRepository, MusicScoreRepository>();
			service.AddScoped<IVideoRepository, VideoRepository>();
			return service;
		}

        private static string GetHerokuConnectionString()
        {
            string? connectionUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            var databaseUri =  new Uri(connectionUrl);

            string db = databaseUri.LocalPath.TrimStart('/');
            string[] userInfo = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);

            return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
        }
    }
}
