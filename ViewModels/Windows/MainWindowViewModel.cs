// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Collections.ObjectModel;
using Gleb.Helpers;
using Gleb.ViewModels.Pages;
using Gleb.Views;
using Gleb.Views.Pages;
using Wpf.Ui.Controls;

namespace Gleb.ViewModels.Windows;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private string _applicationTitle = "Школьный журнал";

    [ObservableProperty]
    private ObservableCollection<object> _footerMenuItems =
    [
        new NavigationViewItem
        {
            Content = "Настройки",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
            TargetPageType = typeof(SettingsPage)
        }
    ];

    [ObservableProperty]
    private ObservableCollection<object> _menuItems =
    [
        new NavigationViewItem
        {
            Content = "Предметы",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Book24 },
            TargetPageType = typeof(SubjectsListPage)
        },

        new NavigationViewItem
        {
            Content = "Учителя",
            Icon = new SymbolIcon { Symbol = SymbolRegular.HatGraduation24 },
            TargetPageType = typeof(TeachersListPage)
        },

        new NavigationViewItem
        {
            Content = "Классы",
            Icon = new SymbolIcon { Symbol = SymbolRegular.PeopleCommunity24 },
            TargetPageType = typeof(ClassesListPage)
        },

        new NavigationViewItem
        {
            Content = "Занятия",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Book24 },
            TargetPageType = typeof(LessonClassesListPage)
        },
        
        new NavigationViewItem
        {
            Content = "Пропуски",
            Icon = new SymbolIcon { Symbol = SymbolRegular.AnimalRabbitOff20 },
            TargetPageType = typeof(AttendancePage)
        },
    ];

    [ObservableProperty]
    private ObservableCollection<MenuItem> _trayMenuItems = [
        new MenuItem { Header = "Home", Tag = "tray_home" },
    ];

    public MainWindowViewModel(
        TeacherPage teacherPage,
        ClassPage classPage,
        SubjectPage subjectPage,
        StudentPage studentPage,
        LessonPage lessonPage,
        LessonClassSubjectsListPage lessonClassSubjectsListPage,
        LessonsListPage lessonsListPage)

    {
        MenuItems.Add(MenuItemFactory.BindingToTitle(teacherPage));
        MenuItems.Add(MenuItemFactory.BindingToTitle(classPage));
        MenuItems.Add(MenuItemFactory.BindingToTitle(studentPage));
        MenuItems.Add(MenuItemFactory.BindingToTitle(subjectPage));
        MenuItems.Add(MenuItemFactory.BindingToTitle(lessonPage));
        MenuItems.Add(MenuItemFactory.BindingToTitle(lessonClassSubjectsListPage));
        MenuItems.Add(MenuItemFactory.BindingToTitle(lessonsListPage));
        
    }
}