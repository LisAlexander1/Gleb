using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.Messaging;
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
    private bool _isInitialized = false;
    private INavigationService NavigationService { get; }
    private JournalDbContext DbContext { get; }
    private IContentDialogService DialogService { get; }

    [Required]
    [MaxLength(15)]
    [MinLength(1)]
    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private bool _isLoading = false;
    
    [ObservableProperty]
    private bool _isEdit = false;
    
    [ObservableProperty]
    private int _id = 0;
    
    [ObservableProperty]
    private List<Student> _students;
    
    public event EventHandler? FormSubmissionCompleted;
    public event EventHandler? FormSubmissionFailed;

    public ClassViewModel(INavigationService navigationService, JournalDbContext dbContext, IContentDialogService contentDialogService)
    {
        NavigationService = navigationService;
        DbContext = dbContext;
        DialogService = contentDialogService;
        WeakReferenceMessenger.Default.Register<ClassMessage>(this, HandleMessage);
        FormSubmissionCompleted += HandleSubmissionCompleted;
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
    
    private void HandleMessage(object recipient, ClassMessage message)
    {
        IsEdit = message.IsEdit;
        Id = message.Class.Id;
        Name = message.Class.Name;
        Students = message.Class.Students;
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
        if (Id == 0)
        {
            await DbContext.Classes.AddAsync(new Class()
            {
                Name = this.Name
            });
        }
        else
        {
            var @class = await DbContext.Classes.FirstOrDefaultAsync(t => t.Id == this.Id);
            if (@class != null)
            {
                @class.Id = Id;
                @class.Name = this.Name;
                
                DbContext.Classes.Update(@class);
            }
        }

        await DbContext.SaveChangesAsync();
        Back();
    }

    [RelayCommand]
    private void CreateStudent()
    {
        WeakReferenceMessenger.Default.Send(new StudentMessage(new Student(), true, Id));
        NavigationService.NavigateWithHierarchy(typeof(ClassPage));
    }
    
    [RelayCommand]
    private void OpenStudent(Student student)
    {
        WeakReferenceMessenger.Default.Send(new StudentMessage(student, false, Id));
        NavigationService.NavigateWithHierarchy(typeof(ClassPage));
    }

    [RelayCommand]
    private async void RefreshStudents()
    {
        IsLoading = true;
        await Task.Delay(1000);
        await DbContext.Classes.ToListAsync();
        IsLoading = false;
    }
}