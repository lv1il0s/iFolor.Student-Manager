using System.IO;
using System.Web;

using iFolor.StudentManager.Core.Configuration;
using iFolor.StudentManager.Core.Contracts.Infrastructure;
using iFolor.StudentManager.Core.Contracts.UI;
using iFolor.StudentManager.Core.Helpers;
using iFolor.StudentManager.Infrastructure.Configuration;
using iFolor.StudentManager.Infrastructure.Contracts.Services;
using iFolor.StudentManager.Infrastructure.Repositories;
using iFolor.StudentManager.Infrastructure.Services;
using iFolor.StudentManager.Windows.ViewModels;
using iFolor.StudentManager.Windows.Views.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Prism.Events;

using Serilog;

namespace iFolor.StudentManager.Windows.Configuration;

/// <summary>
/// Extension methods for the <see cref="IServiceCollection"/>.
/// </summary>
internal static class ServiceCollectionExtensions
{
    private static string _appSettingsFileName = "appsettings";
    private static string _appSettingsFileExtension = "json";
    internal static void ConfigureServices(this IServiceCollection services)
    {
        ConfigureSettings(services);
        ConfigureLogging(services);
        ConfigureAppServices(services);
    }

    /// <summary>
    /// Configures AppSettings and adds it to the service collection.
    /// </summary>
    /// <param name="services"></param>
    private static void ConfigureSettings(IServiceCollection services)
    {
        var builder = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
         .AddJsonFile($"{_appSettingsFileName}.{_appSettingsFileExtension}", optional: false);

        var environment = Environment.GetEnvironmentVariable(Constants.Configuration.EnvironmentVariables.ENVIRONMENT_VARIABLE)
            ?? Constants.Configuration.Environment.DEVELOPMENT;
        builder.AddJsonFile($"appsettings.{environment}.json", optional: true);

        var configuration = builder.Build();

        var appSettings = configuration.GetSection("AppSettings").Get<AppSettings>();

        if (appSettings is null)
        {
            Log.Logger.Error("Failed to load app settings");
            return;
        }

        services.AddSingleton(appSettings);
        services.AddSingleton(appSettings.DataSource);
    }

    private static void ConfigureLogging(IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs/StudentManager.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        services.AddLogging(config =>
        {
            config.ClearProviders();
            config.AddSerilog();
        });

        Log.Information("Application Starting");
    }

    /// <summary>
    /// Registers all services and view models to the service collection.
    /// </summary>
    /// <param name="services"></param>
    private static void ConfigureAppServices(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();

        services.AddSingleton<IEventAggregator, EventAggregator>();

        services.AddCoreServices();

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<StudentItemViewModel>();
        services.AddSingleton<StudentsViewModel>();
        services.AddSingleton<EmptyViewModel>();

        services.AddSingleton(typeof(IXmlFileClient<>), typeof(XmlFileClient<>));
        services.AddSingleton<StudentRepository>();
        services.AddSingleton<IStudentRepository, StudentRepository>(x => x.GetRequiredService<StudentRepository>());
        services.AddSingleton<IDataResetter, StudentRepository>(x => x.GetRequiredService<StudentRepository>());

        services.AddSingleton<IDialogService, DialogService>();

        MappingConfiguration.RegisterMappings();
    }
}
