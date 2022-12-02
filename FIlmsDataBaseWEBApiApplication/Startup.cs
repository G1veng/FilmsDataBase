using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FIlmsDataBaseWEBApiApplication.Infrastructure;
using FIlmsDataBaseWEBApiApplication.Data;
using Microsoft.AspNetCore.Routing;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http;
using System.Web;
using Microsoft.AspNetCore.Http.Features;
using FIlmsDataBaseWEBApiApplication.Services;  
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace FIlmsDataBaseWEBApiApplication
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
      services.AddRazorPages();
      services.AddControllers();
      services.AddTransient<IEFFilmREpository, EFFilmRepository>();
      services.AddDbContext<EFFilmsDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseStaticFiles();
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
        endpoints.MapRazorPages();
      });
      app.Use(async (context, next) =>
      {
        context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = null; // unlimited I guess
        await next.Invoke();
      });
    }
  }
}
