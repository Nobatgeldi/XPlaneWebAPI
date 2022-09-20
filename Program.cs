using Microsoft.AspNetCore;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace XPlaneDotNetCoreWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                options.ExcludedHosts.Add("covisart.com");
                options.ExcludedHosts.Add("xplane.covisart.com");
            });

            builder.Services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
                options.HttpsPort = 7208;
            });

            builder.Services.AddCors(options =>
                    options.AddDefaultPolicy(build =>
                    build.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
            ));

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
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Listen(IPAddress.Any, 7208,
                            listenOptions =>
                            {
                                listenOptions.UseHttps("myssl.pfx", "45548598");
                            });
            });

            var app = builder.Build();

            app.UseExceptionHandler("/Error");
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseCors();
            app.UseRouting();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();
            app.UseAuthentication();

            //app.MapControllers();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Run();
        }
    }
}