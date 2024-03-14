using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using Gleb.Helpers;
using Gleb.Models;
using Gleb.Models.Messages;
using Gleb.Views.Pages;
using Microsoft.EntityFrameworkCore;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Gleb.ViewModels.Pages;

public partial class LessonsListViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty] private Class _class;
    [ObservableProperty] private Subject _subject;
    [ObservableProperty] private ObservableCollection<Lesson> _lessons;

    private bool _isInitialized;

    [ObservableProperty] private bool _isLoading;

    public LessonsListViewModel(JournalDbContext dbContext, INavigationService navigationService)
    {
        DbContext = dbContext;
        NavigationService = navigationService;

        WeakReferenceMessenger.Default.Register<LessonsListMessage>(this, HandleMessage);
    }

    private JournalDbContext DbContext { get; }
    private INavigationService NavigationService { get; }

    public void OnNavigatedTo()
    {
        RefreshCommand.Execute(null);
    }

    public void OnNavigatedFrom() {}
    

    private void HandleMessage(object recipient, LessonsListMessage message)
    {
        Class = message.Class;
        Subject = message.Subject;
        Lessons = message.Class.ClassSubjects.SelectMany(cs => cs.Lessons).Where(l => l.SubjectId == Subject.Id).ToObservableCollection();
    }

    [RelayCommand]
    private void Edit(Lesson lesson)
    {
        WeakReferenceMessenger.Default.Send(new LessonMessage(lesson));
        NavigationService.NavigateWithHierarchy(typeof(LessonPage));
    }
    
    [RelayCommand]
    private async Task Refresh()
    {
        var l = await DbContext.Lessons.Where(l => l.ClassId == Class.Id && l.SubjectId == Subject.Id).ToListAsync();
        Lessons = l.ToObservableCollection();
    }
    
    [RelayCommand]
    private void Create()
    {
        WeakReferenceMessenger.Default.Send(new LessonMessage(new Lesson()
            { ClassSubject = new ClassSubject
                { Class = Class, Subject = Subject}}) );
        NavigationService.NavigateWithHierarchy(typeof(LessonPage));
    }
}