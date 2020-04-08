using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Pandemia.Application.Services;
using Pandemia.Infrastructure.ExternalServices;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using Pandemia.Application.Repositories;
using Pandemia.Domain.ValueObjects;
using Pandemia.Persistance.Repositories;

namespace Pandemia.WebApi.StartupExtentions
{
  public static class ServicesCollectionExtentions
  {


    public static IServiceCollection AddExternalService(this IServiceCollection services)
    {
      return services
        .AddScoped<IPandemiaDataService, CSSEGISandDataService>()
        .AddScoped<ICountryService, CountryService>()
        .AddScoped<IRepository<Country>, CountryRepository>();
    }


    public static IServiceCollection ConfigureAndValidate<T>(this IServiceCollection services,
          IConfiguration config) where T : class
    {
      return services
        .Configure<T>(config.GetSection(typeof(T).Name))
        .PostConfigure<T>(settings =>
        {
          ValidationContext context = new ValidationContext(settings, null, null);
          var results = new List<ValidationResult>();
          Validator.TryValidateObject(settings, context, results, true);

          var configErrors = results.Select(e => e.ErrorMessage).ToArray();
          if (configErrors.Any())
          {
            string aggrErrors = string.Join(",", configErrors);
            var count = configErrors.Length;
            var configType = typeof(T).Name;
            throw new ConfigurationErrorsException(
              $"Found {count} configuration error(s) in {configType}: {aggrErrors}");
          }
        });
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
      services.ConfigureSwaggerGen(
        options =>
        {
          options.SwaggerDoc("v1", new OpenApiInfo
          {
            Title = "Pandemia API",
            Version = "v1",
            Description = "Pandemia data",
            Contact = new OpenApiContact()
            {
              Name = "Kemo"
            }
          });
          //var basePath = PlatformServices.Default.Application.ApplicationBasePath;
          //var xmlPath = Path.Combine(basePath, "swaggerdoc.xml");
          //options.IncludeXmlComments(xmlPath);
          options.CustomSchemaIds(x => x.FullName);
        });
      services.AddSwaggerGen();

      return services;
    }
  }
}
