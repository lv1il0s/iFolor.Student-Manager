using System.Windows.Controls;
using System.Windows;

using iFolor.StudentManager.Core.Models;
using iFolor.StudentManager.Windows.ViewModels;

namespace iFolor.StudentManager.Windows.Converters;

/// <summary>
/// Selects appropriate data template based on the gender of the student.
/// </summary>
public class GenderTemplateSelector : DataTemplateSelector
{
    public DataTemplate MaleStudentTemplate { get; set; }
    public DataTemplate FemaleStudentTemplate { get; set; }
    public DataTemplate UnspecifiedStudentTemplate { get; set; }

    public override DataTemplate? SelectTemplate(object item, DependencyObject container)
    {
        if (item is not StudentItemViewModel student) return null;

        return student.Gender switch
        {
            Gender.Male => MaleStudentTemplate,
            Gender.Female => FemaleStudentTemplate,
            Gender.Unspecified => UnspecifiedStudentTemplate
        };
    }
}