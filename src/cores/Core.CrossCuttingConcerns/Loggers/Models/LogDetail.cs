namespace Core.CrossCuttingConcerns.Loggers.Models;

public class LogDetail
{
    public string? MethodName { get; set; }

    public string? User { get; set; }

    public List<LogParameter>? Parameters { get; set; }

}