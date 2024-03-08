using System.Windows.Controls;

namespace Gleb.Views.UserControls;

public partial class EditPageButtons : UserControl
{
    public EditPageButtons()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty CancelCommandProperty = DependencyProperty.Register(
        nameof(CancelCommand), 
        typeof(RelayCommand),
        typeof(EditPageButtons));
    
    public RelayCommand CancelCommand
    {
        get { return (RelayCommand)GetValue(CancelCommandProperty); }
        set { SetValue(CancelCommandProperty, value); }
    }
    
    public static readonly DependencyProperty SaveCommandProperty = DependencyProperty.Register(
        nameof(SaveCommand), 
        typeof(RelayCommand),
        typeof(EditPageButtons));
    
    public RelayCommand SaveCommand
    {
        get { return (RelayCommand)GetValue(SaveCommandProperty); }
        set { SetValue(SaveCommandProperty, value); }
    }
    
    public static readonly DependencyProperty DeleteCommandProperty = DependencyProperty.Register(
        nameof(DeleteCommand), 
        typeof(RelayCommand),
        typeof(EditPageButtons));
    
    public RelayCommand DeleteCommand
    {
        get { return (RelayCommand)GetValue(DeleteCommandProperty); }
        set { SetValue(DeleteCommandProperty, value); }
    }
    
    public static readonly DependencyProperty IsEditProperty = DependencyProperty.Register(
        nameof(IsEdit), 
        typeof(bool),
        typeof(EditPageButtons), 
        new FrameworkPropertyMetadata(false));
    
    public RelayCommand IsEdit
    {
        get { return (RelayCommand)GetValue(IsEditProperty); }
        set { SetValue(IsEditProperty, value); }
    }
}