using iFolor.StudentManager.Infrastructure.Configuration;

namespace iFolor.StudentManager.Windows.Configuration;

/// <summary>
/// Application settings.
/// </summary>
public class AppSettings
{
    /// <summary>
    /// Name of the application.
    /// </summary>
    public required string ApplicationName { get; set; }

    /// <summary>
    /// Data source configuration.
    /// </summary>
    public required DataSource DataSource { get; set; }
}
