using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using CommunityToolkit.Mvvm.Messaging;
using Gleb.Helpers;
using Gleb.Models;
using Gleb.Models.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace Gleb.ViewModels.Pages;

public partial class StudentViewModel : ObservableValidator, INavigationAware
{
    [Required] [ObservableProperty] private DateTime? _birthDay;

    [Required] [ObservableProperty] private int _classId;

    [Required] [ObservableProperty] private string _firstName;


    [ObservableProperty] private int _id;

    [ObservableProperty] private bool _isEdit;

    private bool _isInitialized;

    [Required] [ObservableProperty] private string _lastName;

    [Required] [Range(100000, 999999)] [ObservableProperty]
    private int _passportNumber;

    [Required] [Range(1000, 9999)] [ObservableProperty]
    private int _passportSerial;

    [ObservableProperty] private byte[]? _photo;

    [ObservableProperty] private string? _surname;

    [ObservableProperty] private ObservableCollection<SubjectAverageMark> _marks;

    public StudentViewModel(JournalDbContext journalDbContext, INavigationService navigationService,
        IContentDialogService dialogService)
    {
        JournalDbContext = journalDbContext;
        NavigationService = navigationService;
        DialogService = dialogService;
        WeakReferenceMessenger.Default.Register<StudentMessage>(this, HandleMessage);
        FormSubmissionCompleted += HandleSubmissionCompleted;
    }

    private JournalDbContext JournalDbContext { get; }
    private INavigationService NavigationService { get; }
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

    private void HandleMessage(object recipient, StudentMessage message)
    {
        IsEdit = message.IsEdit;
        Id = message.Student.Id;
        FirstName = message.Student.FirstName;
        LastName = message.Student.LastName;
        Surname = message.Student.Surname;
        PassportSerial = message.Student.PassportSerial;
        PassportNumber = message.Student.PassportNumber;
        BirthDay = message.Student.BirthDay == DateTime.MinValue ? DateTime.Now : message.Student.BirthDay;
        Photo = message.Student.Photo;
        ClassId = message.Student.ClassId;

        var classSubjects = message.Student.Class?.ClassSubjects.Select(cs => cs.Subject) ?? new List<Subject>();
        Marks = classSubjects.Select(s =>
        {
            var results = s.ClassSubjects
                .SelectMany(cs => cs.Lessons)
                .Where(l => l.SubjectId == s.Id)
                .SelectMany(l => l.LessonResults)
                .Where(l => l.StudentId == Id).ToList();
            
            return results.Count != 0 ? new SubjectAverageMark
                {
                    Subject = s,
                    Mark = results.Average(lr => lr.Mark) ?? 0,
                    Attendance = results.Average(lr => !lr.IsSkipped ? 5 : 0)
                } : new SubjectAverageMark { Subject = s };
        }).ToObservableCollection();
    }

    private void InitializeViewModel()
    {
        if (_isInitialized) return;

        _isInitialized = true;
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
        var student = await JournalDbContext.Students.FirstOrDefaultAsync(t => t.Id == this.Id);
        if (student != null)
        {
            JournalDbContext.Students.Remove(student);
            await JournalDbContext.SaveChangesAsync();
        }

        Back();
    }

    private async void HandleSubmissionCompleted(object? sender, EventArgs eventArgs)
    {
        var student = Id == 0
            ? new Student()
            : await JournalDbContext.Students.FirstOrDefaultAsync(t => t.Id == this.Id) ?? new Student();
        
        student.Id = Id;
        student.BirthDay = BirthDay!.Value;
        student.FirstName = FirstName;
        student.LastName = LastName;
        student.Surname = Surname;
        student.PassportNumber = PassportNumber;
        student.PassportSerial = PassportSerial;
        student.Photo = Photo;
        student.ClassId = ClassId;
        
        if (Id == 0)
            await JournalDbContext.Students.AddAsync(student);
        else
            JournalDbContext.Students.Update(student);

        await JournalDbContext.SaveChangesAsync();
        
        Back();
    }

    [RelayCommand]
    private void OpenPicture()
    {
        OpenFileDialog openFileDialog =
            new()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Filter = "Image files (*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png|All files (*.*)|*.*"
            };

        if (openFileDialog.ShowDialog() != true) return;

        if (!File.Exists(openFileDialog.FileName)) return;

        Photo = openFileDialog.OpenFile().ReadFully();
    }
}