   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	    REMINDER CALENDAR,   &REMINDER_CALENDAR.Views.TaskEditWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   _ [MaterialDesignThemes.Wpf, Version=4.7.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   0sys&clr-namespace:System;assembly=mscorlib  5   	     � Title$  Task Editor��5   	   $	��460��6   $	��480��6*    � WindowStartupLocation$ CenterOwner=�5   	   ���#F9FAFB.��5
      �� $��24q�6   $��Center=�6   .+�5   
   �� $��Task Editor��6   $��22(�5      $
��Bold"�5      ���#1F2937$��0,0,0,24q�5      $��Center=�5      �� 5   
   $��Title��6   $��SemiBold"�6!   ���#37415167   �� 5   
   -   5         ��Name$ TitleTextBox��5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Title��5      
����������#D1D5DB���#111827$��0,4,0,12q�5      �� 5   
   $��Description��6   $��0,0,0,4q�6'   $��SemiBold"�68   ���#3741516N   �� 5   
   -   5        $ DescTextBox��5      $��60��5       �� AcceptsReturn .  �� TextWrapping$
 Wrap=�5      $ Description��5      
����������#D1D5DB���#111827$��0,0,0,16q�5#      �� 5%   
    �� Orientation$ 
Horizontal=�6   $��0,0,0,16q�6.   $��Center=�6@   .+�5&      �� $	��200��6   .+�5'      �� $��Start Date/Time��6   $��SemiBold"�63   ���#3741516I   (  "System.Windows.Controls.DatePicker  5(      -   5          Name$ StartDatePicker��5(      $��0,4,0,6q�66   )  #MaterialDesignThemes.Wpf.TimePicker  5)      -   5          Name$ StartTimePicker��5)   ,   
	 �� Now
   SelectedTime#
 Z	 6E   5*      �� 5,      $	��200��6   $��20,0,0,0q�6%   .+�5-      �� $��End Date/Time��6   $��SemiBold"�61   ���#3741516G     5.      -   5        $ EndDatePicker��5.      $��0,4,0,6q�64     5/      -   5        $ EndTimePicker��5/   ,   #
 Z	 6C   50      51      �� 53   
   $ 
Horizontal=�6   $��Center=�6.   $��0,10,0,0q�6K   .+�54      �� -   5         ��Name$ 
SaveButton��54      $
��Save��55      $	��100��56      $��36��57      ���#6366F1
�������$��SemiBold"�5:      $��0q�5;      $	��8,0q�5<       J� Effect 5>      3  -System.Windows.Media.Effects.DropShadowEffect  5?         
BlurRadius$ 4Q�6'      ShadowDepth$ 2Q�66      Color$ 	#44000000��6F   5A      �� 5C      -   5        $ CancelButton��5C      $��Cancel��5D      $	��100��5E      $��36��5F      ���#E5E7EB���#374151$��SemiBold"�5I      $��0q�5J      $	��8,0q�5K       5M        5N      $ 4Q�6'   $ 2Q�66   $ 	#22000000��6F   5P      5Q      5R      5S      