using Serilog;
using Serilog.Formatting.Compact;

namespace Terminal.Classes;

public static class Log
{
    public static ILogger Logger;
    
    private static ILogger CreateLogger() => 
        new LoggerConfiguration()
        .WriteTo.Console(new RenderedCompactJsonFormatter())
        .WriteTo.Debug(new RenderedCompactJsonFormatter())
        .CreateLogger();

    static Log()
    {
        Logger = CreateLogger();
    }
}