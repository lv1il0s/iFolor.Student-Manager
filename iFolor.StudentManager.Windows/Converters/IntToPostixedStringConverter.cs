using System.Globalization;
using System.Windows.Data;

namespace iFolor.StudentManager.Windows.Converters;

/// <summary>
/// Converts an integer to a string with a postfix.
/// </summary>
public class IntToPostixedStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string suffix = parameter?.ToString() ?? string.Empty;
        if (value is int intValue)
        {
            return intValue.ToString() + suffix;
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string input = value?.ToString() ?? string.Empty;
        string suffix = parameter?.ToString() ?? string.Empty;
        if (input.EndsWith(suffix))
        {
            string intValueString = input.Substring(0, input.Length - suffix.Length);
            if (int.TryParse(intValueString, out int intValue))
            {
                return intValue;
            }
        }
        return value;
    }
}
