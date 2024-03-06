using System.Windows.Controls;
using Gleb.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Gleb.Views.Pages;

public partial class TeacherPage : INavigableView<TeacherViewModel>
{
    public TeacherPage(TeacherViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        
        InitializeComponent();
    }

    public TeacherViewModel ViewModel { get; }
}