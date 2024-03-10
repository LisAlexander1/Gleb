using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using Gleb.Helpers;
using Gleb.Models;
using Gleb.Models.Messages;
using Gleb.Views.Pages;
using Microsoft.EntityFrameworkCore;
using Wpf.Ui;
using Wpf.Ui.Controls;
using NavigationService = System.Windows.Navigation.NavigationService;

namespace Gleb.ViewModels.Pages;

public partial class LessonsListViewModel : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private Class _class;

    [ObservableProperty]
    private ObservableCollection<Lesson> _lessons;

    private JournalDbContext DbContext { get; }
    private INavigationService NavigationService { get; }

    public LessonsListViewModel(JournalDbContext dbContext, INavigationService navigationService)
    {
        DbContext = dbContext;
        NavigationService = navigationService;
        
        WeakReferenceMessenger.Default.Register<LessonClassMessage>(this, HandleMessage);

    }

    private void HandleMessage(object recipient, LessonClassMessage message)
    {
        this.Class = message.Class;
        this.Lessons = message.Class.Lessons!.ToObservableCollection();
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
        Lessons = this.Class.Lessons!.ToObservableCollection();
        IsLoading = false;
    }
    
    [RelayCommand]
    private void Create()
    {
        WeakReferenceMessenger.Default.Send(new LessonMessage(new Lesson()));
        NavigationService.NavigateWithHierarchy(typeof(ClassPage));
    }

    [RelayCommand]
    private void Edit(Lesson lesson)
    {
        WeakReferenceMessenger.Default.Send(new LessonMessage(lesson));
        NavigationService.NavigateWithHierarchy(typeof(ClassPage));
    }
}