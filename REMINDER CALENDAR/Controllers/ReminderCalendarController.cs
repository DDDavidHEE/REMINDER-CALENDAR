using System;
using System.Collections.Generic;
using System.Linq;
using REMINDER_CALENDAR.Models;

namespace REMINDER_CALENDAR.Controllers
{
    public class ReminderCalendarController
    {
        private TaskRepository _taskRepo;

        public ReminderCalendarController()
        {
            _taskRepo = new TaskRepository();
        }

        /// <summary>
        /// Lấy tất cả Task của một ngày (theo date).
        /// </summary>
        public List<TaskItem> GetTasksByDate(DateTime date)
        {
            return _taskRepo.GetAllTasks()
                .Where(t => t.StartDateTime.Date == date.Date)
                .OrderBy(t => t.StartDateTime)
                .ToList();
        }


        /// <summary>
        /// Thêm Task mới
        /// </summary>
        public void AddTask(TaskItem task)
        {
            if (string.IsNullOrEmpty(task.Id))
                task.Id = Guid.NewGuid().ToString();
            _taskRepo.AddTask(task);
        }

        /// <summary>
        /// Cập nhật Task
        /// </summary>
        public void UpdateTask(TaskItem updatedTask)
        {
            _taskRepo.UpdateTask(updatedTask);
        }

        /// <summary>
        /// Xoá Task
        /// </summary>
        public void DeleteTask(string taskId)
        {
            var task = _taskRepo.GetAllTasks().FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                _taskRepo.DeleteTask(task);
            }
        }

        /// <summary>
        /// Đánh dấu Task là hoàn thành (Check)
        /// </summary>
        public void CompleteTask(string taskId)
        {
            var task = _taskRepo.GetAllTasks().FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                task.IsCompleted = true;
                _taskRepo.UpdateTask(task);
            }
        }

        /// <summary>
        /// Mute Task (tắt thông báo)
        /// </summary>
        public void MuteTask(string taskId)
        {
            var task = _taskRepo.GetAllTasks().FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                task.IsMuted = true;
                _taskRepo.UpdateTask(task);
            }
        }
    }
}
