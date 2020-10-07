using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleAPI.Context;

namespace SimpleAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var host = CreateWebHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    try
                    {
                        var context = scope.ServiceProvider.GetService<SimpleApiContext>();

                        // for demo purposes, delete the database & migrate on startup so 
                        // we can start with a clean slate
                        context.Database.EnsureDeleted();
                        context.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        throw;
                        //logger.Error(ex, "An error occurred while migrating the database.");
                    }
                }

                host.Run();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
