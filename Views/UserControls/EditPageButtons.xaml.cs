using System.Windows.Controls;

namespace Gleb.Views.UserControls;

public partial class EditPageButtons : UserControl
{
    public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register(
        nameof(CancelCommand),
        typeof(RelayCommand),
        typeof(EditPageButtons));

    public static readonly DependencyProperty SaveCommandProperty = DependencyProperty.Register(
        nameof(SaveCommand),
        typeof(RelayCommand),
        typeof(EditPageButtons));

    public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register(
        nameof(DeleteCommand),
        typeof(RelayCommand),
        typeof(EditPageButtons));

    public static readonly DependencyProperty IsEditProperty = DependencyProperty.Register(
        nameof(IsEdit),
        typeof(bool),
        typeof(EditPageButtons),
        new FrameworkPropertyMetadata(false));

    public EditPageButtons()
    {
        InitializeComponent();
    }

    public RelayCommand CancelCommand
    {
        get => (RelayCommand)GetValue(CancelCommandProperty);
        set => SetValue(CancelCommandProperty, value);
    }

    public RelayCommand SaveCommand
    {
        get => (RelayCommand)GetValue(SaveCommandProperty);
        set => SetValue(SaveCommandProperty, value);
    }

    public RelayCommand DeleteCommand
    {
        get => (RelayCommand)GetValue(DeleteCommandProperty);
        set => SetValue(DeleteCommandProperty, value);
    }

    public RelayCommand IsEdit
    {
        get => (RelayCommand)GetValue(IsEditProperty);
        set => SetValue(IsEditProperty, value);
    }
}