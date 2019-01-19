using IDResolver.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IDResolver
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.Converters.Add(new StringEnumConverter(true));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var redisHost = "SET_PRODUCTION_HOST_HERE";
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                redisHost = "localhost";
            }
            
            RedisDatabase.Initialize(redisHost);

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "resolve", template: "/resolve/{id}",
                    defaults: new { controller = "Resolve", action = "ResolveId"});
                routes.MapRoute(name: "id", template: "/id/",
                    defaults: new { controller = "Id", action = "PostId"});
            });
        }
    }
}