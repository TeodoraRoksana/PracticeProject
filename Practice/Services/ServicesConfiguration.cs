using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Practice.Services
{
    public static class ServicesConfiguration
    {
        public static void AddJWTAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://*:7001",
                    ValidAudience = "http://*:7001",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("JWTTokenSecretKey_JWTTokenSecretKey"))
                };
            });
        }
    }
}
