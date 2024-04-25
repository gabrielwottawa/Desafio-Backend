using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

namespace MotorbikeRental.Configurations
{
    public static class SwaggerConfiguration
    {
        public static WebApplicationBuilder AddSwaggerConfiguration(this WebApplicationBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.CustomSchemaIds((x) =>
                {
                    try
                    {
                        var attribute = x.GetCustomAttributes<DisplayNameAttribute>().SingleOrDefault();
                        return attribute == null ? x.Name : attribute.DisplayName;
                    }
                    catch
                    {
                        return x.Name;
                    }
                });

                swagger.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "Motorbike Rental",
                    Description = "Motorbike Rental API para gerenciamento de aluguel de motos e entregadores.",
                    Contact = new OpenApiContact
                    {
                        Name = "Gabriel Wottawa",
                        Email = "gabrielclerio10@gmail.com"
                    }
                });

                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."

                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
                });

                SetXmlDocumentation(swagger);
            });

            return builder;
        }

        private static void SetXmlDocumentation(SwaggerGenOptions options)
        {
            var xmlDocumentPath = GetXmlDocumentPath();
            var existsXmlDocument = File.Exists(xmlDocumentPath);

            if (existsXmlDocument)
                options.IncludeXmlComments(xmlDocumentPath);
        }

        private static string GetXmlDocumentPath()
        {
            var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;
            var applicationName = PlatformServices.Default.Application.ApplicationName;
            return Path.Combine(applicationBasePath, $"{applicationName}.xml");
        }
    }
}