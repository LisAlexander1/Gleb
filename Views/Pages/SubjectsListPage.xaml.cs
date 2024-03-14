using Gleb.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Gleb.Views.Pages;

public partial class SubjectsListPage : INavigableView<SubjectsListViewModel>
{
    public SubjectsListPage(SubjectsListViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    public SubjectsListViewModel ViewModel { get; }
}