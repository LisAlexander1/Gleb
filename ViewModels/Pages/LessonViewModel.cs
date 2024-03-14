using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.Messaging;
using Gleb.Helpers;
using Gleb.Models;
using Gleb.Models.Messages;
using Gleb.Views.Pages;
using Microsoft.EntityFrameworkCore;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace Gleb.ViewModels.Pages;

public partial class LessonViewModel : ObservableValidator, INavigationAware
{
    [ObservableProperty] private int _id = 0;

    [ObservableProperty] private bool _isEdit = false;
    [ObservableProperty] private Subject _subject;
    [ObservableProperty] private Class _class;
    
    
    [ObservableProperty] private List<int?> _marks = [0, 2, 3, 4, 5];

    [ObservableProperty] private ObservableCollection<LessonResult> _lessonResults;

    [Required] [ObservableProperty] private Teacher _teacher;

    [Required] [ObservableProperty] private DateTime _date;

    private bool _isInitialized;

    [ObservableProperty] private bool _isLoading = false;

    [ObservableProperty] private ObservableCollection<Student> _students;
    [ObservableProperty] private ObservableCollection<Teacher> _teachers;

    public LessonViewModel(INavigationService navigationService, JournalDbContext dbContext,
        IContentDialogService contentDialogService)
    {
        NavigationService = navigationService;
        DbContext = dbContext;
        DialogService = contentDialogService;
        WeakReferenceMessenger.Default.Register<LessonMessage>(this, HandleMessage);
        FormSubmissionCompleted += HandleSubmissionCompleted;
    }

    private INavigationService NavigationService { get; }
    private JournalDbContext DbContext { get; }
    private IContentDialogService DialogService { get; }

    public void OnNavigatedTo()
    {
        InitializeViewModel();
    }

    public void OnNavigatedFrom()
    {
    }

    public event EventHandler? FormSubmissionCompleted;
    public event EventHandler? FormSubmissionFailed;

    private void InitializeViewModel()
    {
        if (_isInitialized) return;

        _isInitialized = true;
    }

    private void HandleMessage(object recipient, LessonMessage message)
    {
        Id = message.Lesson.Id;
        Date = message.Lesson.Date == DateTime.MinValue ? DateTime.Today : message.Lesson.Date;
        Subject = message.Lesson.ClassSubject.Subject;
        if (message.Lesson.ClassSubject != null) Class = message.Lesson.ClassSubject.Class;
        if (message.Lesson.TeacherSubject != null) Teacher = message.Lesson.TeacherSubject.Teacher;
        Teachers = DbContext.TeacherSubjects.Where(ts => ts.SubjectId == Subject.Id)
            .Select(ts => ts.Teacher)
            .ToObservableCollection();

        
        LessonResults = Id == 0
            ? Class.Students.Select(s => new LessonResult { Student = s , LessonId = Id}).ToObservableCollection()
            : message.Lesson.LessonResults.ToObservableCollection();
    }


    [RelayCommand]
    private void Submit()
    {
        ValidateAllProperties();

        if (HasErrors)
            FormSubmissionFailed?.Invoke(this, EventArgs.Empty);
        else
            FormSubmissionCompleted?.Invoke(this, EventArgs.Empty);
    }

    [RelayCommand]
    private void Back()
    {
        NavigationService.GoBack();
    }

    [RelayCommand]
    private async void Delete()
    {
        var result = await DialogService.ShowSimpleDialogAsync(new SimpleContentDialogCreateOptions
        {
            Title = "Удаление",
            CloseButtonText = "Отмена",
            PrimaryButtonText = "Удалить",
            Content = "Вы уверены что хотите удалить запись?"
        });
        if (result != ContentDialogResult.Primary) return;
        var @class = await DbContext.Classes.FirstOrDefaultAsync(c => c.Id == Id);
        if (@class != null)
        {
            DbContext.Classes.Remove(@class);
            await DbContext.SaveChangesAsync();
        }

        Back();
    }

    private async void HandleSubmissionCompleted(object? sender, EventArgs eventArgs)
    {
        var lesson = Id == 0
            ? new Lesson()
            : await DbContext.Lessons.FirstOrDefaultAsync(t => t.Id == Id) ??
              new Lesson();
        lesson.Id = Id;
        lesson.Date = Date;
        lesson.SubjectId = Subject.Id;
        lesson.TeacherId = Teacher.Id;
        lesson.ClassId = Class.Id;
        


        if (Id == 0)
            await DbContext.Lessons.AddAsync(lesson);
        else
            DbContext.Lessons.Update(lesson);

        await DbContext.SaveChangesAsync();
        
        foreach (var t in LessonResults)
        {
            t.Mark = t.Mark == 0 ? null : t.Mark ;
        }
        
        
        if (Id == 0)
        {
            foreach (var t in LessonResults)
            {
                t.LessonId = lesson.Id;
            }

            await DbContext.AddRangeAsync(LessonResults);
        }
        else
            DbContext.UpdateRange(LessonResults);

            
        
        Back();
    }
}