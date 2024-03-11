namespace iFolor.StudentManager.Core.Contracts.Infrastructure;

/// <summary>
/// Resets the data.
/// </summary>
public interface IDataResetter
{
    /// <summary>
    /// Resets data using the backup file defined in configuration.
    /// </summary>
    void Reset();
}
