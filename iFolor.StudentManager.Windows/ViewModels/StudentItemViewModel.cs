using FluentValidation;

using iFolor.StudentManager.Core.Models;
using iFolor.StudentManager.Core.Validators;

using Prism.Events;

namespace iFolor.StudentManager.Windows.ViewModels;

/// <summary>
/// Student model wrapper for the view.
/// </summary>
public class StudentItemViewModel : ValidatableViewModelBase<Student>
{
    protected override Student Model { get; }
    private readonly StudentValidator _validator = new();

    /// <summary>
    /// Creates an instance of the <see cref="StudentItemViewModel"/> class.
    /// </summary>
    /// <param name="model"></param>
    public StudentItemViewModel(Student model,
                                IValidator<Student> validator,
                                IEventAggregator eventAggregator) 
        : base(validator, eventAggregator)
    {
        Model = model;
        Validate(Model, _validator);
    }
    
    /// <summary>
    /// Student's ID.
    /// </summary>
    public int Id => Model.Id;

    /// <summary>
    /// Student's first name.
    /// </summary>
    public string FirstName
    {
        get => Model.FirstName;
        set
        {
            Model.FirstName = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Student's last name.
    /// </summary>
    public string LastName
    {
        get => Model.LastName;
        set
        {
            Model.LastName = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Student's age.
    /// </summary>
    public int Age
    {
        get => Model.Age;
        set
        {
            Model.Age = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Student's gender.
    /// </summary>
    public Gender Gender
    {
        get => Model.Gender;
        set
        {
            Model.Gender = value;
            OnPropertyChanged();
        }
    }

}
