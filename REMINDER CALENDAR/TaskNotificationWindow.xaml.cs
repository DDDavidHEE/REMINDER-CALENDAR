using System;
using System.IO;
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

        public TaskNotificationWindow(TaskItem task)
        {
            InitializeComponent();

            TaskTitleTextBlock.Text = task.Title;
            TaskDescriptionTextBlock.Text = task.Description;

            this.Loaded += OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Calendarsound.wav");

                if (!File.Exists(path))
                {
                    MessageBox.Show("Không tìm thấy file âm thanh.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                audioFile = new AudioFileReader(path);
                waveOut = new WaveOutEvent();
                waveOut.Init(audioFile);
                waveOut.Volume = 1.0f;
                waveOut.Play();

                StartAutoCloseTimer();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi âm thanh: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void StopSoundAndCloseWindow()
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
                Interval = TimeSpan.FromSeconds(10)
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
