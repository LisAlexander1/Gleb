using CommunityToolkit.Mvvm.Messaging;
using Gleb.Models;
using Gleb.Models.Messages;
using Gleb.Views.Pages;
using Microsoft.EntityFrameworkCore;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Gleb.ViewModels.Pages;

public partial class LessonClassesListViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty] private List<Class> _classes;

    private bool _isInitialized;

    [ObservableProperty] private bool _isLoading;

    public LessonClassesListViewModel(JournalDbContext dbContext, INavigationService navigationService)
    {
        DbContext = dbContext;
        NavigationService = navigationService;
    }

    private JournalDbContext DbContext { get; }
    private INavigationService NavigationService { get; }

    public void OnNavigatedTo()
    {
        LoadData();
        InitializeViewModel();
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
    private async Task LoadData()
    {
        IsLoading = true;
        
        Classes = await DbContext.Classes.ToListAsync();
        IsLoading = false;
    }

    [RelayCommand]
    private void Create()
    {
        WeakReferenceMessenger.Default.Send(new LessonClassSubjectMessage(new Class()));
        NavigationService.NavigateWithHierarchy(typeof(LessonClassSubjectsListPage));
    }

    [RelayCommand]
    private void Edit(Class @class)
    {
        WeakReferenceMessenger.Default.Send(new LessonClassSubjectMessage(@class));
        NavigationService.NavigateWithHierarchy(typeof(LessonClassSubjectsListPage));
    }
}