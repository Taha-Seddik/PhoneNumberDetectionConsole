using Microsoft.Extensions.DependencyInjection;
using TechnicalTest.Services;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        using var serviceProvider = services.BuildServiceProvider();

        var app = serviceProvider.GetService<App>();
        app.Run(args);
    }

    static void ConfigureServices(IServiceCollection services)
    {
        // Register services here
        services.AddSingleton<App>();
        services.AddSingleton<IPhoneNumberParserService, PhoneNumberParserService>(); 
    }
}
