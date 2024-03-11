using FluentAssertions;
using FluentValidation.Results;
using iFolor.StudentManager.Core.Validators;
using iFolor.StudentManager.Core.Models;
using iFolor.StudentManager.Core.Helpers;


namespace iFolor.StudentManager.Core.UnitTests.Validators;

public class StudentValidatorTests
{
    private readonly StudentValidator _sut = new();

    [Fact]
    public void Validate_WithEmptyFirstName_ShouldFail()
    {
        // Arrange
        var student = GetValidStudent();
        student.FirstName = "";

        // act
        ValidationResult result = _sut.Validate(student);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "FirstName");
    }

    [Fact]
    public void Validate_WithEmptyLastName_ShouldFail()
    {
        // Arrange
        var student = GetValidStudent();
        student.LastName = "";

        // Act
        ValidationResult result = _sut.Validate(student);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "LastName");
    }

    [Theory]
    [InlineData(Constants.Validation.Student.MIN_AGE - 1)]
    [InlineData(Constants.Validation.Student.MAX_AGE + 1)]
    public void Validate_WithAgeOutOfRange_ShouldFail(int age)
    {
        // Arrange
        var student = GetValidStudent();
        student.Age = age;

        // Act
        ValidationResult result = _sut.Validate(student);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Age");
    }

    [Fact]
    public void Validate_WithUnspecifiedGender_ShouldFail()
    {
        // Arrange
        var student = GetValidStudent();
        student.Gender = Gender.Unspecified;

        // Act
        ValidationResult result = _sut.Validate(student);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Gender");
    }

    [Fact]
    public void Validate_WithValidStudent_ShouldPass()
    {
        // Arrange
        var student = GetValidStudent();

        // Act
        ValidationResult result = _sut.Validate(student);

        // Assert
        result.IsValid.Should().BeTrue();
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
