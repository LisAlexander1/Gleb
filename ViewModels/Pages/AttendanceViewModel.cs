using System.Collections.ObjectModel;
using System.Windows.Controls;
using Gleb.Helpers;
using Gleb.Models;
using Microsoft.EntityFrameworkCore;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Gleb.ViewModels.Pages;

public partial class AttendanceViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty]
    private DateTime? _from;
    
    [ObservableProperty]
    private DateTime? _to;

    [ObservableProperty]
    private Class? _class;

    [ObservableProperty]
    private ObservableCollection<Class> _classes;

    [ObservableProperty]
    private double? _result;

    public AttendanceViewModel(INavigationService navigationService, JournalDbContext dbContext)
    {
        NavigationService = navigationService;
        DbContext = dbContext;
    }

    private INavigationService NavigationService { get; }
    private JournalDbContext DbContext { get; }

    public void OnNavigatedTo()
    {
        Classes = DbContext.Classes.ToObservableCollection();
    }

    public void OnNavigatedFrom()
    {
       
    }

    [RelayCommand]
    private void Calc()
    {
        if (Class == null) return;
        var from = From ?? DateTime.MinValue;
        var to = To ?? DateTime.MaxValue;

        var results = DbContext.Classes.SelectMany(@class => @class.Students).SelectMany(s => s.LessonResults);
        Result = results.Average(s => s.IsSkipped ? 100 : 0);
    }
    
    [RelayCommand]
    private void Reset()
    {
        From = null;
        To = null;
        Class = null;
        Result = null;
    }
}