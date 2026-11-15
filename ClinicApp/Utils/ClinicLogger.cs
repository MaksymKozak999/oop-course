using System.Text;

namespace ClinicApp.Utils;

public class ClinicLogger
{
    private readonly string _logPath;
    public ClinicLogger(string logPath = "clinic.log")
    {
        this._logPath = logPath;
    }

    private void Write(string level, string message)
    {
        string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level,-5}] {message}";
        File.AppendAllText(_logPath, line + Environment.NewLine, Encoding.UTF8);
    }
    public void LogInfo(string message)
    {
        Write("INFO", message);
    }
    
    public void LogError(string message)
    {
        Write("ERROR", message);    
    }
    public void LogWarning(string message)
    {
        Write("WARN", message);
    }

    public string[] GetLastLines(int n)
    {
        if (n <= 0)
            return Array.Empty<string>();
        
        if (!File.Exists(_logPath)) 
            return Array.Empty<string>();
        
        string[] all = File.ReadAllLines(_logPath, Encoding.UTF8);
        if(all.Length < n)
            n = all.Length;
        int startIndex = all.Length - n;
        string[] result = new string[n];
        for (int i = 0; i < n; i++)
        {
            result[i] = all[startIndex + i];
        }
        return result;
    }

    public void Clear()
    {
        if(File.Exists(_logPath))
            File.Delete(_logPath);
    }
    
    public bool Exists(){
        if(File.Exists(_logPath))
            return true;
        return false;
    }
}