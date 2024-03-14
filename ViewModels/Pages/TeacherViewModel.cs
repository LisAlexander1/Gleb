using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

public partial class TeacherViewModel : ObservableValidator, INavigationAware
{
    [Required]
    [ObservableProperty]
    private DateTime? _birthDay;

    [Required]
    [ObservableProperty]
    private string _firstName;

    [ObservableProperty]
    private ObservableCollection<Subject> _availableSubjects;

    [ObservableProperty]
    private Subject _selectedSubject;


    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private bool _isEdit;

    private bool _isInitialized;

    [Required]
    [ObservableProperty]
    private string _lastName;

    [Required]
    [Range(100000, 999999)]
    [ObservableProperty]
    private int _passportNumber;

    [Required]
    [Range(1000, 9999)]
    [ObservableProperty]
    private int _passportSerial;

    [ObservableProperty]
    private byte[]? _photo;

    [ObservableProperty]
    private string? _surname;

    [ObservableProperty]
    private ObservableCollection<Subject> _subjects = [];
    
    public TeacherViewModel(JournalDbContext journalDbContext, INavigationService navigationService,
        IContentDialogService dialogService)
    {
        JournalDbContext = journalDbContext;
        NavigationService = navigationService;
        DialogService = dialogService;
        WeakReferenceMessenger.Default.Register<TeacherMessage>(this, HandleMessage);
        FormSubmissionCompleted += HandleSubmissionCompleted;
    }

    private JournalDbContext JournalDbContext { get; }
    private INavigationService NavigationService { get; }
    private IContentDialogService DialogService { get; }

    public void OnNavigatedTo()
    {
        InitializeViewModel();
    }

    private async void LoadSubjects()
     {
         var subjects = await JournalDbContext.Subjects.ToListAsync();
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

    public void OnNavigatedFrom()
    {
    }

    // partial void OnSelectedSubjectChanged(Subject? value)
    // {
    //     if (value == null)
    //         return;
    //
    //     value.IsSelected = true;
    // }

    public event EventHandler? FormSubmissionCompleted;
    public event EventHandler? FormSubmissionFailed;

    private void HandleMessage(object recipient, TeacherMessage message)
    {
        IsEdit = message.IsEdit;
        Id = message.Teacher.Id;
        FirstName = message.Teacher.FirstName;
        LastName = message.Teacher.LastName;
        Surname = message.Teacher.Surname;
        PassportSerial = message.Teacher.PassportSerial;
        PassportNumber = message.Teacher.PassportNumber;
        BirthDay = message.Teacher.BirthDay == DateTime.MinValue ? DateTime.Now : message.Teacher.BirthDay;
        Photo = message.Teacher.Photo;
        Subjects = message.Teacher.Subjects.ToObservableCollection();

        LoadSubjects();
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
    private async Task Delete()
    {
        var result = await DialogService.ShowSimpleDialogAsync(new SimpleContentDialogCreateOptions
        {
            Title = "Удаление",
            CloseButtonText = "Отмена",
            PrimaryButtonText = "Удалить",
            Content = "Вы уверены что хотите удалить запись?"
        });
        if (result != ContentDialogResult.Primary) return;
        var teacher = await JournalDbContext.Teachers.FirstOrDefaultAsync(t => t.Id == this.Id);
        if (teacher != null)
        {
            JournalDbContext.Teachers.Remove(teacher);
            await JournalDbContext.SaveChangesAsync();
        }

        Back();
    }

    private async void HandleSubmissionCompleted(object? sender, EventArgs eventArgs)
    {

        var teacher = (Id == 0 ? new Teacher() : await JournalDbContext.Teachers.FirstOrDefaultAsync(t => t.Id == this.Id)) ??
                      new Teacher();
        
        teacher.Id = Id;
        teacher.BirthDay = BirthDay!.Value;
        teacher.FirstName = FirstName;
        teacher.LastName = LastName;
        teacher.Surname = Surname;
        teacher.PassportNumber = PassportNumber;
        teacher.PassportSerial = PassportSerial;
        teacher.Photo = Photo;
        teacher.Subjects = AvailableSubjects.Where(s => s.IsSelected).ToList();

        if (Id == 0)
            await JournalDbContext.Teachers.AddAsync(teacher);
        else
            JournalDbContext.Teachers.Update(teacher);

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