using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Hodgepodge.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            //using (var serviceScope = host.Services.CreateScope())
            //{
            //    var dbInitializer = serviceScope
            //        .ServiceProvider
            //        .GetRequiredService<IDbInitializer>();

            //    dbInitializer.InitializeAsync().GetAwaiter().GetResult();
            //    dbInitializer.SeedAsync().GetAwaiter().GetResult();
            //}

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseStartup<Startup>()
                .Build();
    }
}
