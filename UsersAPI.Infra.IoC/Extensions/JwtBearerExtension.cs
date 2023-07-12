using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UsersAPI.Domain.Interfaces.Security;
using UsersAPI.Infra.Security.Services;
using UsersAPI.Infra.Security.Settings;

namespace UsersAPI.Infra.IoC.Extensions
{
    public static class JwtBearerExtension
    {
        public static IServiceCollection AddJwtBearer(this IServiceCollection services, IConfiguration configuration)
        {
            //lendo configurações do /appsettings.json
            var issuer = configuration.GetSection("TokenSettings:Issuer").Value;
            var audience = configuration.GetSection("TokenSettings:Audience").Value;
            var secretKey = configuration.GetSection("TokenSettings:SecretKey").Value;
            var expirationInMinutes = int.Parse(configuration.GetSection("TokenSettings:ExpirationInMinutes").Value);

            //definindo a politica de autenticação
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, //emissor do token
                    ValidateAudience = true, // validar o destinatario do token
                    ValidateLifetime = true, // validar tempo da aplicação
                    ValidateIssuerSigningKey = true, // validar a chave secreta utilizada pelo emissor do token

                    ValidIssuer = issuer, //nome do emissor do token
                    ValidAudience = audience, //cliente do token
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)) //chave

                };
            });

            services.Configure<TokenSettings>(configuration.GetSection("TokenSettings"));
            services.AddTransient<ITokenService, TokenService>();


            return services;
        }
    }
}
