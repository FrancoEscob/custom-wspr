using System.Diagnostics;

namespace CustomWspr.App.Services;

public class LoggingService
{
    private readonly string _logFilePath;

    public LoggingService()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var logDir = Path.Combine(appDataPath, "CustomWspr", "Logs");
        Directory.CreateDirectory(logDir);
        _logFilePath = Path.Combine(logDir, $"app_{DateTime.Now:yyyyMMdd}.log");
    }

    public void Log(string message)
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        var logEntry = $"[{timestamp}] {message}";
        
        Debug.WriteLine(logEntry);
        
        try
        {
            File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
        }
        catch
        {
        }
    }

    public void LogError(string message, Exception? ex = null)
    {
        var errorMsg = ex != null ? $"{message}: {ex.Message}" : message;
        Log($"ERROR: {errorMsg}");
    }
}
