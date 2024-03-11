using iFolor.StudentManager.Windows.Configuration;
using iFolor.StudentManager.Windows.Events;

using Prism.Events;

namespace iFolor.StudentManager.Windows.ViewModels;

/// <summary>
/// Main view model for the application.
/// </summary>
public class MainViewModel : ViewModelBase
{
    private ViewModelBase? _selectedViewModel;

    private readonly StudentsViewModel _studentsViewModel;
    private EmptyViewModel? _emptyViewModel;
    private string? _title;

    /// <summary>
    /// Creates an instance of the <see cref="MainViewModel"/> class.
    /// </summary>
    /// <param name="studentsViewModel">One of the viewmodels that can be shown <see cref="StudentsViewModel"/></param>
    /// <param name="emptyViewModel"></param>
    /// <param name="appSettings"></param>
    /// <param name="eventAggregator"></param>
    public MainViewModel(StudentsViewModel studentsViewModel,
                         EmptyViewModel emptyViewModel,
                         AppSettings appSettings,
                         IEventAggregator eventAggregator)
    {
        _studentsViewModel = studentsViewModel;
        _emptyViewModel = emptyViewModel;

        Title = appSettings.ApplicationName;
        eventAggregator.GetEvent<StudentListEmptyEvent>().Subscribe(() => SelectedViewModel = _emptyViewModel);
        eventAggregator.GetEvent<StudentsResetCompletedEvent>().Subscribe(() => SelectedViewModel = _studentsViewModel);
        SelectedViewModel = _studentsViewModel;
    }

    /// <summary>
    /// Application title.
    /// </summary>
    public string? Title
    {
        get => _title; 
        set
        {
            _title = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Currently selected viewmodel.
    /// </summary>
    public ViewModelBase? SelectedViewModel
    {
        get => _selectedViewModel;
        set
        {
            _selectedViewModel = value;
            _selectedViewModel?.Load();
            OnPropertyChanged();
        }
    }
}
