using CommunityToolkit.Mvvm.Messaging;
using Gleb.Models;
using Gleb.Models.Messages;
using Gleb.Views.Pages;
using Microsoft.EntityFrameworkCore;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Gleb.ViewModels.Pages;

public partial class TeachersListViewModel : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;

    [ObservableProperty]
    private List<Teacher> _teachers;

    [ObservableProperty]
    private bool _isLoading; 

    private JournalDbContext DbContext { get; }
    private INavigationService NavigationService { get; }

    public TeachersListViewModel(JournalDbContext dbContext, INavigationService navigationService)
    {
        DbContext = dbContext;
        NavigationService = navigationService;
    }

    public void OnNavigatedTo()
    {
        InitializeViewModel();
        LoadData();
    }

    public void OnNavigatedFrom()
    {
    }

    private void InitializeViewModel()
    {
        if (_isInitialized) return;
        
        _isInitialized = true;
    }
    
    [RelayCommand]
    private async void LoadData()
    {
        IsLoading = true;
        await Task.Delay(1000);
        Teachers = await DbContext.Teachers.ToListAsync();
        IsLoading = false;
    }

    [RelayCommand]
    private void Create()
    {
        WeakReferenceMessenger.Default.Send<TeacherMessage>(new TeacherMessage(new Teacher(), true));
        NavigationService.NavigateWithHierarchy(typeof(TeacherPage));
    }

    [RelayCommand]
    private void Edit(Teacher teacher)
    {
        WeakReferenceMessenger.Default.Send<TeacherMessage>(new TeacherMessage(teacher, false));
        NavigationService.NavigateWithHierarchy(typeof(TeacherPage));
    }
}