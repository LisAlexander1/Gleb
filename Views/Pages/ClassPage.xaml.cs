using Gleb.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Gleb.Views.Pages;

public partial class ClassPage : INavigableView<ClassViewModel>
{
    public ClassPage(ClassViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }

    public ClassViewModel ViewModel { get; }
}