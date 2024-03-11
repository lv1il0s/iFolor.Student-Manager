using System.Windows;

using iFolor.StudentManager.Core.Contracts.UI;

namespace iFolor.StudentManager.Windows.Views.Services;
public class DialogService : IDialogService
{
    public bool ShowConfirmationDialog(string message)
    {
        MessageBoxResult result = MessageBox.Show(message, "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        return result == MessageBoxResult.Yes;
    }

    public void ShowInfoDialog(string message)
    {
        MessageBox.Show(message, "Students Saved Successfully", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}

