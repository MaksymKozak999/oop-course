namespace ClinicApp.Utils;

public class ImportResult
{
    public int Imported { get; private set; }
    public int Skipped { get; private set; }
    private List<string> _errors = new List<string>();

    public void AddSuccess()
    {
        Imported++;
    }

    public void AddError(int lineNumber, string reason)
    {
        Skipped++;
        _errors.Add($"Рядок {lineNumber}: {reason}");
    }
    
    public void Print()
    {
        Console.WriteLine($"Імпортовано: {Imported} | Пропущено: {Skipped}");
        foreach (string error in _errors)
        {
            Console.WriteLine($"  {error}");
        }
        
    }
}