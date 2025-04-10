using System;
using System.Collections.Generic;
using System.Linq;
using REMINDER_CALENDAR.Models;
using System.IO;
using System.Windows;
using System.Media;
using NAudio.Wave;

namespace REMINDER_CALENDAR.Controllers
{
    public class ReminderCalendarController
    {
        private TaskRepository _taskRepo;

        public ReminderCalendarController()
        {
            _taskRepo = new TaskRepository();
        }

        
        public List<TaskItem> GetTasksByDate(DateTime date)
        {
            return _taskRepo.GetAllTasks()
                .Where(t => t.StartDateTime.Date == date.Date)
                .OrderBy(t => t.StartDateTime)
                .ToList();
        }


        
        public void AddTask(TaskItem task)
        {
            if (string.IsNullOrEmpty(task.Id))
                task.Id = Guid.NewGuid().ToString();
            _taskRepo.AddTask(task);
        }

        
        public void UpdateTask(TaskItem updatedTask)
        {
            _taskRepo.UpdateTask(updatedTask);
        }

        
        public void DeleteTask(string taskId)
        {
            var task = _taskRepo.GetAllTasks().FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                _taskRepo.DeleteTask(task);
            }
        }


        public void CompleteTask(string taskId)
        {
            var task = _taskRepo.GetAllTasks().FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                task.IsCompleted = true;
                _taskRepo.UpdateTask(task);

                // Play sound for completed task
                PlayCompletionSound(task);
            }
        }



        private void PlayCompletionSound(TaskItem task)
        {
            var audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "your-audio-file.wav");

            if (File.Exists(audioPath))
            {
                try
                {
                    var audioFile = new AudioFileReader(audioPath);
                    var waveOut = new WaveOutEvent();
                    waveOut.Init(audioFile);
                    waveOut.Volume = 1.0f;
                    waveOut.Play();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error playing sound: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Audio file not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



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
