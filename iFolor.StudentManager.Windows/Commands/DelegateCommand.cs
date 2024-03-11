using System.Windows.Input;

namespace iFolor.StudentManager.Windows.Commands;

/// <summary>
/// A command that delegates its <see cref="ICommand.CanExecute"/> 
/// and <see cref="ICommand.Execute"/> methods to the specified delegates.
/// </summary>
public class DelegateCommand : ICommand
{
    protected readonly Action<object?> _execute;
    protected readonly Func<object?, bool>? _canExecute;


    /// <summary>
    /// Creates an instance of the <see cref="DelegateCommand"/> class.
    /// </summary>
    /// <param name="execute">Action to execute.</param>
    /// <param name="canExecute">Optional condition returning whether command should be executed.</param>
    public DelegateCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        ArgumentNullException.ThrowIfNull(execute);
        _execute = execute;
        _canExecute = canExecute;
    }

    /// <summary>
    /// Notifies that the <see cref="ICommand.CanExecute"/> property has changed.
    /// </summary>
    public event EventHandler? CanExecuteChanged;

    /// <summary>
    /// Raises the <see cref="CanExecuteChanged"/> event.
    /// </summary>
    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    /// <summary>
    /// Determines whether the command can be executed.
    /// </summary>
    /// <param name="parameter">Func</param>
    /// <returns></returns>
    public virtual bool CanExecute(object? parameter)
    {
        return _canExecute is null || _canExecute(parameter);
    }

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="parameter">Action</param>
    public virtual void Execute(object? parameter)
    {
        _execute(parameter);
    }
}
