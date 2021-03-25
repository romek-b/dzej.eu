using DzejEu.Api.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace DzejEu.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices(services =>
                {
                    services.AddHostedService<UpdateStreamsService>();
                })
                .ConfigureLogging(builder =>
                {
                    builder.AddSimpleConsole(opts =>
                    {
                        opts.SingleLine = true;
                        opts.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
                        opts.UseUtcTimestamp = true;
                        opts.ColorBehavior = LoggerColorBehavior.Enabled;
                    });
                });
    }
}
