using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace CityInfo.API
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc()
        .AddMvcOptions(o => o.OutputFormatters.Add(
          new XmlDataContractSerializerOutputFormatter()));
      //.AddJsonOptions(o =>
      //{
      //  if (o.SerializerSettings.ContractResolver is DefaultContractResolver castedResolver)
      //  {
      //    castedResolver.NamingStrategy = null;
      //  }
      //});
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
      loggerFactory.AddConsole();
      loggerFactory.AddDebug();

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler();
      }

      app.UseStatusCodePages(); // add this will show status code text like 404 on the browser page. or user needs to open developer tools to see this
      app.UseMvc();

      //app.Run((context) =>
      //{
      //  throw new Exception("something bad happened");
      //});
      //app.Run(async (context) =>
      //{
      //  await context.Response.WriteAsync("Hello World!");
      //});
    }
  }
}
