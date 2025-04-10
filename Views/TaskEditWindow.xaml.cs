using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using NAudio.Wave;
using REMINDER_CALENDAR.Models;

namespace REMINDER_CALENDAR.Views
{
    public partial class TaskEditWindow : Window
    {
        // Declare waveOut and audioFile at the class level to be accessible in all methods
        private WaveOutEvent waveOut;
        private AudioFileReader audioFile;

        public TaskItem EditedTask { get; set; }
        private DateTime _selectedDate;

        public TaskEditWindow(TaskItem task, DateTime selectedDate)
        {
            InitializeComponent();  // This should correctly reference the XAML elements

            _selectedDate = selectedDate;

            // Ensure the elements are properly bound and initialized
            if (StartTimePicker.SelectedTime == null)
                StartTimePicker.SelectedTime = DateTime.Now;

            if (EndTimePicker.SelectedTime == null)
                EndTimePicker.SelectedTime = DateTime.Now.AddHours(1);

            if (task == null)
            {
                EditedTask = new TaskItem();

                var startTime = StartTimePicker.SelectedTime.Value.TimeOfDay;
                EditedTask.StartDateTime = selectedDate.Date + startTime;
                EditedTask.EndDateTime = EditedTask.StartDateTime.AddHours(1);
            }
            else
            {
                EditedTask = task;
            }

            // Binding data to the UI elements
            TitleTextBox.Text = EditedTask.Title ?? "";
            DescTextBox.Text = EditedTask.Description ?? "";
            StartDatePicker.SelectedDate = EditedTask.StartDateTime.Date;
            EndDatePicker.SelectedDate = EditedTask.EndDateTime.Date;
            StartTimePicker.SelectedTime = EditedTask.StartDateTime;
            EndTimePicker.SelectedTime = EditedTask.EndDateTime;
        }





        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Dừng nhạc khi nhấn Save
            StopSoundAndCloseWindow();

            // Lưu thông tin từ UI vào task
            string oldTitle = EditedTask?.Title ?? "";
            EditedTask.Title = TitleTextBox.Text;
            EditedTask.Description = DescTextBox.Text;

            if (string.IsNullOrWhiteSpace(EditedTask.Title))
            {
                MessageBox.Show("Tiêu đề không được để trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!oldTitle.Equals(EditedTask.Title, StringComparison.OrdinalIgnoreCase))
            {
                // Đổi tên file task nếu tiêu đề thay đổi
                string taskDirectory = new TaskRepository().GetTaskDirectory();
                string oldFilePath = Path.Combine(taskDirectory, $"{oldTitle}.txt");
                string newFilePath = Path.Combine(taskDirectory, $"{EditedTask.Title}.txt");

                if (File.Exists(newFilePath))
                {
                    MessageBox.Show($"Task với tiêu đề \"{EditedTask.Title}\" đã tồn tại. Vui lòng chọn tiêu đề khác.",
                                    "Trùng lặp", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                try
                {
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

            // Cập nhật thời gian cho task
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

            var taskRepository = new TaskRepository();
            taskRepository.UpdateTask(EditedTask);

            MainWindow parentWindow = Application.Current.MainWindow as MainWindow;
            parentWindow.LoadTasksForDate(_selectedDate);  // Cập nhật lại danh sách task

            MessageBox.Show($"Task '{EditedTask.Title}' đã được cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            this.DialogResult = true;
            this.Close();
        }



        // Method to stop the audio when needed
        public void StopSoundAndCloseWindow()
        {
            if (waveOut != null)
            {
                waveOut.Stop();  // Dừng nhạc nếu đang phát
                waveOut.Dispose();
                waveOut = null;
            }

            if (audioFile != null)
            {
                audioFile.Dispose();
                audioFile = null;
            }
        }

        // In TaskEditWindow.xaml.cs, add this method to open the file dialog for selecting a song
        private void ChooseSongButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Audio Files (*.mp3;*.wav)|*.mp3;*.wav";
            openFileDialog.InitialDirectory = @"C:\Users\admin\Downloads";

            if (openFileDialog.ShowDialog() == true)
            {
                string audioPath = openFileDialog.FileName;

                // Save the selected sound file path to the task
                EditedTask.SoundFilePath = audioPath;

                // Play the selected sound file immediately after selection
                try
                {
                    // Stop any previously playing sound
                    StopSound();

                    // Play the new sound
                    audioFile = new AudioFileReader(audioPath);
                    waveOut = new WaveOutEvent();
                    waveOut.Init(audioFile);
                    waveOut.Volume = 1.0f;
                    waveOut.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error playing sound: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void StopSound()
        {
            if (waveOut != null)
            {
                waveOut.Stop();  // Dừng bất kỳ âm thanh nào đang phát
                waveOut.Dispose();
                waveOut = null;
            }

            if (audioFile != null)
            {
                audioFile.Dispose();
                audioFile = null;
            }
        }





        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
