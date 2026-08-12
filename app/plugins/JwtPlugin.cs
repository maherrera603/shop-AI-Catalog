using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

using Catalog.Api.app.domain.error;

namespace Catalog.Api.app.plugins
{

    public class JwtPlugin {
        private readonly IConfiguration _configuration;

        public JwtPlugin(IConfiguration configuration){
            this._configuration = configuration;
        }

        public ClaimsPrincipal ValidateToken(string token){
            var secret = this._configuration["Jwt:Secret"] ?? throw CustomError.InternalServer("la clave JWT no esta configurado.");


            var issuer = this._configuration["Jwt:Issuer"] ?? throw CustomError.InternalServer("El Issuer JWT no esta configurado.");

            var audience = this._configuration["Jwt:Audience"] ?? throw CustomError.InternalServer("El Audience JWT no esta configurado.");


            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters{
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(secret)),

                ValidateIssuer = true,
                ValidIssuer = issuer,

                ValidateAudience = false,
                ValidAudience = audience,

                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            try
            {
                ClaimsPrincipal principal = tokenHandler.ValidateToken(
                    token,
                    validationParameters,
                    out SecurityToken validatedToken
                );

                if(validatedToken is not JwtSecurityToken jwtToken) throw CustomError.Unauthorized("Token invalido.");

                var tokenType = principal.FindFirst("type")?.Value;

                if(tokenType != "ACCESS") throw CustomError.Unauthorized("El token no es valido para esta operacion.");

                return principal;
            }
            catch(SecurityTokenException){
                throw CustomError.Unauthorized("Token invalido o expirado.");
            }
            catch
            {
                    
                throw CustomError.Unauthorized("El token no es valido para esta operacion.");
            }
        }

    }
    
}
