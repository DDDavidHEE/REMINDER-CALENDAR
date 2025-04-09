using REMINDER_CALENDAR.Models;
using System;
using System.Windows;
using System.IO;

namespace REMINDER_CALENDAR.Views
{
    public partial class TaskEditWindow : Window
    {
        public TaskItem EditedTask { get; set; }
        private DateTime _selectedDate;

        public TaskEditWindow(TaskItem task, DateTime selectedDate)
        {
            InitializeComponent();
            _selectedDate = selectedDate;

            // Thiết lập mặc định cho TimePicker nếu bị null
            if (StartTimePicker.SelectedTime == null)
                StartTimePicker.SelectedTime = DateTime.Now;

            if (EndTimePicker.SelectedTime == null)
                EndTimePicker.SelectedTime = DateTime.Now.AddHours(1);

            if (task == null)
            {
                EditedTask = new TaskItem();

                // Lấy giờ mặc định an toàn từ TimePicker
                var startTime = StartTimePicker.SelectedTime.Value.TimeOfDay;
                EditedTask.StartDateTime = selectedDate.Date + startTime;
                EditedTask.EndDateTime = EditedTask.StartDateTime.AddHours(1);
            }
            else
            {
                EditedTask = task;
            }

            // Binding dữ liệu vào UI
            TitleTextBox.Text = EditedTask.Title ?? "";
            DescTextBox.Text = EditedTask.Description ?? "";
            StartDatePicker.SelectedDate = EditedTask.StartDateTime.Date;
            EndDatePicker.SelectedDate = EditedTask.EndDateTime.Date;
            StartTimePicker.SelectedTime = EditedTask.StartDateTime;
            EndTimePicker.SelectedTime = EditedTask.EndDateTime;
        }




        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Lưu tiêu đề cũ của task (nếu có, nếu không gán chuỗi rỗng)
            string oldTitle = EditedTask?.Title ?? ""; // Tránh null reference nếu không có title cũ

            // Cập nhật tiêu đề và mô tả từ UI
            EditedTask.Title = TitleTextBox.Text;
            EditedTask.Description = DescTextBox.Text;

            // Kiểm tra nếu tiêu đề trống, hiển thị lỗi
            if (string.IsNullOrWhiteSpace(EditedTask.Title))
            {
                MessageBox.Show("Tiêu đề không được để trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kiểm tra nếu tiêu đề có thay đổi
            if (!oldTitle.Equals(EditedTask.Title, StringComparison.OrdinalIgnoreCase))
            {
                // Tiêu đề đã thay đổi, thực hiện việc đổi tên file
                string taskDirectory = new TaskRepository().GetTaskDirectory();
                string oldFilePath = Path.Combine(taskDirectory, $"{oldTitle}.txt");
                string newFilePath = Path.Combine(taskDirectory, $"{EditedTask.Title}.txt");

                // Nếu file mới đã tồn tại (trùng tên), yêu cầu người dùng đổi tiêu đề khác
                if (File.Exists(newFilePath))
                {
                    MessageBox.Show($"Task với tiêu đề \"{EditedTask.Title}\" đã tồn tại. Vui lòng chọn tiêu đề khác.",
                                    "Trùng lặp", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;  // Ngừng thêm mới để tránh trùng lặp dữ liệu
                }

                // Đổi tên file
                try
                {
                    // Đổi tên file task cũ thành tên mới
                    if (File.Exists(oldFilePath))
                    {
                        File.Move(oldFilePath, newFilePath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi đổi tên file: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            // Cập nhật lại thời gian nếu cần thiết
            if (StartDatePicker.SelectedDate.HasValue && StartTimePicker.SelectedTime.HasValue)
            {
                var startDate = StartDatePicker.SelectedDate.Value;
                var startTime = StartTimePicker.SelectedTime.Value.TimeOfDay;
                EditedTask.StartDateTime = startDate.Date + startTime;
            }

            if (EndDatePicker.SelectedDate.HasValue && EndTimePicker.SelectedTime.HasValue)
            {
                var endDate = EndDatePicker.SelectedDate.Value;
                var endTime = EndTimePicker.SelectedTime.Value.TimeOfDay;
                EditedTask.EndDateTime = endDate.Date + endTime;
            }

            // Cập nhật task vào repository mà không cần xóa
            var taskRepository = new TaskRepository();
            taskRepository.UpdateTask(EditedTask);

            // Cập nhật lại danh sách task trong MainWindow
            MainWindow parentWindow = Application.Current.MainWindow as MainWindow;
            parentWindow.LoadTasksForDate(_selectedDate);  // Cập nhật lại danh sách task

            MessageBox.Show($"Task '{EditedTask.Title}' đã được cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            // Đóng cửa sổ và thông báo thành công
            this.DialogResult = true;
            this.Close();
        }




        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
