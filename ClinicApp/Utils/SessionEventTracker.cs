using ClinicApp.Events;

namespace ClinicApp.Utils;

internal class SessionEventTracker
{
    private readonly Clinic _clinic;
    public int PatientsAdded { get; private set; } = 0;
    public int AppointmentsBooked { get; private set; } = 0;
    public int UrgentBooked { get; private set; } = 0;
    public int AppointmentsCancelled { get; private set; } = 0;
    public int AppointmentsCompleted { get; private set; } = 0;
    public int PaymentsReceived { get; private set; } = 0;
    public int PlansCompleted { get; private set; } = 0;

    public SessionEventTracker(Clinic clinic)
    {
        this._clinic = clinic;
    }

    public void PatientAdded(object? sender, PatientEventArgs e)
    {
        PatientsAdded++;
    }
    public void UrgentBook(object? sender, AppointmentEventArgs e)
    {
        UrgentBooked++;
    }
    public void AppointmentBooked(object? sender, AppointmentEventArgs e)
    {
        AppointmentsBooked++;
    }
    public void AppointmentCancelled(object? sender, AppointmentEventArgs e)
    {
        AppointmentsCancelled++;
        if (_clinic.WaitingRoom.Count > 0)
        {
            var next= _clinic.WaitingRoom.Peek();
            Console.WriteLine($"[ЧЕРГА] Слот звільнився. Наступний: {next}");
        }
    }
    public void AppointmentCompleted(object? sender, AppointmentEventArgs e)
    {
        AppointmentsCompleted++;
    }
    public void PaymentReceived(object? sender, PaymentEventArgs e)
    {
        PaymentsReceived++;
    }
    public void PlanCompleted(object? sender, TreatmentPlanEventArgs e)
    {
        PlansCompleted++;
    }
    
    private List<string> BuildSummaryLines()
    {
        return new List<string>
        {
            "=== Підсумок сесії ===",
            $"Пацієнтів додано:   {PatientsAdded}",
            $"Записів створено:   {AppointmentsBooked} (термінових: {UrgentBooked})",
            $"Скасовано:          {AppointmentsCancelled}",
            $"Завершено:          {AppointmentsCompleted}",
            $"Оплат:              {PaymentsReceived}",
            $"Планів завершено:   {PlansCompleted}"
        };
    }

    public void PrintSummary()
    {
        foreach (var line in BuildSummaryLines())
        {
            Console.WriteLine(line);
        }
    }

    public void SaveSummary(string path = "session_summary.txt")
    {
        var lines = BuildSummaryLines();
        lines.Add($"Сформовано: {DateTime.Now:dd.MM.yyyy HH:mm}");
        File.WriteAllLines(path, lines);
    }
}