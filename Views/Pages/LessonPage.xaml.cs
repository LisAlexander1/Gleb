using Gleb.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Gleb.Views.Pages;

public partial class LessonPage : INavigableView<LessonViewModel>
{
    public LessonPage(LessonViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    public LessonViewModel ViewModel { get; }
}