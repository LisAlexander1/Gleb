using System.Windows.Controls;
using System.Windows.Data;
using Wpf.Ui.Controls;

namespace Gleb.Helpers;

public static class MenuItemFactory
{
    public static NavigationViewItem BindingToTitle<T>(T page)
    {
        var binding = new Binding("Title") { Source = page, Mode = BindingMode.OneWay, NotifyOnSourceUpdated = true, NotifyOnTargetUpdated = true};
        var menuItem = new NavigationViewItem
        {
            TargetPageType = typeof(T),
            Visibility = Visibility.Collapsed
        };
        menuItem.SetBinding(ContentControl.ContentProperty, binding);
        return menuItem;
    }
}