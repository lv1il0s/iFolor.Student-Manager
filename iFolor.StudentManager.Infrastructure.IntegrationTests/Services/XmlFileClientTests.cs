using iFolor.StudentManager.Infrastructure.Configuration;
using iFolor.StudentManager.Infrastructure.Services;

using Microsoft.Extensions.Logging;

using Moq;

namespace iFolor.StudentManager.Infrastructure.IntegrationTests.Services;
public class XmlFileClientIntegrationTests : IDisposable
{
    private readonly string _testDirectory;
    private readonly DataSource _dataSource;
    private readonly Mock<ILogger<XmlFileClient<TestDataWrapper>>> _loggerMock = new();

    public XmlFileClientIntegrationTests()
    {
        _testDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        Directory.CreateDirectory(_testDirectory);

        _dataSource = new DataSource
        {
            FolderPath = _testDirectory,
            BackupFile = "backup.xml",
            WorkingFile = "working.xml"
        };

        // Create a backup file for testing
        var backupFilePath = Path.Combine(_testDirectory, _dataSource.BackupFile);
        File.WriteAllText(backupFilePath, GetRawXmlData());
    }

    [Fact]
    public void LoadData_WhenFileExists_ReturnsData()
    {
        // Arrange
        var client = new XmlFileClient<TestDataWrapper>(_dataSource, _loggerMock.Object);

        // Act
        var result = client.LoadData();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void SaveChanges_WhenCalled_WritesDataToFile()
    {
        // Arrange
        var client = new XmlFileClient<TestDataWrapper>(_dataSource, _loggerMock.Object);
        var testData = new TestDataWrapper();

        // Act
        var filePath = client.SaveChanges(testData);

        // Assert
        Assert.True(File.Exists(filePath));
    }

    [Fact]
    public void Reset_WhenCalled_ResetsWorkingFileFromBackup()
    {
        // Arrange
        var client = new XmlFileClient<TestDataWrapper>(_dataSource, _loggerMock.Object);
        var workingFilePath = Path.Combine(_testDirectory, _dataSource.WorkingFile);

        // Modify the working file to simulate changes
        File.WriteAllText(workingFilePath, "<ModifiedData></ModifiedData>");

        // Act
        client.Reset();

        // Assert
        var workingFileContents = File.ReadAllText(workingFilePath);
        Assert.Contains("<TestDataWrapper>", workingFileContents);
    }

    public void Dispose()
    {
        // Clean up test data directory
        if (Directory.Exists(_testDirectory))
        {
            Directory.Delete(_testDirectory, true);
        }
    }

    private string GetRawXmlData() =>
                    """
                    <?xml version="1.0" encoding="UTF-8"?>
                    <TestDataWrapper>
                        <Data>
                            <TestProperty>Value 1</TestProperty>
                        </Data>
                        <Data>
                            <TestProperty>Value 2</TestProperty>
                        </Data>
                        <Data>
                            <TestProperty>Value 3</TestProperty>
                        </Data>
                        <Data>
                            <TestProperty>Value 4</TestProperty>
                        </Data>
                        <Data>
                            <TestProperty>Value 5</TestProperty>
                        </Data>
                        <Data>
                            <TestProperty>Value 6</TestProperty>
                        </Data>
                    </TestDataWrapper>
                    """;

}

public record TestDataWrapper()
{
    List<TestData>? Data { get; init; }
}
public record TestData()
{
    public string? TestProperty { get; init; }
}