using System;
using System.IO;
using ExceptionManager.Interfaces;
using ExceptionManager.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExceptionManager
{
    class Program
    {
        static void Main()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            serviceProvider.GetService<App>()?.Run();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            services.AddSingleton(configuration);

            var managerOptions = configuration
                .GetSection("ExceptionManager")
                .Get<ExceptionManagerOptions>();

            services.AddSingleton(managerOptions);

            services.AddScoped<IExceptionManager, Implementations.ExceptionManager>();
            services.AddScoped<IServerClient, DummyServerClient>();

            services.AddSingleton<App>();
        }
    }
}
