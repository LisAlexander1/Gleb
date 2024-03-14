using CommunityToolkit.Mvvm.Messaging;
using Gleb.Models;
using Gleb.Models.Messages;
using Gleb.Views.Pages;
using Microsoft.EntityFrameworkCore;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Gleb.ViewModels.Pages;

public partial class ClassesListViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty] private List<Class> _classes;

    private bool _isInitialized;

    [ObservableProperty] private bool _isLoading;

    public ClassesListViewModel(JournalDbContext dbContext, INavigationService navigationService)
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
    private async void LoadData()
    {
        IsLoading = true;
        
        Classes = await DbContext.Classes.ToListAsync();
        IsLoading = false;
    }

    [RelayCommand]
    private void Create()
    {
        WeakReferenceMessenger.Default.Send(new ClassMessage(new Class(), true));
        NavigationService.NavigateWithHierarchy(typeof(ClassPage));
    }

    [RelayCommand]
    private void Edit(Class @class)
    {
        WeakReferenceMessenger.Default.Send(new ClassMessage(@class, false));
        NavigationService.NavigateWithHierarchy(typeof(ClassPage));
    }
}