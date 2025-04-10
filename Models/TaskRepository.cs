using Newtonsoft.Json;
using REMINDER_CALENDAR.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace REMINDER_CALENDAR.Models
{
    public class TaskRepository
    {
        private readonly string _taskDirectory;

        public TaskRepository()
        {
            // Tạo đường dẫn đến thư mục "Tasks"
            _taskDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Tasks");

            // Loại bỏ dòng MessageBox về đường dẫn thư mục
            // MessageBox.Show($"Task Directory: {_taskDirectory}");  // Xóa dòng này

            // Kiểm tra nếu thư mục không tồn tại và tạo mới
            if (!Directory.Exists(_taskDirectory))
            {
                Directory.CreateDirectory(_taskDirectory);
            }
        }


        // Thêm phương thức GetTaskDirectory để trả về thư mục chứa task
        public string GetTaskDirectory()
        {
            return _taskDirectory;
        }
        // Lấy tất cả task
        public List<TaskItem> GetAllTasks()
        {
            List<TaskItem> tasks = new List<TaskItem>();

            foreach (var filePath in Directory.GetFiles(_taskDirectory, "*.txt"))
            {
                try
                {
                    var json = File.ReadAllText(filePath);
                    var task = JsonConvert.DeserializeObject<TaskItem>(json);
                    if (task != null)
                    {
                        tasks.Add(task);
                    }
                }
                catch (Exception ex)
                {
                    // Đảm bảo không bị lỗi nếu có lỗi khi đọc file
                    MessageBox.Show($"Lỗi khi đọc tệp: {ex.Message}");
                }
            }

            return tasks;
        }
        // Thêm một task mới
        public void AddTask(TaskItem task)
        {
            string taskFilePath = Path.Combine(_taskDirectory, $"{task.Title}.txt");

            // Kiểm tra xem task có tồn tại trong repository không
            if (File.Exists(taskFilePath))
            {
                
                return; // Ngừng thực hiện nếu task đã tồn tại
            }

            // Nếu task chưa tồn tại, thực hiện thêm task mới
            try
            {
                var json = JsonConvert.SerializeObject(task, Formatting.Indented);
                File.WriteAllText(taskFilePath, json);  // Tạo tệp mới với task đã thêm

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu task: " + ex.Message);
            }
        }





        // Cập nhật một task
        public void UpdateTask(TaskItem updatedTask)
        {
            string taskFilePath = Path.Combine(_taskDirectory, $"{updatedTask.Title}.txt");

            // Kiểm tra xem task có tồn tại trong repository không
            if (File.Exists(taskFilePath))
            {
                try
                {
                    var json = JsonConvert.SerializeObject(updatedTask, Formatting.Indented);
                    File.WriteAllText(taskFilePath, json);  // Cập nhật task trong file

                    // Chỉ thông báo một lần khi task được cập nhật
                    MessageBox.Show($"Task \"{updatedTask.Title}\" đã được cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật task: " + ex.Message);
                }
            }
            else
            {
                // Không hiển thị thông báo gì nếu task không tồn tại
            }
        }
        // Xóa một task
        public void DeleteTask(TaskItem task)
        {
            string taskFilePath = Path.Combine(_taskDirectory, $"{task.Title}.txt");

            // Kiểm tra xem task có tồn tại trong repository không
            if (File.Exists(taskFilePath))
            {
                try
                {
                    File.Delete(taskFilePath);  // Xóa task từ repository
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa task: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Task không tồn tại trong repository.");
            }
        }
    }
}
