using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsersAPI.Domain.Interfaces.Security;
using UsersAPI.Domain.ValueObjects;
using UsersAPI.Infra.Security.Settings;

namespace UsersAPI.Infra.Security.Services
{
    public class TokenService: ITokenService
    {
        private readonly TokenSettings _settings;

        public TokenService(IOptions<TokenSettings> settings)
        {
            _settings = settings.Value;
        }

        public string CreateToke(UserAuthVO userAuth)
        {
            //Definir as CLAIMS que serão gravadas no token
            //CLAIMS -> Identificações para o usuário
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, JsonConvert.SerializeObject(userAuth)),
                new Claim(ClaimTypes.Role, userAuth.Role),
            };

            //gerando a assinatura antifalsificação do token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings?.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //preenchendo as informações do token
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _settings?.Issuer,
                audience: _settings?.Audience,
                claims: claims, //informações de identificação do usuário
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_settings?.ExpirationInMinutes)),
                signingCredentials: credentials //assinatura do token
                );

            //retornando o token
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(jwtSecurityToken);

        }
    }
}
