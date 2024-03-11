using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using FluentValidation;

using iFolor.StudentManager.Windows.Events;

using Prism.Events;

namespace iFolor.StudentManager.Windows.ViewModels;

/// <summary>
/// Base class for ViewModels that support validation.
/// </summary>
/// <typeparam name="TModel">Model that validation is based on.</typeparam>
public abstract class ValidatableViewModelBase<TModel> : ViewModelBase, INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> _errorsByPropertyName = new();
    private readonly IValidator<TModel> _validator;
    private readonly IEventAggregator _eventAggregator;

    protected abstract TModel Model { get; }

    protected ValidatableViewModelBase(IValidator<TModel> validator,
                                       IEventAggregator eventAggregator)
    {
        _validator = validator;
        _eventAggregator = eventAggregator;
    }

    /// <summary>
    /// Checks if the model has any validation errors.
    /// </summary>
    public bool HasErrors => _errorsByPropertyName.Count != 0;

    /// <summary>
    /// Notifies about changes in the validation errors.
    /// </summary>
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    IEnumerable INotifyDataErrorInfo.GetErrors(string? propertyName)
    {
        if (propertyName is null)
        {
            return Enumerable.Empty<string>();
        }

        var hasRelatedErrors = _errorsByPropertyName.ContainsKey(propertyName);
        if (!hasRelatedErrors)
        {
            return Enumerable.Empty<string>();
        }

        return _errorsByPropertyName[propertyName];
    }

    protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
    {
        ErrorsChanged?.Invoke(this, e);
        _eventAggregator.GetEvent<ErrorsChangedEvent<TModel>>().Publish(Model);
    }

    protected void AddError(string error, [CallerMemberName] string? propertyName = null)
    {
        if (propertyName is null) return;

        if (!_errorsByPropertyName.ContainsKey(propertyName))
        {
            _errorsByPropertyName[propertyName] = new List<string>();
        }
        if (!_errorsByPropertyName[propertyName].Contains(error))
        {
            _errorsByPropertyName[propertyName].Add(error);
            OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(HasErrors));
        }
    }

    protected void ClearErrors([CallerMemberName] string? propertyName = null)
    {
        if (propertyName is null) return;

        if (_errorsByPropertyName.ContainsKey(propertyName))
        {
            _errorsByPropertyName.Remove(propertyName);
            OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
            OnPropertyChanged(nameof(HasErrors));
        }
    }

    protected void Validate(TModel model, IValidator<TModel> validator)
    {
        var validationResult = validator.Validate(model);
        foreach (var error in validationResult.Errors)
        {
            AddError(error.ErrorMessage, error.PropertyName);
        }
    }

    /// <summary>
    /// Re-evaluates the validation of the model.
    /// </summary>
    /// <param name="propertyName">Name of the property that triggered the method.</param>
    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        ClearErrors(propertyName);
        Validate(Model, _validator);
        base.OnPropertyChanged(propertyName);
    }


}
