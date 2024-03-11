using System.Xml.Serialization;

namespace iFolor.StudentManager.Infrastructure.Contracts.Dtos;

/// <summary>
/// XML data transfer object wrapping Student DTOs.
/// </summary>
[XmlRoot("Students")]
public class XmlStudentsDto
{
    [XmlElement("Student")]
    public required List<XmlStudentItemDto> Students { get; set; }
}
