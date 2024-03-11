using System.Windows;

using iFolor.StudentManager.Windows.Configuration;

using Microsoft.Extensions.DependencyInjection;

using Serilog;

namespace iFolor.StudentManager.Windows;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        ServiceCollection services = new();
        services.ConfigureServices();
        _serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = _serviceProvider.GetService<MainWindow>();
        mainWindow?.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        Log.CloseAndFlush();
        base.OnExit(e);
    }

    private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Exception Message", MessageBoxButton.OK, MessageBoxImage.Error);
        Log.Logger.Fatal(e.Exception, "An unhandled exception just occurred");
        e.Handled = true;
    }

    //TODO: Add Ranorex UI tests
    //TODO: Add integration tests covering interaction with file system
    //TODO: Improve logging
    //TODO: Add more Try/catch blocks
    //TODO: Implement Edit/Cancel/Save commands (change-tracking, caching)
    //TODO: Maybe - implement logic to show which items are affected
    //TODO: Custom numeric box
    //Done: Fix Azure pipeline (CI)
    //TODO: Add CD 
    //TODO: Add MSIX installer
    //TODO: Add App Center SDK (instrument app for activity tracking)
    //TODO: Add some database via docker and allow persisting there
    //TODO: Add solution-
}
