using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Project.Repository.Data;

namespace Project.WebApi
{
    /// <summary>
    /// The main class of the solution
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method of the solution
        /// </summary>
        /// <param name="args">string[] args</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// A method to enable some more configurations for the project. Like Database initializer.
        /// </summary>
        /// <param name="args">string[] args</param>
        /// <returns>A WebHost</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            DbInitializer.Initialize();

            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        }
    }
}
