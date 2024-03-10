// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using Gleb.ViewModels.Pages;
using Gleb.Views.Pages;
using Wpf.Ui.Controls;
using MenuItem = Wpf.Ui.Controls.MenuItem;

namespace Gleb.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "Школьный журнал";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Учителя",
                Icon = new SymbolIcon { Symbol = SymbolRegular.HatGraduation24 },
                TargetPageType = typeof(Views.Pages.TeachersListPage),
            },
            new NavigationViewItem()
            {
                Content = "Классы",
                Icon = new SymbolIcon { Symbol = SymbolRegular.PeopleCommunity24 },
                TargetPageType = typeof(Views.Pages.ClassesListPage)
            },
            new NavigationViewItem()
            {
                Content = "Занятия",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Book24 },
                TargetPageType = typeof(Views.Pages.LessonClassesListPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Настройки",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };

        public MainWindowViewModel(TeacherViewModel teacherViewModel, ClassViewModel classViewModel, StudentViewModel studentViewModel)
        {
            var lastNameBinding = new Binding("LastName") { Source = teacherViewModel, Mode = BindingMode.OneWay};

            var teacherMenuItem = new NavigationViewItem()
            {
                TargetPageType = typeof(Views.Pages.TeacherPage),
                Visibility = Visibility.Collapsed
            };
            teacherMenuItem.SetBinding(ContentControl.ContentProperty, lastNameBinding);
            this.MenuItems.Add(teacherMenuItem);
            
            
            var classNameBinding = new Binding("Name") { Source = classViewModel, Mode = BindingMode.OneWay};

            var classMenuItem = new NavigationViewItem()
            {
                TargetPageType = typeof(Views.Pages.ClassPage),
                Visibility = Visibility.Collapsed
            };
            classMenuItem.SetBinding(ContentControl.ContentProperty, classNameBinding);
            this.MenuItems.Add(classMenuItem);
            
            var studentBinding = new Binding("LastName") { Source = studentViewModel, Mode = BindingMode.OneWay};
            
            var studentMenuItem = new NavigationViewItem()
            {
                TargetPageType = typeof(Views.Pages.StudentPage),
                Visibility = Visibility.Collapsed
            };
            studentMenuItem.SetBinding(ContentControl.ContentProperty, studentBinding);
            this.MenuItems.Add(studentMenuItem);
        }
    }
}
