using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace VenueAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)            
            .UseUrls("http://*:52000", "https://*:52001")
            .UseStartup<Startup>();
    }
}
