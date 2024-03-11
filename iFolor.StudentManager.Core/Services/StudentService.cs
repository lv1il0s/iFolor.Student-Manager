using iFolor.StudentManager.Core.Contracts.Infrastructure;
using iFolor.StudentManager.Core.Contracts.Services;
using iFolor.StudentManager.Core.Contracts.UI;
using iFolor.StudentManager.Core.Models;
using Microsoft.Extensions.Logging;

namespace iFolor.StudentManager.Core.Services;

/// <inheritdoc/>
public class StudentService(IStudentRepository studentRepository,
                            IDialogService dialogService,
                            ILogger<StudentService> logger) : IStudentService
{
    /// <inheritdoc/>
    public void AddStudent(Student student)
    {
        try
        {
            studentRepository.Add(student);
        }
        catch (Exception ex)
        {
            // TODO: e.g. some custom business rule
            logger.LogError(ex, "Error adding student");
        }
    }

    /// <inheritdoc/>
    public bool DeleteStudents(IEnumerable<int> studentIds)
    {
        var userMessage = $"Are you sure you want to delete selected {studentIds.Count()} student(s)?";
        bool isConfirmedByUser = dialogService.ShowConfirmationDialog(userMessage);
        if (!isConfirmedByUser)
        {
            return false;
        }

        try
        {
            foreach (var id in studentIds)
            {
                studentRepository.Remove(id);
            }
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed deleting students");
            return false;
        }
    }

    /// <inheritdoc/>
    public IEnumerable<Student> GetAllStudents()
    {
        return studentRepository.GetAll();
    }

    /// <inheritdoc/>
    public string SaveChanges()
    {
        logger.LogInformation("Saving changes");
        return studentRepository.SaveChanges();
    }
}
