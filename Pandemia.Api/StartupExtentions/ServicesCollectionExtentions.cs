using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Pandemia.Application.Services;
using Pandemia.Infrastructure.ExternalServices;

namespace Pandemia.WebApi.StartupExtentions
{
  public static class ServicesCollectionExtentions
  {


    public static IServiceCollection AddExternalService(this IServiceCollection services)
    {
      return services
        .AddScoped<IPandemiaDataService, RetrieveRawDataFromCSSEGISandData>();
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
