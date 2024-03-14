using System.Windows.Controls;
using Gleb.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Gleb.Views;

public partial class AttendancePage : INavigableView<AttendanceViewModel>
{
    public AttendancePage(AttendanceViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }

    public AttendanceViewModel ViewModel { get; }
}