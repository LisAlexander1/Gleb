using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.Messaging;
using Gleb.Models;
using Gleb.Models.Messages;
using Microsoft.EntityFrameworkCore;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Gleb.ViewModels.Pages;

public partial class TeacherViewModel : ObservableValidator, INavigationAware
{
    private JournalDbContext JournalDbContext { get; }
    private INavigationService NavigationService { get; }
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

    public TeacherViewModel(JournalDbContext journalDbContext, INavigationService navigationService)
    {
        JournalDbContext = journalDbContext;
        NavigationService = navigationService;
        WeakReferenceMessenger.Default.Register<TeacherMessage>(this, HandleMessage);
        FormSubmissionCompleted += HandleSubmissionCompleted;
    }

    private void HandleMessage(object recipient, TeacherMessage message)
    {
        IsEdit = message.IsEdit;
        this.Id = message.Teacher.Id;
        this.FirstName = message.Teacher.FirstName;
        this.LastName = message.Teacher.LastName;
        this.Surname = message.Teacher.Surname;
        this.PassportSerial = message.Teacher.PassportSerial;
        this.PassportNumber = message.Teacher.PassportNumber;
        this.BirthDay = message.Teacher.BirthDay == DateTime.MinValue ? DateTime.Now : message.Teacher.BirthDay;
        this.Photo = message.Teacher.Photo;
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
        if (this.Id == 0)
        {
            await JournalDbContext.Teachers.AddAsync(new Teacher()
            {
                BirthDay = _birthDay!.Value,
                FirstName = _firstName,
                LastName = _lastName,
                Surname = _surname,
                PassportNumber = _passportNumber,
                PassportSerial = _passportSerial,
                Photo = _photo,
            });
        }
        else
        {
            var teacher = await JournalDbContext.Teachers.FirstOrDefaultAsync(t => t.Id == this.Id);
            if (teacher != null)
            {
                teacher.Id = Id;
                teacher.BirthDay = BirthDay!.Value;
                teacher.FirstName = FirstName;
                teacher.LastName = LastName;
                teacher.Surname = Surname;
                teacher.PassportNumber = PassportNumber;
                teacher.PassportSerial = PassportSerial;
                teacher.Photo = Photo;
                JournalDbContext.Teachers.Update(teacher);
            }
            
        }

        JournalDbContext.SaveChanges();
        Back();
    }
}