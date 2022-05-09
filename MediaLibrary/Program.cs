using ConsoleApp1.ConsoleMenus.Top;
using NLog;

namespace ConsoleApp1;

internal static class Program
{
    private static Logger logger = LogManager.GetCurrentClassLogger();
    public static void Main(string[] args)
    {
        var config = new NLog.Config.LoggingConfiguration();

        // Targets where to log to: File and Console
        var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "file.txt" };

        // Rules for mapping loggers to targets            
        config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);
            
        // Apply config           
        NLog.LogManager.Configuration = config;
        
    
        new MainMenu().Run();
    }
}