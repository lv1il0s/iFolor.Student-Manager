namespace iFolor.StudentManager.Core.Models;

/// <summary>
/// Main domain model.
/// </summary>
public class Student
{
    required public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; } = Gender.Unspecified;
}
