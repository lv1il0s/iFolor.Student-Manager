namespace iFolor.StudentManager.Core.Helpers;
public static class Constants
{
    public static class Validation
    {
        public static class Student
        {
            public const int MIN_AGE = 16;
            public const int MAX_AGE = 100;
        }
    }

    public static class Configuration
    {
        public static class EnvironmentVariables
        {
            public static string ENVIRONMENT_VARIABLE = "WPF_ENVIRONMENT";
        }
        public static class Environment
        {
            public static string DEVELOPMENT = "dev";
            public static string PRODUCTION = "prod";
        }
    }
}
