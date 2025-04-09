using System.ComponentModel;
using System;

public class TaskItem : INotifyPropertyChanged
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }

    private bool _isCompleted;
    public bool IsCompleted
    {
        get => _isCompleted;
        set
        {
            _isCompleted = value;
            OnPropertyChanged(nameof(IsCompleted));
        }
    }

    private bool _isMuted;
    public bool IsMuted
    {
        get => _isMuted;
        set
        {
            _isMuted = value;
            OnPropertyChanged(nameof(IsMuted));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


// CHANGE BACKGROUND BY USER USING OPTION
// CHANGE SOUND BY USER, OR // GIONG DOC AI,, FUN ^^ DOC TASK INSTEAD OF SHOWING NOTIFICATION 
//REPEAT TASK EVERY DAY,WEEK,PERCIFIC DAYT, WEEK TIME ...
// SORT TASK BY DATE, TIME, TITLE, STATUS ...
//DASH BOARD MANAGER TASK BY COLER , .. WHICH ONE IS FINISHED, WHICH ONE IS NOT
// SUA PHAN ZOOM CALENDAR 
