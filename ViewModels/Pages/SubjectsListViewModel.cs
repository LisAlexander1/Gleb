using CommunityToolkit.Mvvm.Messaging;
using Gleb.Models;
using Gleb.Models.Messages;
using Gleb.Views.Pages;
using Microsoft.EntityFrameworkCore;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Gleb.ViewModels.Pages;

public partial class SubjectsListViewModel : ObservableObject, INavigationAware
{
    private bool _isInitialized;

    [ObservableProperty] private bool _isLoading;

    [ObservableProperty] private List<Subject> _subjects;

    public SubjectsListViewModel(JournalDbContext dbContext, INavigationService navigationService)
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
        Subjects = await DbContext.Subjects.ToListAsync();
        IsLoading = false;
    }

    [RelayCommand]
    private void Open(Subject schoolSubject)
    {
        WeakReferenceMessenger.Default.Send(new SubjectMessage(schoolSubject, false));
        NavigationService.NavigateWithHierarchy(typeof(SubjectPage));
    }

    [RelayCommand]
    private void Create()
    {
        WeakReferenceMessenger.Default.Send(new SubjectMessage(new Subject(), true));
        NavigationService.NavigateWithHierarchy(typeof(SubjectPage));
    }
}