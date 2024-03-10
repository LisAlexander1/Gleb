using System.Windows.Controls;
using Gleb.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Gleb.Views.Pages;

public partial class StudentPage : INavigableView<StudentViewModel>
{
    public StudentPage(StudentViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        
        InitializeComponent();
    }

    public StudentViewModel ViewModel { get; }
}