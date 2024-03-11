using FluentValidation;

using iFolor.StudentManager.Core.Helpers;
using iFolor.StudentManager.Core.Models;

namespace iFolor.StudentManager.Core.Validators;

/// <summary>
/// Validation rules for the <see cref="Student"/> model.
/// </summary>
public class StudentValidator : AbstractValidator<Student>
{
    public StudentValidator()
    {
        RuleFor(s => s.FirstName).NotEmpty();
        RuleFor(s => s.LastName).NotEmpty();
        RuleFor(s => s.Age)
            .InclusiveBetween(Constants.Validation.Student.MIN_AGE,
                              Constants.Validation.Student.MAX_AGE)
            // Note: Redundant
            .GreaterThanOrEqualTo(0);
        RuleFor(s => s.Gender).Must(x => x != Gender.Unspecified)
            .WithMessage("Gender must be specified");
    }
}
