using Core.CrossCuttingConcerns.Exceptions.ExceptionTypes;
using Core.CrossCuttingConcerns.Loggers.Serilog.ConfigurationModels;
using Core.CrossCuttingConcerns.Loggers.Serilog.ServiceBase;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace Core.CrossCuttingConcerns.Loggers.Serilog.Loggers;

public class MsSqlLogger : LoggerService
{
    public MsSqlLogger(IConfiguration configuration)
    {
        MsSqlConfiguration? logConfig = configuration.GetSection("SerilogLogConfigurations:MsSqlConfiguration")
            .Get<MsSqlConfiguration>();

        if (logConfig?.ConnectionStrings == null || !logConfig.ConnectionStrings.TryGetValue("BasakSehir", out string? value))
            throw new NotFoundException("MsSqlConfiguration not found!");

        Logger = new LoggerConfiguration().WriteTo.MSSqlServer(connectionString: value,
            sinkOptions: new MSSqlServerSinkOptions()
                { TableName = logConfig.TableName, AutoCreateSqlTable = logConfig.AutoCreateSqlTable }).CreateLogger();
    }
}