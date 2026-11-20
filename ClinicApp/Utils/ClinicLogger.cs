using System.Text;
using ClinicApp.Events;
using ClinicApp.Utils;

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
    
    public void OnPatientAdded(object? sender, PatientEventArgs e)
    {
        LogInfo($"Новий пацієнт #{e.PatientId}: {e.FullName}");
    }

    public void OnAppointmentBooked(object? sender, AppointmentEventArgs e)
    {
        LogInfo($"Запис #{e.AppointmentId} створено: пацієнт #{e.PatientId} → лікар #{e.DoctorId}");
    }

    public void OnAppointmentCancelled(object? sender, AppointmentEventArgs e)
    {
        LogInfo($"Запис #{e.AppointmentId} скасовано: {e.Notes}");
    }

    public void OnAppointmentCompleted(object? sender, AppointmentEventArgs e)
    {
        LogInfo($"Запис #{e.AppointmentId} завершено");
    }

    public void OnPaymentReceived(object? sender, PaymentEventArgs e)
    {
        LogInfo($"Оплата запису #{e.AppointmentId}: {e.Amount:F2} грн");
    }

    public void OnPlanCompleted(object? sender, TreatmentPlanEventArgs e)
    {
        LogInfo($"План лікування #{e.PlanId} завершено: {e.Diagnosis}");
    }

    public void OnUrgentAppointmentBooked(object? sender, AppointmentEventArgs e)
    {
        LogWarning($"ТЕРМІНОВИЙ запис #{e.AppointmentId}: {e.Notes}");

        Directory.CreateDirectory("alerts");
        string path = Path.Combine("alerts", $"urgent_{DateTime.Now:yyyy-MM-dd}.txt");
        string line = $"[{DateTime.Now:HH:mm:ss}] Запис #{e.AppointmentId}: пацієнт #{e.PatientId} → лікар #{e.DoctorId}, {e.Notes}";
        File.AppendAllText(path, line + Environment.NewLine, Encoding.UTF8);
    }
}