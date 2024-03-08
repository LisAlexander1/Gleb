using CommunityToolkit.Mvvm.Messaging;
using Gleb.Models;
using Gleb.Models.Messages;
using Gleb.Views.Pages;
using Microsoft.EntityFrameworkCore;
using Wpf.Ui;
using Wpf.Ui.Controls;
using NavigationService = System.Windows.Navigation.NavigationService;

namespace Gleb.ViewModels.Pages;

public partial class ClassesListViewModel : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;

    [ObservableProperty]
    private List<Class> _classes;
    
    [ObservableProperty]
    private bool _isLoading; 

    private JournalDbContext DbContext { get; }
    private INavigationService NavigationService { get; }

    public ClassesListViewModel(JournalDbContext dbContext, INavigationService navigationService)
    {
        DbContext = dbContext;
        NavigationService = navigationService;
    }

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
        await Task.Delay(1000);
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