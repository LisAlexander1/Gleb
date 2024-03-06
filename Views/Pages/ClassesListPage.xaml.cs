﻿using System.Windows.Controls;
using Gleb.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace Gleb.Views.Pages;

public partial class ClassesListPage : INavigableView<ClassesListViewModel>
{
    public ClassesListPage(ClassesListViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }

    public ClassesListViewModel ViewModel { get; }
}