using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace XPlaneDotNetCoreWebAPI
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
            services.AddCors(options =>
            options.AddDefaultPolicy(builder =>
            builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            // configure DI for application services
            services.AddSwaggerGen(c =>
            {
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
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });
                c.OperationFilter<AuthenticationRequirementsOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHsts();

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        public class AuthenticationRequirementsOperationFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                if (operation.Security == null)
                    operation.Security = new List<OpenApiSecurityRequirement>();


                var scheme = new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" } };
                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    [scheme] = new List<string>()
                });
            }
        }


    }
}
