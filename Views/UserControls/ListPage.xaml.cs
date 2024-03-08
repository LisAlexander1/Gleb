using System.Collections;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Gleb.Views.UserControls;

public partial class ListPage
{
    public ListPage()
    {
        InitializeComponent();
    }
    
    public static readonly DependencyProperty DataTemplateProperty = DependencyProperty.Register(
        nameof(DataTemplate), 
        typeof(DataTemplate),
        typeof(ListPage));

    public DataTemplate DataTemplate
    {
        get { return (DataTemplate)GetValue(DataTemplateProperty); }
        set { SetValue(DataTemplateProperty, value); }
    }

    public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
        nameof(Items), 
        typeof(IEnumerable),
        typeof(ListPage),
        new FrameworkPropertyMetadata(null));
    
    public IEnumerable Items
    {
        get { return (IEnumerable)GetValue(ItemsProperty); }
        set { SetValue(ItemsProperty, value); }
    }
    
    public static readonly DependencyProperty RefreshCommandProperty = DependencyProperty.Register(
        nameof(RefreshCommand), 
        typeof(RelayCommand),
        typeof(ListPage));
    
    public RelayCommand RefreshCommand
    {
        get { return (RelayCommand)GetValue(RefreshCommandProperty); }
        set { SetValue(RefreshCommandProperty, value); }
    }
    
    public static readonly DependencyProperty AddCommandProperty = DependencyProperty.Register(
        nameof(AddCommand), 
        typeof(RelayCommand),
        typeof(ListPage));
    
    public RelayCommand AddCommand
    {
        get { return (RelayCommand)GetValue(AddCommandProperty); }
        set { SetValue(AddCommandProperty, value); }
    }
    
    public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.Register(
        nameof(IsLoading), 
        typeof(bool),
        typeof(ListPage));
    
    public bool IsLoading
    {
        get { return (bool)GetValue(IsLoadingProperty); }
        set { SetValue(IsLoadingProperty, value); }
    }
    
    public static readonly DependencyProperty SomeTextProperty = DependencyProperty.Register(
        nameof(SomeText), 
        typeof(string),
        typeof(ListPage),
        new PropertyMetadata(""));
    
    public string SomeText
    {
        get { return (string)GetValue(SomeTextProperty); }
        set { SetValue(SomeTextProperty, value); }
    }
}