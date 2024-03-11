using System.Xml.Serialization;

namespace iFolor.StudentManager.Infrastructure.Contracts.Dtos;

/// <summary>
/// Data transfer object for the Student entity in XML format.
/// </summary>
public class XmlStudentItemDto
{
    [XmlAttribute(AttributeName = "Id")]
    public required int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required uint Age { get; set; }

    public required string Gender { get; set; }
}
