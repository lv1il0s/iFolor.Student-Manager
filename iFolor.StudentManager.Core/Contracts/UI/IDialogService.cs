namespace iFolor.StudentManager.Core.Contracts.UI;
public interface IDialogService
{
    bool ShowConfirmationDialog(string message);
    void ShowInfoDialog(string message);
}

