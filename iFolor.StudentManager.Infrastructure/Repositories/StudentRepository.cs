using iFolor.StudentManager.Core.Contracts.Infrastructure;
using iFolor.StudentManager.Core.Models;
using iFolor.StudentManager.Infrastructure.Contracts.Dtos;
using iFolor.StudentManager.Infrastructure.Contracts.Services;
using Mapster;

using Microsoft.Extensions.Logging;

namespace iFolor.StudentManager.Infrastructure.Repositories;

/// <inheritdoc/>
public class StudentRepository : IStudentRepository, IDataResetter
{
    private readonly IXmlFileClient<XmlStudentsDto> _client;
    private readonly ILogger<StudentRepository> _logger;

    private List<Student> _inMemoryStudents = new();

    /// <summary>
    /// Creates an instance of the <see cref="StudentRepository"/> class.
    /// </summary>
    /// <param name="client">Client that extracts / persists data using a file.</param>
    /// <param name="logger">Logging helper.</param>
    public StudentRepository(IXmlFileClient<XmlStudentsDto> client,
                             ILogger<StudentRepository> logger)
    {
        _client = client;
        _logger = logger;
    }

    /// <inheritdoc/>
    public void Add(Student item)
    {
        _inMemoryStudents.Add(item);
    }

    /// <inheritdoc/>
    public IEnumerable<Student> GetAll()
    {
        LoadStudentsFromXml();
        return _inMemoryStudents;
    }

    /// <inheritdoc/>
    public Student? GetById(uint id)
    {
        return _inMemoryStudents.FirstOrDefault(x => x.Id == id);
    }

    /// <inheritdoc/>
    public void Remove(int id)
    {
        _inMemoryStudents.Remove(_inMemoryStudents.FirstOrDefault(x => x.Id == id));
    }

    /// <inheritdoc/>
    public string SaveChanges()
    {
        var studentsDto = new XmlStudentsDto
        {
            Students = _inMemoryStudents.Adapt<List<XmlStudentItemDto>>()
        };

        var savedFilePath = _client.SaveChanges(studentsDto);
        return savedFilePath;
    }

    /// <inheritdoc/>
    public void Reset()
    {
        _client.Reset();
        LoadStudentsFromXml();
    }

    private void LoadStudentsFromXml()
    {
        var studentsFromFile = _client.LoadData();
        _inMemoryStudents = studentsFromFile.Students.Adapt<List<Student>>();

        _logger.LogDebug("Successfully loaded {@students} from xml file.", _inMemoryStudents);
    }


}
