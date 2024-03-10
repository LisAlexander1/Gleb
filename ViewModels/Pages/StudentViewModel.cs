using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Windows.Media.Imaging;
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
    private JournalDbContext JournalDbContext { get; }
    private INavigationService NavigationService { get; }
    private IContentDialogService DialogService { get; }
    
    private bool _isInitialized = false;
    
    public event EventHandler? FormSubmissionCompleted;
    public event EventHandler? FormSubmissionFailed;

    
    [ObservableProperty]
    private int _id;

    [Required]
    [ObservableProperty] 
    private string _firstName;
    
    [Required]
    [ObservableProperty]
    private string _lastName;
    
    [ObservableProperty] 
    private string? _surname;
    
    [ObservableProperty] 
    private byte[]? _photo;

    [Required]
    [Range(1000,9999)]
    [ObservableProperty]
    private int _passportSerial;
    
    [Required]
    [Range(100000,999999)]
    [ObservableProperty]
    private int _passportNumber;
    
    [Required]
    [ObservableProperty]
    private DateTime? _birthDay;

    [ObservableProperty]
    private bool _isEdit;

    [Required]
    [ObservableProperty]
    private int _classId;

    public StudentViewModel(JournalDbContext journalDbContext, INavigationService navigationService, IContentDialogService dialogService)
    {
        JournalDbContext = journalDbContext;
        NavigationService = navigationService;
        DialogService = dialogService;
        WeakReferenceMessenger.Default.Register<StudentMessage>(this, HandleMessage);
        FormSubmissionCompleted += HandleSubmissionCompleted;
    }

    private void HandleMessage(object recipient, StudentMessage message)
    {
        IsEdit = message.IsEdit;
        this.Id = message.Student.Id;
        this.FirstName = message.Student.FirstName;
        this.LastName = message.Student.LastName;
        this.Surname = message.Student.Surname;
        this.PassportSerial = message.Student.PassportSerial;
        this.PassportNumber = message.Student.PassportNumber;
        this.BirthDay = message.Student.BirthDay == DateTime.MinValue ? DateTime.Now : message.Student.BirthDay;
        this.Photo = message.Student.Photo;
        this.ClassId = message.ClassId;
    }

    public void OnNavigatedTo()
    {
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
    private void Submit()
    {
        ValidateAllProperties();

        if (HasErrors)
        {
            FormSubmissionFailed?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            FormSubmissionCompleted?.Invoke(this, EventArgs.Empty);
        }

    }

    [RelayCommand]
    private void Back()
    {
        NavigationService.GoBack();
    }

    [RelayCommand]
    private async void Delete()
    {
        var result = await DialogService.ShowSimpleDialogAsync(new SimpleContentDialogCreateOptions()
        {
            Title = "Удаление",
            CloseButtonText = "Отмена",
            PrimaryButtonText = "Удалить",
            Content = "Вы уверены что хотите удалить запись?"
        });
        if (result != ContentDialogResult.Primary)
        {
            return;
        }
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
        if (this.Id == 0)
        {
            await JournalDbContext.Students.AddAsync(new Student()
            {
                BirthDay = _birthDay!.Value,
                FirstName = _firstName,
                LastName = _lastName,
                Surname = _surname,
                PassportNumber = _passportNumber,
                PassportSerial = _passportSerial,
                Photo = _photo,
                ClassId = _classId
            });
        }
        else
        {
            var student = await JournalDbContext.Students.FirstOrDefaultAsync(t => t.Id == this.Id);
            if (student != null)
            {
                student.Id = Id;
                student.BirthDay = BirthDay!.Value;
                student.FirstName = FirstName;
                student.LastName = LastName;
                student.Surname = Surname;
                student.PassportNumber = PassportNumber;
                student.PassportSerial = PassportSerial;
                student.Photo = Photo;
                student.ClassId = ClassId;
                JournalDbContext.Students.Update(student);
            }
            
        }

        await JournalDbContext.SaveChangesAsync();
        Back();
    }
    
    [RelayCommand]
    public void OpenPicture()
    {

        OpenFileDialog openFileDialog =
            new()
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Filter = "Image files (*.bmp;*.jpg;*.jpeg;*.png)|*.bmp;*.jpg;*.jpeg;*.png|All files (*.*)|*.*"
            };

        if (openFileDialog.ShowDialog() != true)
        {
            return;
        }

        if (!File.Exists(openFileDialog.FileName))
        {
            return;
        }

        Photo = openFileDialog.OpenFile().ReadFully();
    }
}