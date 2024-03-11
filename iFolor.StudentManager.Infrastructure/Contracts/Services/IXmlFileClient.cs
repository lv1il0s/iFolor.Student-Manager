namespace iFolor.StudentManager.Infrastructure.Contracts.Services;

/// <summary>
/// Interface for a client that can read and write data to an XML file.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IXmlFileClient<T> where T : class
{
    /// <summary>
    /// Loads and serializes the data from the file.
    /// </summary>
    /// <returns></returns>
    T? LoadData();

    /// <summary>
    /// Resets the data in the working file from the backup file.
    /// </summary>
    void Reset();

    /// <summary>
    /// Persists data passed as parameter to the working file.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    string SaveChanges(T item);
}