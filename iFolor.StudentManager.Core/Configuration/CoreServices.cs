using FluentValidation;

using iFolor.StudentManager.Core.Contracts.Services;
using iFolor.StudentManager.Core.Models;
using iFolor.StudentManager.Core.Services;
using iFolor.StudentManager.Core.Validators;

using Microsoft.Extensions.DependencyInjection;

namespace iFolor.StudentManager.Core.Configuration;
public static class CoreServices
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddSingleton<IStudentService, StudentService>();
        services.AddTransient<IValidator<Student>, StudentValidator> ();
        return services;
    }
}
