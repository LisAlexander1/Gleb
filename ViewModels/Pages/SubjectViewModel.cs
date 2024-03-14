using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.Messaging;
using Gleb.Helpers;
using Gleb.Models;
using Gleb.Models.Messages;
using Microsoft.EntityFrameworkCore;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace Gleb.ViewModels.Pages;

public partial class SubjectViewModel : ObservableValidator, INavigationAware
{
    [ObservableProperty]
    private int _id;

    [ObservableProperty]
    private bool _isEdit = true;

    private bool _isInitialized;

    [Required]
    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private ObservableCollection<Teacher> _teachers;


    public SubjectViewModel(JournalDbContext journalDbContext, INavigationService navigationService,
        IContentDialogService dialogService)
    {
        JournalDbContext = journalDbContext;
        NavigationService = navigationService;
        DialogService = dialogService;
        WeakReferenceMessenger.Default.Register<SubjectMessage>(this, HandleMessage);
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

    private void HandleMessage(object recipient, SubjectMessage message)
    {
        IsEdit = message.IsEdit;
        Id = message.Subject.Id;
        Name = message.Subject.Name;
        Teachers = message.Subject.Teachers.ToObservableCollection();
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
        var subject = await JournalDbContext.Subjects.FirstOrDefaultAsync(t => t.Id == this.Id);
        if (subject != null)
        {
            JournalDbContext.Subjects.Remove(subject);
            await JournalDbContext.SaveChangesAsync();
        }

        Back();
    }

    private async void HandleSubmissionCompleted(object? sender, EventArgs eventArgs)
    {

        var subject = Id == 0
            ? new Subject()
            : await JournalDbContext.Subjects.FirstOrDefaultAsync(t => t.Id == this.Id) ?? new Subject();
        
        subject.Id = Id;
        subject.Name = Name;
        if (Id == 0)
            await JournalDbContext.Subjects.AddAsync(subject);
        else
            JournalDbContext.Subjects.Update(subject);
        
        await JournalDbContext.SaveChangesAsync();
        Back();
    }
}