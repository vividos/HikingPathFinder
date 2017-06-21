using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace HikingPathFinder.Backend.WebApi
{
    /// <summary>
    /// Program class for the Web API project
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main function for the program
        /// </summary>
        /// <param name="args">arguments (unused)</param>
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
