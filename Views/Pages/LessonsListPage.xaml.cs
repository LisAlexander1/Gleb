using Gleb.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Gleb.Views.Pages;

public partial class LessonsListPage : INavigableView<LessonsListViewModel>
{
    public LessonsListPage(LessonsListViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }

    public LessonsListViewModel ViewModel { get; }
}