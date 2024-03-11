using iFolor.StudentManager.Core.Contracts.Infrastructure;
using iFolor.StudentManager.Windows.Commands;
using iFolor.StudentManager.Windows.Events;

using Prism.Events;

namespace iFolor.StudentManager.Windows.ViewModels;

/// <summary>
/// ViewModel shown in case there are no more students in the list.
/// </summary>
/// <param name="studentResetter">Used to reset the list from the backup file.</param>
/// <param name="eventAggregator">Used to notify containing view model about the finished reset operation.</param>
public class EmptyViewModel(IDataResetter studentResetter,
                            IEventAggregator eventAggregator) : ViewModelBase
{
    /// <summary>
    /// Resets the student list.
    /// </summary>
    public DelegateCommand ResetCommand { get; } = new DelegateCommand((_) => 
    {
        studentResetter.Reset();
        eventAggregator.GetEvent<StudentsResetCompletedEvent>().Publish();
    });
}
