using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {
            //Defines the key with which we will be signing JWTs
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(AppUser user)
        {
            //Defines the claims that will be in our JWT (ie. certain info about user)
            var claims = new List<Claim> 
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            //Storing credentials to sign the token with
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //Defines the description of our token (in other words, some essential properties of our token)
            var tokenDescriptor = new SecurityTokenDescriptor() 
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            //Initializes instance of JWTSecurityTokenHandler ([Cmnd + L-Click] on signature for more info about handler)
            var tokenHandler = new JwtSecurityTokenHandler();

            //Creates the JSON Web Token & assigns to var token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //Serializes JWT Security Token into compact JWT format & returns ready-to-use JWT
            return tokenHandler.WriteToken(token);

        }
    }
}