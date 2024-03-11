using iFolor.StudentManager.Core.Models;

namespace iFolor.StudentManager.Core.Contracts.Services;

/// <summary>
/// Holds the business logic for managing students.
/// </summary>
public interface IStudentService
{
    /// <summary>
    /// Gets all students.
    /// </summary>
    /// <returns></returns>
    IEnumerable<Student> GetAllStudents();

    /// <summary>
    /// Adds a new student.
    /// </summary>
    /// <param name="student"></param>
    void AddStudent(Student student);

    /// <summary>
    /// Deletes students by their IDs.
    /// </summary>
    /// <param name="studentIds">IDs of students to be deleted.</param>
    /// <returns>Whether operation was successful.</returns>
    bool DeleteStudents(IEnumerable<int> studentIds);
    
    /// <summary>
    /// Persists changes to the data store.
    /// </summary>
    /// <returns>Location of the file where data is persisted.</returns>
    string SaveChanges();


}