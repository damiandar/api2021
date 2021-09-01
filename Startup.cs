using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clase2DatabaseFirst.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Clase2DatabaseFirst
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "Clase2DatabaseFirst", 
                    Version = "v1",
                    Description = "Materia: Programacion WEB 2021 2do cuatrimestre",
                    TermsOfService = new Uri("https://istea.edu.ar"),
                    Contact = new OpenApiContact
                    {
                        Name = "Damian Rosso",
                        Email = "drosso@istea.com.ar",
                        Url = new Uri("https://istea.edu.ar"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "GNU",
                        Url = new Uri("https://istea.edu.ar"),
                    }
                });
            });
            services.AddDbContext<ComercioDbContext> (o => o.UseSqlServer(Configuration.GetConnectionString("ComercioCS")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clase2DatabaseFirst v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
