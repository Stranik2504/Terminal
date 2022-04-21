using Serilog;
using Serilog.Formatting.Compact;

namespace Terminal.Classes;

public static class Log
{
    public static ILogger Logger;
    
    private static ILogger CreateLogger() => 
        new LoggerConfiguration()
        .MinimumLevel.Information()
        .WriteTo.Console(new CompactJsonFormatter())
        .CreateLogger();

    static Log()
    {
        Logger = CreateLogger();
    }
}