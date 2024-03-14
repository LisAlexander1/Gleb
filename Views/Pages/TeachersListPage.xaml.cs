using Gleb.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Gleb.Views.Pages;

public partial class TeachersListPage : INavigableView<TeachersListViewModel>
{
    public TeachersListPage(TeachersListViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    public TeachersListViewModel ViewModel { get; }
}