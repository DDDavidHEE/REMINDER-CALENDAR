using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace REMINDER_CALENDAR.Converters
{
    public class CompletedTaskConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? TextDecorations.Strikethrough : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}

