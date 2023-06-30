using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace UsersAPI.Services.Extensions
{
    public static class JwtBearerExtension
    {
        public static IServiceCollection AddJwtBearer(this IServiceCollection services)
        {
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
                    ValidateIssuerSigningKey = true // validar a chave secreta utilizada pelo emissor do token
                };
            });

            return services;
        }
    }
}
