namespace Core.CrossCuttingConcerns.Loggers.Serilog.ConfigurationModels;

public class MsSqlConfiguration
{
    public Dictionary<string, string> ConnectionStrings { get; set; } = new();
    public required string TableName { get; set; }
    public bool AutoCreateSqlTable { get; set; }
}