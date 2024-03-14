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

public partial class ClassViewModel : ObservableValidator, INavigationAware
{
    [ObservableProperty]
    private int _id = 0;

    [ObservableProperty]
    private bool _isEdit = false;

    private bool _isInitialized;

    [ObservableProperty]
    private bool _isLoading = false;

    [Required]
    [MaxLength(15)]
    [MinLength(1)]
    [ObservableProperty]
    private string _name;

    [ObservableProperty] 
    private ObservableCollection<Student> _students;

    [ObservableProperty]
    private ObservableCollection<Teacher> _teachers;

    [ObservableProperty]
    private ObservableCollection<Subject> _availableSubjects;

    [ObservableProperty]
    private Subject _selectedSubject;
    
    [ObservableProperty]
    private ObservableCollection<Subject> _subjects = [];

    public ClassViewModel(INavigationService navigationService, JournalDbContext dbContext,
        IContentDialogService contentDialogService)
    {
        NavigationService = navigationService;
        DbContext = dbContext;
        DialogService = contentDialogService;
        RefreshStudentsCommand = new AsyncRelayCommand(RefreshStudents);
        WeakReferenceMessenger.Default.Register<ClassMessage>(this, HandleMessage);
        FormSubmissionCompleted += HandleSubmissionCompleted;
    }

    private INavigationService NavigationService { get; }
    private JournalDbContext DbContext { get; }
    private IContentDialogService DialogService { get; }

    public void OnNavigatedTo()
    {
        RefreshStudentsCommand.ExecuteAsync(null);

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

    private void HandleMessage(object recipient, ClassMessage message)
    {
        IsEdit = message.IsEdit;
        Id = message.Class.Id;
        Name = message.Class.Name;
        Students = message.Class.Students.ToObservableCollection();
        Subjects = message.Class.Subjects.ToObservableCollection();
        Teachers = message.Class.ClassSubjects.SelectMany(cs => cs.Lessons).Select(l => l.TeacherSubject.Teacher).Distinct().ToObservableCollection();

        LoadSubjects();
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
    
    private async void LoadSubjects()
    {
        var subjects = await DbContext.Subjects.ToListAsync();
        foreach (var subject in subjects)
        {
            subject.IsSelected = false;
        }
        AvailableSubjects = subjects.ToObservableCollection();
        var willSelect = AvailableSubjects.Intersect(Subjects);
        foreach (var subject in willSelect)
        {
            subject.IsSelected = true;
        }
    }

    private async void HandleSubmissionCompleted(object? sender, EventArgs eventArgs)
    {
        var @class = Id == 0 ? new Class() :  await DbContext.Classes.FirstOrDefaultAsync(t => t.Id == this.Id) ?? new Class();

        
        @class.Name = Name;
        @class.Subjects = AvailableSubjects.Where(s => s.IsSelected).ToList();

        if (Id == 0)
            await DbContext.Classes.AddAsync(@class);
        else
            DbContext.Classes.Update(@class);

        await DbContext.SaveChangesAsync();
        Id = @class.Id;
        IsEdit = false;
        IsLoading = false;
    }

    [RelayCommand]
    private void CreateStudent()
    {
        WeakReferenceMessenger.Default.Send(new StudentMessage(new Student(), true, Id));
        NavigationService.NavigateWithHierarchy(typeof(StudentPage));
    }

    [RelayCommand]
    private void OpenStudent(Student student)
    {
        WeakReferenceMessenger.Default.Send(new StudentMessage(student, false, Id));
        NavigationService.NavigateWithHierarchy(typeof(StudentPage));
    }
    
    public IAsyncRelayCommand RefreshStudentsCommand { get; }

    private async Task RefreshStudents()
    {
        IsLoading = true;

        if (Id == 0)
        {
            IsLoading = false;
            return;
        }
        await DbContext.Classes.Include(c => c.Students).LoadAsync();
        var @class = await DbContext.Classes.FirstOrDefaultAsync(c => c.Id == Id);
        if (@class == null)
        {
            IsLoading = false;
            return;
        }
        Students = @class.Students!.ToObservableCollection();
        IsLoading = false;
    }
}