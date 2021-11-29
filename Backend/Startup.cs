using Backend.Infrastructure.Data;
using Backend.Core.Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using Backend.Controllers;
using Backend.Infrastructure.Data.Repositories.Interfaces;
using Backend.Infrastructure.Data.Repositories;
using System.Reflection;
using System.IO;

namespace Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton((x) =>
            {
                var smtpConfiguration = new SmtpConfiguration();
                Configuration.GetSection("Smtp").Bind(smtpConfiguration);
                return smtpConfiguration;
            });
            services.AddScoped<MailClient>();

            services.AddSingleton(provider => Configuration);
            services.AddSingleton<LicenseGenerator>();

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                c.AddPolicy("AllowExposedXTotalCount", options => options.AllowAnyHeader().WithExposedHeaders("Access-Control-Expose-Headers"));
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySQL(
                Configuration.GetConnectionString("DefaultConnection")));

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OneBlinq API - V1", Version = "v1" });
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "OneBlinq API - V2", Version = "v2" });
                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authenthication for API",

                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.OperationFilter<RemoveVersionFromParameter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPath>();
                c.DocInclusionPredicate(DocInclusionPredicates.FilterByApiVersion);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            //services.AddAuthentication("BasicAuthentication")
            //    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            services.AddScoped<ILicenceRepository, LicenceRepository>();
            services.AddScoped<IPluginRepository, PluginRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            System.Console.WriteLine("Applying Migrations....");
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate();
            }

            if (env.IsDevelopment() || env.IsEnvironment("local"))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OneBlinq API - v1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "OneBlinq API - v2");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
