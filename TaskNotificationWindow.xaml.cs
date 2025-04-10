using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using NAudio.Wave;
using REMINDER_CALENDAR.Models;

namespace REMINDER_CALENDAR.Views
{
    public partial class TaskNotificationWindow : Window
    {
        private WaveOutEvent waveOut;
        private AudioFileReader audioFile;
        private DispatcherTimer autoCloseTimer;
        private bool isClosing = false;


        private TaskItem task;

        public TaskNotificationWindow(TaskItem task)
        {
            InitializeComponent();

            this.task = task;

            TaskTitleTextBlock.Text = task.Title;
            TaskDescriptionTextBlock.Text = task.Description;

            this.Loaded += OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string path;

                // Check if the task has a custom sound file
                if (!string.IsNullOrWhiteSpace(task.SoundFilePath) && File.Exists(task.SoundFilePath))
                {
                    // Use the custom sound file from the task
                    path = task.SoundFilePath;
                }
                else
                {
                    // Fallback to the default sound if the task doesn't have a custom sound
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Calendarsound.wav");
                }

                // Check if the sound file exists before trying to play it
                if (!File.Exists(path))
                {
                    MessageBox.Show("Audio file not found!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Play the sound
                audioFile = new AudioFileReader(path);
                waveOut = new WaveOutEvent();
                waveOut.Init(audioFile);
                waveOut.Volume = 1.0f;
                waveOut.Play();

                StartAutoCloseTimer();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing sound: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }





        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            StopSoundAndCloseWindow();
        }

        protected override void OnClosed(EventArgs e)
        {
            StopSoundAndCloseWindow();
            base.OnClosed(e);
        }

        public  void StopSoundAndCloseWindow()
        {
            if (isClosing) return;
            isClosing = true;

            try
            {
                if (waveOut != null)
                {
                    waveOut.Volume = 0.0f; // 🔇 Dập tắt âm thanh ngay lập tức
                    waveOut.Stop();
                    waveOut.Dispose();
                    waveOut = null;
                }

                audioFile?.Dispose();
                audioFile = null;

                autoCloseTimer?.Stop();
                autoCloseTimer = null;
            }
            catch { }

            if (this.IsVisible)
                this.Close();
        }

        private void StartAutoCloseTimer()
        {
            autoCloseTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(20)  // If want to play the song longer, change this value
            };
            autoCloseTimer.Tick += (s, e) =>
            {
                autoCloseTimer.Stop();
                StopSoundAndCloseWindow();
            };
            autoCloseTimer.Start();
        }
    }
}
