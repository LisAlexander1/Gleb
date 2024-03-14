using System.Windows.Controls;
using Gleb.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Gleb.Views.Pages;

public partial class LessonClassSubjectsListPage : INavigableView<LessonClassSubjectsListViewModel>
{
    public LessonClassSubjectsListPage(LessonClassSubjectsListViewModel listViewModel)
    {
        ViewModel = listViewModel;
        DataContext = this;
        InitializeComponent();
    }

    public LessonClassSubjectsListViewModel ViewModel { get; }
}