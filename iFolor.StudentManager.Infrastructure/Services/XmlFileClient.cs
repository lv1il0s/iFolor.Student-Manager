using System.Xml.Serialization;

using Ardalis.GuardClauses;
using iFolor.StudentManager.Infrastructure.Configuration;
using iFolor.StudentManager.Infrastructure.Contracts.Services;
using Microsoft.Extensions.Logging;

namespace iFolor.StudentManager.Infrastructure.Services;

/// <inheritdoc/>
public class XmlFileClient<T> : IXmlFileClient<T> where T : class
{
    private readonly DataSource _dataSource;
    private string _backupFilePath => Path.Combine(Environment.CurrentDirectory,
                                                   _dataSource.FolderPath,
                                                   _dataSource.BackupFile);

    private string _workingFilePath => Path.Combine(Environment.CurrentDirectory,
                                                    _dataSource.FolderPath,
                                                    _dataSource.WorkingFile);

    private static readonly XmlSerializer _serializer = new XmlSerializer(typeof(T));
    private readonly ILogger<XmlFileClient<T>> _logger;

    /// <summary>
    /// Creates an instance of the <see cref="XmlFileClient{T}"/> class.
    /// </summary>
    /// <param name="appSettings">Configuration from which data source is read.</param>
    public XmlFileClient(DataSource dataSource,
                         ILogger<XmlFileClient<T>> logger)
    {
        _dataSource = Guard.Against.Null(dataSource, nameof(dataSource));
        _logger = logger;
        ValidateDataSources(_dataSource);
        InitializeWorkingFile();
    }


    /// <inheritdoc/>
    public T? LoadData()
    {
        using var reader = new StreamReader(_workingFilePath);
        var studentsDto = _serializer.Deserialize(reader) as T;

        if (studentsDto is null)
        {
            _logger.LogError("Failed to deserialize data from file");
            return null;
        }

        return studentsDto;
    }

    /// <inheritdoc/>
    public string SaveChanges(T item)
    {
        using var writer = new StreamWriter(_workingFilePath);
        _serializer.Serialize(writer, item);

        return _workingFilePath;
    }

    /// <inheritdoc/>
    public void Reset()
    {
        try
        {
            File.Copy(_backupFilePath, _workingFilePath, overwrite: true);
            _logger.LogDebug("Successfully reset working file from backup file");
        }
        catch
        {
            throw;
        }
    }

    private void InitializeWorkingFile()
    {
        if (!File.Exists(_backupFilePath))
        {
            throw new FileNotFoundException("Backup file not found");
        }
        if (!File.Exists(_workingFilePath))
        {
            File.Copy(_backupFilePath, _workingFilePath);
        }
    }

    private static void ValidateDataSources(DataSource dataSource)
    {
        Guard.Against.Null(dataSource.BackupFile, nameof(dataSource.BackupFile));
        Guard.Against.Null(dataSource.WorkingFile, nameof(dataSource.WorkingFile));
    }
}
