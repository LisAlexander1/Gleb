using Gleb.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Gleb.Views.Pages;

public partial class SubjectPage : INavigableView<SubjectViewModel>
{
    public SubjectPage(SubjectViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }

    public SubjectViewModel ViewModel { get; }
}