using iFolor.StudentManager.Core.Models;

namespace iFolor.StudentManager.Core.Contracts.Infrastructure;

/// <summary>
/// Repository for the Student entity (for possible future extensions not supported by base generic variant).
/// </summary>
public interface IStudentRepository : IRepository<Student>
{
}
