using Gleb.Models;
using Microsoft.EntityFrameworkCore;
using Wpf.Ui.Controls;

namespace Gleb.ViewModels.Pages;

public partial class ClassesListViewModel : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;

    [ObservableProperty]
    private List<Class> _classes;
    
    [ObservableProperty]
    private bool _isLoading; 

    private JournalDbContext DbContext { get; }

    public ClassesListViewModel(JournalDbContext dbContext)
    {
        DbContext = dbContext;
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
        
        LoadData();
        
        _isInitialized = true;
    }
    
    [RelayCommand]
    private async void LoadData()
    {
        IsLoading = true;
        await Task.Delay(1000);
        Classes = await DbContext.Classes.ToListAsync();
        IsLoading = false;
    }
}