using System;
using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using Split.Application;
using Split.Infrastructure;

namespace Split.WebApi
{
    public static class Program
    {
        private static readonly string BinariesPath = Path.GetDirectoryName(typeof(Program).Assembly.Location);

        public static void Main(string[] args)
        {
            try
            {
                var logger = CreateNLogLogger();
                logger.Info("Create Split host and run...");

                CreateHostBuilder(args)
                    .Build()
                    .Run();
                
                logger.Info("Split host terminated.");
            }
            catch (Exception ex)
            {
                // Inform on console, logger might be dead by now
                Console.WriteLine("Split Host stopped because of exception!");
                Console.WriteLine(ex.ToString());

                new NLogLoggerProvider()
                    .CreateLogger(typeof(Program).FullName)
                    .LogError(ex, "Split Host stopped because of exception!");
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads
                // before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
        }

        private static Logger CreateNLogLogger()
        {
            var configPath = Path.Combine(BinariesPath, "nlog.config");
            var log = NLogBuilder.ConfigureNLog(configPath).GetCurrentClassLogger();

            return log;
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseContentRoot(BinariesPath)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices((context, services) =>
                {
                    services
                        .AddApplicationServices()
                        .AddPersistenceServices(context.Configuration);
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    logging.AddNLog();
                })
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}