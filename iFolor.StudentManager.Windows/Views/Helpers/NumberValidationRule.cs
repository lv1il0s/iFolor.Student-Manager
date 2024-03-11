using System.Globalization;
using System.Windows.Controls;

namespace iFolor.StudentManager.Windows.Views.Helpers;

/// <summary>
/// Validates that the input is a number.
/// </summary>
public class NumberValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string inputString = value as string;

        bool isNullOrWhiteSpace = string.IsNullOrWhiteSpace(inputString);
        bool isValidNumber = int.TryParse(inputString, out int _);
        bool hasLeadingMinus = inputString.StartsWith("-");
        bool isValidNegativeNumber = hasLeadingMinus && int.TryParse(inputString.Substring(1), out _);

        if (isNullOrWhiteSpace || !isValidNumber && !isValidNegativeNumber)
        {
            return new ValidationResult(false, "Please enter a valid number.");
        }

        return ValidationResult.ValidResult;
    }
}