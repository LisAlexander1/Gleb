   M S B A M L   `   `   `  ����?  ;Gleb, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Gleb.HelpersGleb.Helpers  5      3clr-namespace:Gleb.Views.PagesGleb.Views.Pages  5      #    Gleb.Views.Pages.SettingsPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .helpersclr-namespace:Gleb.Helpers     5      0localclr-namespace:Gleb.Views.Pages     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      A =Wpf.Ui, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5         TextFillColorPrimaryBrush  N� 
Foreground#  �   5       N� Title$ SettingsPage��5        ApplicationBackgroundBrush  Wpf.Ui.Markup.Design   
Background# �  5	         
Foreground# �   5
       � 	Resources 5      )  #Gleb.Helpers.EnumToBooleanConverter  EnumToBooleanConverter��%   &	         5   
   6)   5      .��5      �� .+�5   
   �� $��20(�5      $��Medium"�5      $��Personalization��5      �� 5   
   $��0,12,0,0q�6   $��Theme��6&   	� 5   
   $��theme_light��5      $��Light��5        	� 	GroupName$ themeSelect��5!      $��0,12,0,0q�5#      ��5      �� *ViewModel.ChangeThemeCommand+ �� Mode$ OneWay=� d� 	IsChecked 5"      �� *ViewModel.CurrentTheme+ �� 	Converter# [ 	 �� ConverterParameter$	 Light��$ OneWay=�5#      	� 5$   
   $��
theme_dark��5&      $
��Dark��5'      $ themeSelect��5(      $��0,8,0,0q�5*      ��5%      �� *ViewModel.ChangeThemeCommand+$ OneWay=� 5)      �� *ViewModel.CurrentTheme+# [ $
	 Dark��$ OneWay=�5*      �� 5,   
   $��20(�5-      $��Medium"�5.      $��0,24,0,0q�5/      $��
About Gleb��50      �� 51   
   $��0,12,0,0q�6   ��6&   �� *ViewModel.AppVersion+$ OneWay=�52      53      