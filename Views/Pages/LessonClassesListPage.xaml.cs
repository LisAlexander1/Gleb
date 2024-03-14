using Gleb.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Gleb.Views.Pages;

public partial class LessonClassesListPage : INavigableView<LessonClassesListViewModel>
{
    public LessonClassesListPage(LessonClassesListViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }

    public LessonClassesListViewModel ViewModel { get; }
}