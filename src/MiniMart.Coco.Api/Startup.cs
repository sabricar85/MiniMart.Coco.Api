using System;
using System.Configuration;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.Options;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
 
using Microsoft.OpenApi.Models;
using MiniMart.Coco.Api.Data;
using MiniMart.Coco.Api.Repository;
 

namespace MiniMart.Coco.Api
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();
            services.AddScoped<IVirtualCartRepository, VirtualCartRepository>();
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();  
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));
           
            services.AddSwaggerGen(c =>
            {
              
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MiniMart API",
                    Version = "v1",
                    Description = "store COCO ",
      
                });
                c.IncludeXmlComments(string.Format(@"{0}MiniMart.Coco.Api.xml",
                                        System.AppDomain.CurrentDomain.BaseDirectory));

            });

            services
               .AddAutoMapper()
             //  .AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
             .AddMediatR(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
             
            }

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "COCO MiniMart API V1");
                c.RoutePrefix = string.Empty;
                c.EnableFilter();
               
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
