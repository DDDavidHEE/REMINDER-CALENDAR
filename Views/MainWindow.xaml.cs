using REMINDER_CALENDAR.Controllers;
using REMINDER_CALENDAR.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Input;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace REMINDER_CALENDAR.Views
{
    public partial class MainWindow : Window
    {
        private ReminderCalendarController _controller;
        private DateTime _selectedDate;
        private System.Windows.Threading.DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();
            _controller = new ReminderCalendarController();

            // Mặc định ngày chọn là hôm nay
            _selectedDate = DateTime.Today;
            MainCalendar.SelectedDate = _selectedDate;

            // Gọi hàm cập nhật UI
            UpdateSelectedDateText(_selectedDate);
            LoadTasksForDate(_selectedDate);

            // Khởi tạo Timer mỗi phút check 1 lần
            _timer = new System.Windows.Threading.DispatcherTimer();
            _timer.Interval = TimeSpan.FromMinutes(1);
            _timer.Tick += CheckTaskNotification;
            _timer.Start();
        }

        private void UpdateSelectedDateText(DateTime date)
        {
            SelectedDateText.Text = date.ToString("dd MMMM yyyy");  // Hiển thị ngày
            
        }


        // Sự kiện khi bấm nút mũi tên trái (giảm năm)
        private void PrevYearButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedDate = _selectedDate.AddYears(-1); // Giảm 1 năm
            UpdateSelectedDateText(_selectedDate); // Cập nhật lại năm hiển thị
            MainCalendar.DisplayDate = new DateTime(_selectedDate.Year, MainCalendar.DisplayDate.Month, 1); // Cập nhật lại lịch
            LoadTasksForDate(_selectedDate); // Tải lại task cho năm mới
        }

        // Sự kiện khi bấm nút mũi tên phải (tăng năm)
        private void NextYearButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedDate = _selectedDate.AddYears(1); // Tăng 1 năm
            UpdateSelectedDateText(_selectedDate); // Cập nhật lại năm hiển thị
            MainCalendar.DisplayDate = new DateTime(_selectedDate.Year, MainCalendar.DisplayDate.Month, 1); // Cập nhật lại lịch
            LoadTasksForDate(_selectedDate); // Tải lại task cho năm mới
        }

        public void LoadTasksForDate(DateTime date)
        {
            var allTasks = _controller.GetTasksByDate(date).ToList();

            TaskListBox.ItemsSource = allTasks;

            int incompleteCount = allTasks.Count(t => !t.IsCompleted);
            TaskCountText.Text = $"{incompleteCount} task(s) remaining";
        }

        private void MuteTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is string taskId)
            {
                var task = _controller.GetTasksByDate(_selectedDate).FirstOrDefault(t => t.Id == taskId);
                if (task != null)
                {
                    task.IsMuted = true;
                    _controller.UpdateTask(task);  // Update task after muting
                    LoadTasksForDate(_selectedDate);  // Refresh task list

                    MessageBox.Show($"Task '{task.Title}' đã được tắt thông báo thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Create the notification window and call the StopSoundAndCloseWindow method
                    var notificationWindow = new TaskNotificationWindow(task);
                    notificationWindow.StopSoundAndCloseWindow(); // Now accessible since it is public
                }
            }
        }




        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is string taskId)
            {
                var task = _controller.GetTasksByDate(_selectedDate).FirstOrDefault(t => t.Id == taskId);
                if (task != null)
                {
                    try
                    {
                        var editWindow = new TaskEditWindow(task, _selectedDate);
                        editWindow.Owner = this;

                        if (editWindow.ShowDialog() == true && editWindow.EditedTask != null)
                        {
                            var updatedTask = editWindow.EditedTask;

                            if (task.Title != updatedTask.Title ||
                                task.StartDateTime != updatedTask.StartDateTime ||
                                task.EndDateTime != updatedTask.EndDateTime ||
                                task.Description != updatedTask.Description)
                            {
                                _controller.UpdateTask(updatedTask);
                                LoadTasksForDate(_selectedDate);

                                MessageBox.Show($"Task '{updatedTask.Title}' đã được cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi mở cửa sổ sửa task: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }



        #region Calendar & Year Buttons

        private void MainCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainCalendar.SelectedDate.HasValue)
            {
                _selectedDate = MainCalendar.SelectedDate.Value;
                UpdateSelectedDateText(_selectedDate);
                LoadTasksForDate(_selectedDate);

                
            }
        }


        private void YearButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && int.TryParse(btn.Content.ToString(), out int year))
            {
                _selectedDate = new DateTime(year, _selectedDate.Month, _selectedDate.Day);
                MainCalendar.DisplayDate = _selectedDate;
                MainCalendar.SelectedDate = _selectedDate;
            }
        }

        #endregion

        #region Task Context Menu (Check, Mute, Edit, Delete)

        private void TaskContextButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            button.ContextMenu.IsOpen = true; // Mở menu khi bấm vào
        }

        private void CheckTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is string taskId)
            {
                var task = _controller.GetTasksByDate(_selectedDate).FirstOrDefault(t => t.Id == taskId);
                if (task != null)
                {
                    task.IsCompleted = true;
                    _controller.UpdateTask(task);
                    LoadTasksForDate(_selectedDate);

                    MessageBox.Show($"Task '{task.Title}' đã được đánh dấu hoàn thành!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }





       





        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem && menuItem.Tag is string taskId)
            {
                var task = _controller.GetTasksByDate(_selectedDate).FirstOrDefault(t => t.Id == taskId);
                if (task != null)
                {
                    var result = MessageBox.Show($"Bạn có muốn xóa task: {task.Title} vào ngày {task.StartDateTime.ToString("dd/MM/yyyy HH:mm")}?",
                                                 "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        _controller.DeleteTask(task.Id);  // Xóa task từ repository
                        LoadTasksForDate(_selectedDate);  // Làm mới danh sách task trong UI
                        MessageBox.Show($"Task '{task.Title}' đã được xóa.");
                    }
                }
            }
        }

        #endregion

        #region Add Task
        // MainWindow.xaml.cs - Keep the message box here
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newTaskWindow = new TaskEditWindow(null, _selectedDate);
                newTaskWindow.Owner = this;

                bool? result = newTaskWindow.ShowDialog();

                if (result == true)
                {
                    var newTask = newTaskWindow.EditedTask;
                    var taskFilePath = Path.Combine(new TaskRepository().GetTaskDirectory(), $"{newTask.Title}.txt");

                    if (!File.Exists(taskFilePath))
                    {
                        _controller.AddTask(newTask);
                        LoadTasksForDate(_selectedDate);
                        MessageBox.Show($"Task \"{newTask.Title}\" đã được thêm mới thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Task với tiêu đề \"{newTask.Title}\" đã tồn tại trong repository.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("LỖI khi mở TaskEditWindow: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        #endregion

        #region Notification Timer
        private void CheckTaskNotification(object sender, EventArgs e)
        {
            // Kiểm tra xem có Task nào sắp đến giờ?
            var allTasks = _controller.GetTasksByDate(DateTime.Today)
                            .Where(t => !t.IsCompleted && !t.IsMuted)
                            .ToList();

            // Lấy tasks mà StartDateTime <= now < StartDateTime + 1ph (hoặc 5ph) => hiển thị thông báo
            var now = DateTime.Now;
            foreach (var task in allTasks)
            {
                // Giả sử ta hiển thị thông báo khi đến giờ StartDateTime
                if (task.StartDateTime.Date == now.Date &&
                    task.StartDateTime.Hour == now.Hour &&
                    task.StartDateTime.Minute == now.Minute)
                {
                    // Hiển thị thông báo
                    ShowTaskNotification(task);
                }
            }
        }

        private static bool isNotificationOpen = false;

        private void ShowTaskNotification(TaskItem task)
        {
            if (isNotificationOpen) return; // Prevent multiple notifications

            var notificationWindow = new TaskNotificationWindow(task);
            notificationWindow.Show();

            isNotificationOpen = true;

            // Reset the flag when the window is closed
            notificationWindow.Closed += (s, e) => isNotificationOpen = false;
        }




        #endregion

        #region Window Chrome (Drag, Minimize, Close)

        private void TitleBar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
                DragMove();
        }

        // Sự kiện Minimize
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized; // Thu nhỏ cửa sổ
        }

        // Sự kiện Maximize/Restore
        //private void Zoom_Click(object sender, RoutedEventArgs e)
        //{
        //    if (WindowState == WindowState.Normal)
        //    {
        //        // Maximize the window
        //        WindowState = WindowState.Maximized;

        //        // Apply a vertical zoom effect by scaling the calendar container and increase the font size of the numbers
        //        CalendarContainer.LayoutTransform = new ScaleTransform(1, 1.5); // Scale height by 1.5x
        //        MainCalendar.FontSize = 18; // Increase the font size for calendar numbers

        //        // Scale the "Add Task" button
        //        AddTaskButton.Width = 90;
        //        AddTaskButton.Height = 90;

        //        // Update the zoom button content
        //        ZoomButton.Content = "↔"; // Restore icon for maximized state
        //    }
        //    else
        //    {
        //        // Restore the window to normal size
        //        WindowState = WindowState.Normal;

        //        // Reset the zoom scale
        //        CalendarContainer.LayoutTransform = new ScaleTransform(1, 1); // Reset scale to normal
        //        MainCalendar.FontSize = 16; // Reset the font size

        //        // Reset the "Add Task" button size
        //        AddTaskButton.Width = 60;
        //        AddTaskButton.Height = 60;

        //        // Update the zoom button content
        //        ZoomButton.Content = "□"; // Maximize icon for normal state
        //    }
        //}





        // Sự kiện Close
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close(); // Đóng cửa sổ
        }

        #endregion
    }
}
