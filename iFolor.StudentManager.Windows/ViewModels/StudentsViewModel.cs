using System.Collections.ObjectModel;

using FluentValidation;

using iFolor.StudentManager.Core.Contracts.Services;
using iFolor.StudentManager.Core.Contracts.UI;
using iFolor.StudentManager.Core.Models;
using iFolor.StudentManager.Windows.Commands;
using iFolor.StudentManager.Windows.Events;

using Prism.Events;

namespace iFolor.StudentManager.Windows.ViewModels;

/// <summary>
/// ViewModel for the StudentsView containing the list of students along .
/// </summary>
public class StudentsViewModel : ViewModelBase
{
    private readonly IStudentService _studentService;
    private readonly IValidator<Student> _studentValidator;
    private readonly IDialogService _dialogService;
    private readonly IEventAggregator _eventAggregator;
    private StudentItemViewModel? _selectedStudent;

    /// <summary>
    /// Creates an instance of the <see cref="StudentsViewModel"/> class.
    /// </summary>
    /// <param name="studentService">Used to manipulate students.</param>
    /// <param name="studentValidator">Validates students.</param>
    /// <param name="dialogService">Used for showing dialogs to users.</param>
    /// <param name="eventAggregator">Used for decoupled communication with other classes.</param>
    public StudentsViewModel(IStudentService studentService,
                             IValidator<Student> studentValidator,
                             IDialogService dialogService,
                             IEventAggregator eventAggregator)
    {
        _studentService = studentService;
        _studentValidator = studentValidator;
        _dialogService = dialogService;
        _eventAggregator = eventAggregator;

        InitCommands();
        InitSelectionCollections();
        InitEventSubscriptions();
    }

    /// <summary>
    /// Currently displayed collection of students.
    /// </summary>
    public ObservableCollection<StudentItemViewModel> Students { get; } = new();

    public StudentItemViewModel? SelectedStudent
    {
        get => _selectedStudent;
        set
        {
            _selectedStudent = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsStudentSelected));
            DeleteCommand.RaiseCanExecuteChanged();
        }
    }

    public bool IsStudentSelected => _selectedStudent != null;

    /// <summary>
    /// Used for adding new students.
    /// </summary>
    public DelegateCommand? AddCommand { get; set; }

    /// <summary>
    /// Used for deleting selected students.
    /// </summary>
    public DelegateCommand DeleteCommand { get; set; }

    /// <summary>
    /// Used for persisting the changes made to the students collection.
    /// </summary>
    public DelegateCommand SaveCommand { get; set; }

    /// <summary>
    /// List of available genders to choose from.
    /// </summary>
    public List<Gender> Genders { get; set; }

    ///
    public override Task Load()
    {
        var students = _studentService.GetAllStudents();
        if (students is not null)
        {
            foreach (var student in students)
            {
                var vm = WrapModel(student);
                Students.Add(vm);
            }
        }

        return Task.CompletedTask;
    }

    private void Add(object? obj)
    {
        var student = new Student() { Id = Students.Last().Id + 1 };
        var vm = WrapModel(student);
        _studentService.AddStudent(student);
        Students.Add(vm);
        SelectedStudent = vm;
        SaveCommand.RaiseCanExecuteChanged();
    }

    private bool CanDelete(object? parameter) => SelectedStudent is not null;

    private void Delete(object? parameter)
    {
        if (parameter is null) return;

        var studentWrappers = parameter as ObservableCollection<object>;
        var students = studentWrappers.OfType<StudentItemViewModel>();

        DeleteStudents(students);
    }

    private void DeleteStudents(IEnumerable<StudentItemViewModel> students)
    {
        var isDeletionSuccessful = _studentService.DeleteStudents(students.Select(s => s.Id));
        if (isDeletionSuccessful)
        {
            foreach (var student in students.ToArray())
            {
                Students.Remove(student);
            }
        }
        SelectedStudent = null;
        SaveCommand.RaiseCanExecuteChanged();
    }

    private bool CanSave(object? arg) => Students.All(x => !x.HasErrors);

    private void Save(object? obj)
    {
        var fileLocation = _studentService.SaveChanges();
        _dialogService.ShowInfoDialog("User collection persisted successfuly. File location: " + fileLocation);
    }

    private void InitCommands()
    {
        AddCommand = new DelegateCommand(Add);
        DeleteCommand = new DelegateCommand(Delete, CanDelete);
        SaveCommand = new DelegateCommand(Save, CanSave);
    }


    private void InitSelectionCollections()
    {
        Genders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
    }

    private void InitEventSubscriptions()
    {
        Students.CollectionChanged += Students_CollectionChanged;
        _eventAggregator.GetEvent<StudentsResetCompletedEvent>().Subscribe(async () => await Load());
        _eventAggregator.GetEvent<ErrorsChangedEvent<Student>>().Subscribe((_) =>
        {
            SaveCommand.RaiseCanExecuteChanged();
        });
    }

    private void Students_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (sender is not ObservableCollection<StudentItemViewModel> students)
        {
            return;
        }

        if (students.Count == 0)
        {
            _eventAggregator.GetEvent<StudentListEmptyEvent>().Publish();
        }
    }

    private StudentItemViewModel WrapModel(Student model)
    {
        var vm = new StudentItemViewModel(model, _studentValidator, _eventAggregator);
        return vm;
    }
}
