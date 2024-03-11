namespace iFolor.StudentManager.Infrastructure.Configuration;

/// <summary>
/// Data source configuration.
/// </summary>
public class DataSource
{
    /// <summary>
    /// Folder where the data files are stored.
    /// </summary>
    public required string FolderPath { get; set; }

    /// <summary>
    /// Working file used by the app.
    /// </summary>
    public required string WorkingFile { get; set; }

    /// <summary>
    /// Original file used as a backup.
    /// </summary>
    public required string BackupFile { get; set; }
}