using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Services;
using API.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Extensions;
using API.Middleware;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Application services are located in the ApplicationServiceExtensions.cs file (Extensions folder)
            services.AddApplicationServices(_config);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            services.AddCors();

            //Application identity services located in the IdentityServiceExtensions.cs file (Extensions folder)
            services.AddIdentityServices(_config);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleware>();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //variable "x" stands for "policy" (ie. policy.AllowAnyHeader(), etc...)
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}


// Example Dependency Injection //
/*
//Singleton == 1 instance of service per application lifetime.
~~ services.AddSingleton<IInterfaceClass, ImplementationClass>();

//Scoped == 1 instance of service per lifetime of an HTTP request. 
~~ services.AddScoped<IInterfaceClass, ImplementationClass>();

//Transient == 1 instance of service, per lifetime of each dependency using the service, of/per each HTTP request. 
~~ services.AddTransient<...>();

//Adds an item to ICollection<T> where "item" is a variable that stores a ServiceDescriptor. 
//In other words, ICollection<ServiceDescriptor>.Add(ServiceDescriptor item).
~~ services.Add(item);
*/