using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace iFolor.StudentManager.Windows.ViewModels;

/// <summary>
/// Base for ViewModels implementing INotifyPropertyChanged and supporting async loading.
/// </summary>
public  class ViewModelBase : INotifyPropertyChanged
{
    /// <summary>
    /// Fired when a property changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Asynchronously loads the ViewModel.
    /// </summary>
    /// <returns></returns>
    public virtual Task Load() => Task.CompletedTask;

}
