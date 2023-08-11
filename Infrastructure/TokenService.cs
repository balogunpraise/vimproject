using Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure
{
    public class TokenService
	{
		private readonly IConfiguration _config;
		private readonly SymmetricSecurityKey _key;
		private readonly string envKey;

		public TokenService(IConfiguration config)
		{
			_config = config;
			envKey = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ? _config["Token:Key"] : Environment.GetEnvironmentVariable("JWT_TOKEN_KEY");
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(envKey));
		}

		public string GenerateToken(ApplicationUser user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Role, user.FirstName)
			};

			var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				SigningCredentials = creds,
				Expires = DateTime.UtcNow.AddDays(7),
				Issuer = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ? _config["Token:Issuer"] : Environment.GetEnvironmentVariable("APP_DOMAIN"),
				IssuedAt = DateTime.UtcNow
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}
