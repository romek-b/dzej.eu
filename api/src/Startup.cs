using DzejEu.Api.Clients.Database;
using DzejEu.Api.Clients.Twitch;
using DzejEu.Api.Models;
using DzejEu.Api.Models.Database.Settings;
using DzejEu.Api.Models.Twitch.Settings;
using DzejEu.Api.Providers.Twitch;
using DzejEu.Api.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DzejEu.Api
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
            services.Configure<TwitchAppSettings>(Configuration.GetSection("TwitchAppSettings"));
            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddMemoryCache();
            services.AddSingleton<DatabaseClient>();
            services.AddTransient<IStreamsRepository, StreamsRepository>();

            services.AddHttpClient<ITwitchAuthClient, TwitchAuthClient>();
            services.AddTransient<ITwitchTokenProvider, TwitchTokenProvider>();
            services.AddHttpClient<ITwitchClient, TwitchClient>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DzejEu.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DzejEu.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
