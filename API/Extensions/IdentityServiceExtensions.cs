using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config) 
        {
            //Injection of Bearer JWT authentication service (Microsoft.AspNetCore.Authentication.JwtBearer)
            //Allows server to authenticate HTTP requests with JWTs provided in Authorization header
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters 
                    {
                        //Asks the server to validate the token (in somewhat oversimplified terms)
                        ValidateIssuerSigningKey = true,
                        //Provides the server with the key used to sign the JWT
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                        //Issuer is our API
                        ValidateIssuer = false,
                        //Audience is the client Angular app
                        ValidateAudience = false
                    };
                });

            return services;
        }
    }
}