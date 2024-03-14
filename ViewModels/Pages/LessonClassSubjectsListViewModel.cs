using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using Gleb.Helpers;
using Gleb.Models;
using Gleb.Models.Messages;
using Gleb.Views.Pages;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Gleb.ViewModels.Pages;

public partial class LessonClassSubjectsListViewModel : ObservableObject, INavigationAware
{
    [ObservableProperty]
    private Class _class;

    [ObservableProperty]
    private ObservableCollection<Subject> _subjects;

    public LessonClassSubjectsListViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
        WeakReferenceMessenger.Default.Register<LessonClassSubjectMessage>(this, HandleMessage);
    }

    private void HandleMessage(object recipient, LessonClassSubjectMessage message)
    {
        Class = message.Class;
        Subjects = message.Class.Subjects.ToObservableCollection();
    }

    public void OnNavigatedTo()
    {
        
    }

    public void OnNavigatedFrom()
    {
    }
    private INavigationService NavigationService { get; }
    
    [RelayCommand]
    private void Open(Subject subject)
    {
        WeakReferenceMessenger.Default.Send(new LessonsListMessage(Class, subject));
        NavigationService.NavigateWithHierarchy(typeof(LessonsListPage));
    }
}