using System.Collections;

namespace Gleb.Views.UserControls;

public partial class ListPage
{
    public static readonly DependencyProperty DataTemplateProperty = DependencyProperty.Register(
        nameof(DataTemplate),
        typeof(DataTemplate),
        typeof(ListPage));

    public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
        nameof(Items),
        typeof(IEnumerable),
        typeof(ListPage),
        new FrameworkPropertyMetadata(null));

    public static readonly DependencyProperty RefreshCommandProperty = DependencyProperty.Register(
        nameof(RefreshCommand),
        typeof(RelayCommand),
        typeof(ListPage));

    public static readonly DependencyProperty AddCommandProperty = DependencyProperty.Register(
        nameof(AddCommand),
        typeof(RelayCommand),
        typeof(ListPage));

    public static readonly DependencyProperty IsLoadingProperty = DependencyProperty.Register(
        nameof(IsLoading),
        typeof(bool),
        typeof(ListPage));

    public static readonly DependencyProperty SomeTextProperty = DependencyProperty.Register(
        nameof(SomeText),
        typeof(string),
        typeof(ListPage),
        new PropertyMetadata(""));

    public ListPage()
    {
        InitializeComponent();
    }

    public DataTemplate DataTemplate
    {
        get => (DataTemplate)GetValue(DataTemplateProperty);
        set => SetValue(DataTemplateProperty, value);
    }

    public IEnumerable Items
    {
        get => (IEnumerable)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    public RelayCommand RefreshCommand
    {
        get => (RelayCommand)GetValue(RefreshCommandProperty);
        set => SetValue(RefreshCommandProperty, value);
    }

    public RelayCommand AddCommand
    {
        get => (RelayCommand)GetValue(AddCommandProperty);
        set => SetValue(AddCommandProperty, value);
    }

    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }

    public string SomeText
    {
        get => (string)GetValue(SomeTextProperty);
        set => SetValue(SomeTextProperty, value);
    }
}