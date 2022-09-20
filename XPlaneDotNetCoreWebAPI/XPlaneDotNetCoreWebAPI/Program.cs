using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Reflection;
using XPlaneConnector;
using XPlaneConnector.DataRefs;

namespace XPlaneDotNetCoreWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "X-Plane 11/12 DataRef .NetCore WebAPI v1",
                    Version = "v1.2",
                    Description = "The API to perform Dataref connection to X-Plane 11/12",
                    TermsOfService = new Uri("https://covisart.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Covisart R&D Company",
                        Email = "dev@covisart.com",
                        Url = new Uri("https://covisart.com/contact"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "COVISART NGS Simulator API LICX",
                        Url = new Uri("https://covisart.com/license"),
                    },
                    Extensions = new Dictionary<string, IOpenApiExtension>()

                });
            });
            /*builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ConfigureEndpointDefaults(listenOptions =>
                {
                    serverOptions.Listen(IPAddress.Any, 8443);
                });
            });*/
            var app = builder.Build();
            /*// Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }*/
            
            app.UseSwagger();
            app.UseSwaggerUI();

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}