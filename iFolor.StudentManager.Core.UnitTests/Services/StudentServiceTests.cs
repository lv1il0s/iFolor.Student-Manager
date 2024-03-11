using iFolor.StudentManager.Core.Contracts.Infrastructure;
using iFolor.StudentManager.Core.Contracts.UI;
using iFolor.StudentManager.Core.Models;
using iFolor.StudentManager.Core.Services;

using Microsoft.Extensions.Logging;

using Moq;

namespace iFolor.StudentManager.Core.UnitTests.Services;
public class StudentServiceTests
{
    private readonly StudentService _sut;
    private readonly Mock<IStudentRepository> _studentRepositoryMock = new();
    private readonly Mock<IDialogService> _dialogServiceMock = new();
    private readonly Mock<ILogger<StudentService>> _loggerMock = new();

    public StudentServiceTests()
    {
        _sut = new StudentService(_studentRepositoryMock.Object, 
                                  _dialogServiceMock.Object,
                                  _loggerMock.Object);
    }

    [Fact]
    public void AddStudent_WhenCalled_ShouldCallRepositoryAddMethod()
    {
        // Arrange
        var student = GetValidStudent();

        // Act
        _sut.AddStudent(student);

        // Assert
        _studentRepositoryMock.Verify(sr => sr.Add(It.IsAny<Student>()), Times.Once);
    }

    [Fact]
    public void DeleteStudents_WhenSuccessful_ReturnsTrue()
    {
        // Arrange
        var studentIds = new List<int> { 1, 2, 3 };
        _dialogServiceMock.Setup(x => x.ShowConfirmationDialog(It.IsAny<string>()))
            .Returns(true);

        // Act
        var result = _sut.DeleteStudents(studentIds);

        // Assert
        Assert.True(result);
        _studentRepositoryMock.Verify(sr => sr.Remove(It.IsAny<int>()), Times.Exactly(studentIds.Count));
    }

    [Fact]
    public void DeleteStudents_WhenFailed_ReturnsFalse()
    {
        // Arrange
        _studentRepositoryMock.Setup(sr => sr.Remove(It.IsAny<int>())).Throws(new Exception());
        _dialogServiceMock.Setup(x => x.ShowConfirmationDialog(It.IsAny<string>()))
            .Returns(true);

        // Act
        var result = _sut.DeleteStudents(new List<int> { 1 });

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void DeleteStudents_WhenDialogCanceled_ReturnsFalse()
    {
        // Arrange
        var studentIds = new List<int> { 1, 2, 3 };
        _dialogServiceMock.Setup(x => x.ShowConfirmationDialog(It.IsAny<string>()))
            .Returns(false);

        // Act
        var result = _sut.DeleteStudents(new List<int> { 1 });

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GetAllStudents_WhenCalled_ReturnsStudents()
    {
        // Arrange
        var students = new List<Student> { GetValidStudent(), GetValidStudent() };
        _studentRepositoryMock.Setup(sr => sr.GetAll()).Returns(students);

        // Act
        var result = _sut.GetAllStudents();

        // Assert
        Assert.Equal(students.Count, result.ToList().Count);
    }

    [Fact]
    public void SaveChanges_WhenCalled_ShouldCallSameMethodOnRepo()
    {
        // Arrange

        // Act
        _sut.SaveChanges();

        // Assert
        _studentRepositoryMock.Verify(sr => sr.SaveChanges(), Times.Once);
    }

    private Student GetValidStudent()
    {
        return new Student
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Age = 25,
            Gender = Gender.Male
        };
    }
}
