   M S B A M L   `   `   `  ����  REMINDER CALENDAR4    .REMINDER_CALENDAR.Views.TaskNotificationWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Task Notification��5   	   $	��250��6#   $	��350��60    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�6.   
������� � Topmost .  � ShowInTaskbar .  ���#4A90E2$��2q�5      ��.  .��5
      �� ���#0D47A1���#1976D2$��2q�68    �� CornerRadius$ 15��6L    �� Padding$ 20q�6^   ._�5   
   �� $��Center=�6   $��Center=�60   .+�5      �� -   5         ��Name$ TaskTitleTextBlock��5      $��24(�64   $
��Bold"�6B   $��Center=�6T   
�������$
��Wrap=�6�   $��0, 0, 0, 10q�6�   �� 5      -   5        $ TaskDescriptionTextBlock��5      $��14(�6:   $��Center=�6H   $��0, 0, 0, 20q�6e   
�������$
��Wrap=�6�   �� 5      -   5        $��OK��5      $	��120��6"   $��40��6.   $��Center=�6:   ���#4A90E2
�������$
��Bold"�6   $��16(�6�   ���#4A90E2$��2q�6�   $��Bottom=�6�   $��0, 0, 20, 20q�6�   ��5      �� 5       �� 
TargetType" ��6&   .R�5      �� #��z��6!   #��z��6K   $��2q�6w   $ 20��6�   $ 5q�6�   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      ��5      �� 5      	 �� 
TargetType"	 ��6   
 �� Triggers
 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5    "   ��  �� Property � �� �� Value �#357ABD6?   �� 5!   "    �@J�Opacity$	 0.9Q�6<   5"      P� 5$       � �� . . �5%   "   ��  � �� �#2C3E506?   �� 5&   "    �@J�Opacity$ 1Q�6<   5'      5)      5+      5,      5-      5.      