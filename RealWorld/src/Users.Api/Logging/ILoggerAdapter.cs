namespace Users.Api.Logging
{
    public interface ILoggerAdapter
    {
        void LogInformation(string? message, params object?[] args);
        void LogError(Exception? ex, string? message, params object?[] args);
    }
}
