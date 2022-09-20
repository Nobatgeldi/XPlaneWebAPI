using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using XPlaneConnector;
using XPlaneConnector.DataRefs;

namespace XPlaneDotNetCoreWebAPI
{
    public class Program
    {

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel(options =>
            {
                options.Listen(IPAddress.Loopback, 7208);
                options.Listen(IPAddress.Loopback, 5208, listenOptions =>
                {
                    listenOptions.UseHttps("myssl.pfx", "45548598");
                });
            })
            .UseStartup<Startup>();
        /*public static void Main(string[] args) {

            CreateWebHostBuilder(args).Build().Run();

        }*/
        public static void Main(string[] args)
        {
            

            Console.WriteLine(Path.Combine(Directory.GetCurrentDirectory(), "myssl.pfx"));

            var clientCertificate =
            new System.Security.Cryptography.X509Certificates.X509Certificate2(
              Path.Combine(Directory.GetCurrentDirectory(), "myssl.pfx"), "45548598");

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient("namedClient", c =>{})
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    var handler = new HttpClientHandler();
                    handler.ClientCertificates.Add(clientCertificate);
                    return handler;
                });
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddAuthentication(
                CertificateAuthenticationDefaults.AuthenticationScheme)
            .AddCertificate(options =>
            {
                options.Events = new CertificateAuthenticationEvents
                {
                    OnCertificateValidated = context =>
                    {
                        var claims = new[]
                        {
                            new Claim(
                                ClaimTypes.NameIdentifier,
                                context.ClientCertificate.Subject,
                                ClaimValueTypes.String, context.Options.ClaimsIssuer),
                            new Claim(
                                ClaimTypes.Name,
                                context.ClientCertificate.Subject,
                                ClaimValueTypes.String, context.Options.ClaimsIssuer)
                        };

                        context.Principal = new ClaimsPrincipal(
                            new ClaimsIdentity(claims, context.Scheme.Name));
                        context.Success();

                        return Task.CompletedTask;
                    }
                };
            });

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
            
            var app = builder.Build();

            /*// Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }*/

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllers();

            app.Run();
        }
    }
}