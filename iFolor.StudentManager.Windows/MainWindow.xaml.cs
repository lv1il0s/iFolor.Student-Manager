using System.Windows;

using iFolor.StudentManager.Windows.ViewModels;

namespace iFolor.StudentManager.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainViewModel _viewModel;

    /// <summary>
    /// Creates an instance of the MainWindow.
    /// </summary>
    /// <param name="viewModel">Related ViewModel.</param>
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        DataContext = _viewModel;
        Loaded += MainWindow_Loaded;
    }

    //Note: This would be used in case VM would need to init itself asynchronously.
    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await _viewModel.Load();
    }
}