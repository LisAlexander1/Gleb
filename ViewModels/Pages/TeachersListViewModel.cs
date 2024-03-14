using CommunityToolkit.Mvvm.Messaging;
using Gleb.Helpers;
using Gleb.Models;
using Gleb.Models.Messages;
using Gleb.Views.Pages;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Gleb.ViewModels.Pages;

public partial class TeachersListViewModel : ObservableObject, INavigationAware
{
    private bool _isInitialized;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private ObservableCollection<Teacher> _teachers;

    public TeachersListViewModel(JournalDbContext dbContext, INavigationService navigationService)
    {
        DbContext = dbContext;
        NavigationService = navigationService;
    }

    private JournalDbContext DbContext { get; }
    private INavigationService NavigationService { get; }

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
        
        var teacherList = await DbContext.Teachers.ToListAsync();
        Teachers = teacherList.ToObservableCollection();
        IsLoading = false;
    }

    [RelayCommand]
    private void Open(Teacher teacher)
    {
        WeakReferenceMessenger.Default.Send(new TeacherMessage(teacher, false));
        NavigationService.NavigateWithHierarchy(typeof(TeacherPage));
    }

    [RelayCommand]
    private void Create()
    {
        WeakReferenceMessenger.Default.Send(new TeacherMessage(new Teacher(), true));
        NavigationService.NavigateWithHierarchy(typeof(TeacherPage));
    }
}