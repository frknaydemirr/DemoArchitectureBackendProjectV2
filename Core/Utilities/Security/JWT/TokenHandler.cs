using Microsoft.Extensions.Configuration;

using Entities.Concrete;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Claims;
using Core.Extensions;

namespace Core.Utilities.Security.JWT
{
    public class TokenHandler : ITokenHandler
    {
        IConfiguration Configuration;

        public TokenHandler(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public Token CreateToken(User user, List<OperationClaim> operationClaims)
        {
           Token token = new Token();

            //Security Key'in simetriğini alalım:
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));


            //Şifrelenmiş kimliği oluşturuyoruz:

            SigningCredentials signingCredentials =
    new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);



            //Token Ayarları.

            token.Expiration = DateTime.Now.AddMinutes(60);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: token.Expiration,
                claims: SetClaims(user,operationClaims),
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials
                );

            //Token oluşturucu sınıfından bir örnek alalım:

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            //Token üretelim.

            token.AccessToken = jwtSecurityTokenHandler.WriteToken(securityToken);

            //Refresh token üretelim
            token.RefreshToken = CreateRefreshToken();

            return token;

 
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using (RandomNumberGenerator random = RandomNumberGenerator.Create())
            {
                random.GetBytes(number);
                return Convert.ToBase64String(number);
            }
        }

        //claim in içersine veriler atayacağız:
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var  claims = new List<Claim>();
            claims.AddName(user.Name);
            claims.AddRoles(operationClaims.Select(p => p.Name).ToArray());
            return claims; //LİSTEYİ döndürdük;
        }
    }
}
