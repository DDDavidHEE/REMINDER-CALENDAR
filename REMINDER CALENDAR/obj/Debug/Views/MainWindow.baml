   M S B A M L   `   `   `  ����L  HREMINDER CALENDAR, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:REMINDER_CALENDAR.ConvertersREMINDER_CALENDAR.Converters  5   	   5clr-namespace:REMINDER_CALENDARREMINDER_CALENDAR  5   	   (    "REMINDER_CALENDAR.Views.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ;conv*clr-namespace:REMINDER_CALENDAR.Converters     5   	   _ [MaterialDesignThemes.Wpf, Version=4.7.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   +localclr-namespace:REMINDER_CALENDAR  5   	     � Title$  Reminder Calendar��5   	   $	��600��5   	   $
��1000��6    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowStyle$
 None=�5
   	    � AllowsTransparency . ���#F9FAFB � 	Resources 5      9   3REMINDER_CALENDAR.Converters.CompletedTaskConverter   CompletedTaskConverter9   3REMINDER_CALENDAR.Converters.MuteTaskColorConverter  MuteTaskColorConverter��%:   &	        &	        5   
   6&     5   
   6&   5      .��5      ��  �� Padding$ 0q�6   ���#E5E7EB$��1q�6/   ���#F9FAFB._�5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� ���#1F2937$��0��6*   ._�5      ]� .+�5      �� $��REMINDER CALENDAR��6    
�������$��20(�5       $��SemiBold"�5        $��20,10q�5!       $
��Left=�5"       �� 5$       �� Orientation$ 
Horizontal=�6!   $��Right=�6:   $��0,5,10,0q�6V   .+�5%      �� -   5        $	��—��5%   !   
������ 
�������$��0q�5&   M   $��40��6a   $��40��6l   �� 5)      -   5        $	��✕��5)   !   
������ 
�������$��0q�5*   M   $��48��6a   $��42��6l   5+      5,      5-      � 50      $��1��6   $��20,10,20,20q�6     � ColumnDefinitions 51      �� 52      $��2* �6'   �� 54      $��3* �6'   .+�59      �� ���#E5E7EB$ 10q�6.   	 �� CornerRadius$	 8��6;   ���#D1D5DB$��1q�6b   ._�5:      � -   5        
 �Name$
 CalendarContainer��5:      .+�5;      &   System.Windows.Controls.Calendar  -   5          Name$ MainCalendar��5;   #   ���#E5E7EB���#111827$��0q�5>   #      DisplayDateStart$ 
2020-01-01X�5?   #      DisplayDateEnd$ 
2030-12-31X�5@   #   $	��Top=�5B   #   5C      5D      � 5G      $��1��6   $��20,0,0,0q�6'    5H      �� 5I      $
��Auto �6(   �� 5J      $��* �6(   .+�5N      �� $ 
Horizontal=�6!   $��0��6:   $��Right=�6G   $��0,0,0,10q�6c   .+�5O      �� 
�������$	 6��64   $
 10,4q�6E   $��0,0,10,0q�6T   ���#6366F1$��1q�5P   7   ._�5Q      �� -   5         ��Name$ SelectedDateText��5Q   (   $��22(�5R   (   $
��Bold"�5S   (   ���#1118275T   (   5U      �� 5V      -   5        $ TaskCountText��5V   $   $��16(�5W   $   $��Italic$�5X   $   ���#6B7280$��Center=�5Y   9   5Z      �� 5^      -   5         ��Name$ TaskListBox��5^      $��1��5_      $��0,0,0,80q�6+   $��0q�5`      
������ $
��Auto=�5b      ��5c      �� 5d      .R�5e   "   �� $��5q�6)   $ 10q�64   
����������#E5E7EB$��1q�5h   )   $	 10��5i   )   ._�5j   &   �  5k   *   �� 5l   .   $
��Auto �6?   �� 5m   .   $��* �6?   �� 5n   .   $
��Auto �6?   .+�5q   *   �� $ Vertical=�65   $��Center=�6L   .+�5r   .   �� $��SemiBold"�6O   $��16(�6e   ��68   �� *Title+��5s   2   �� 5t   6    �� 
TargetType" ��6<   .�5u   :   ��  �� Property � �� �� Value
 �   �6W   �� 5v   :    � ��$ Normal$�6V    �� Triggers 5w   :   �� 5y   >    �� Binding 6J   �� *	IsMuted+ �� Value True6f   .a�5z   B   ��  � ��
 �  ��6_   5{   ?   �� 5~   >    6J   �� *IsCompleted+ True6j   .a�5   B   ��  � ��$ Italic$�6^   �� 5�   B    � �� �#6B72806_   5�   ?   5�   7   5�   /   �� 5�   .   ���#6B7280$��13(�6�   ��68   �� *StartDateTime+ �� StringFormat$ {0:HH:mm tt}��6�   5�   +   �� 5�   *   $��1��65   �� 5�   *   -   5        $��2��5�   1   $	��⋮��6A   
������ ���#6B7280
 � Tag 5�   .   �� 5�   2    �� Path$ Id�6:    � ContextMenu 5�   .   �� 5�   2    � DataContext 6>   �� *PlacementTarget.DataContext+ �� RelativeSource �� *Self+.@�5�   6   m� -	   5        $��Check��5�   ?    6f   �� *Id+m� 5�   6   -
   5        $
��Mute��5�   ?    6d   �� *Id+m� 5�   6   -   5        $
��Edit��5�   ?    6d   �� *Id+m� 5�   6   -   5        $��Delete��5�   ?    6h   �� *Id+5�   3   5�   +   5�   '   5�   #   5�      5�      �� 5�      -   5         ��Name$ AddTaskButton��5�      $��60��5�   	   $��60��6   $��2��5�   	   $��Right=�5�   	   $��Bottom=�6%   ���#6366F1
�������$��28(�5�   	   $
��Bold"�5�   	   $��0q�5�   	   $��	0,0,20,20q�5�   	   $��+��5�   	    J� Effect 5�      3  -System.Windows.Media.Effects.DropShadowEffect  5�         Color$ 	#44000000��6/      
BlurRadius$ 8Q�6A       ShadowDepth$  2Q�6P   5�      5�      5�      5�      5�      5�      