using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pandemia.Infrastructure.ExternalServices;
using Pandemia.Persistance;
using Pandemia.Persistance.Mappings;
using Pandemia.WebApi.StartupExtentions;
using System.Reflection;

namespace Pandemia.Api
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


      services.ConfigureAndValidate<CSSEGISandDataServiceSettings>(Configuration);
      services.ConfigureAndValidate<PersistenceSettings>(Configuration);
      services.AddAutoMapper(typeof(CountryProfile).GetTypeInfo().Assembly);
      services.AddControllers();
      services.AddExternalService();
      services.AddSwagger();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pandemia API"));
    }
  }
}
