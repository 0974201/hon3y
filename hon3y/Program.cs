using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using hon3y.Data;
using Serilog;

namespace hon3y
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //logger aanmaken, schrijft logs naar txt bestanden in de log map, wordt per uur een nieuw txt file aangemaakt
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Hour)
                .CreateLogger();

            Log.Information("Opstarten...");

            try
            {

                Log.Information("Testen of alles naar behoren werkt!");

                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var configuration = services.GetRequiredService<IConfiguration>();

                    //Initialiseer de databases

                    DbInit dbinit = new DbInit(configuration);
                    dbinit.CreateDatabase(); //maakt database voor de webapplicatie aan
                    dbinit.CreateTables(); //maakt de tabellen voor de database aan
                    dbinit.CreateLogsDatabase(); //maakt de database voor de logs aan
                    dbinit.CreateLogDBTable(); //maakt de tabel voor de log database aan
                }

                host.Run();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Er ging iets mis!");
            }
            finally 
            {
                Log.CloseAndFlush(); //reset de logger
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSerilog();
                });
    }
}
