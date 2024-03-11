using iFolor.StudentManager.Core.Models;
using iFolor.StudentManager.Infrastructure.Contracts.Dtos;
using Mapster;

namespace iFolor.StudentManager.Infrastructure.Configuration;

/// <summary>
/// Configuration for mapping between domain models and DTOs.
/// </summary>
public static class MappingConfiguration
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Student, XmlStudentItemDto>.NewConfig()
            .Map(dest => dest.Gender, src => src.Gender.ToString());
        TypeAdapterConfig<XmlStudentItemDto, Student>.NewConfig()
            .Map(dest => dest.Gender, src => Enum.Parse<Gender>(src.Gender));
    }
}
