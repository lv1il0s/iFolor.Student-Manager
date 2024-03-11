using System.Windows.Controls;

using Microsoft.Xaml.Behaviors;

namespace iFolor.StudentManager.Windows.Behaviors;

/// <summary>
/// Scrolls to the selected item in the list view.
/// </summary>
public class ScrollToSelectedItemBehavior : Behavior<ListView>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        this.AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        this.AssociatedObject.SelectionChanged -= AssociatedObject_SelectionChanged;
    }

    private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is ListView listView && listView.SelectedItem != null)
        {
            listView.ScrollIntoView(listView.SelectedItem);
            var item = (ListViewItem)listView.ItemContainerGenerator.ContainerFromItem(listView.SelectedItem);
            item?.Focus();
        }
    }
}
