using ClinicApp;
using ClinicApp.Models;
using System.Text;
using ClinicApp.Interfaces;
using ClinicApp.Managers;

namespace ClinicApp.Utils;

internal class ClinicExporter
{
    private string _baseDir;
    private Clinic _clinic;
    public ClinicExporter(Clinic clinic,string baseDir = "reports")
    {
        this._clinic = clinic;
        this._baseDir = baseDir;
    }
    private string GetReportDirectory()
    {
        string dir = Path.Combine(_baseDir, DateTime.Now.ToString("yyyy-MM-dd"));
        Directory.CreateDirectory(dir);
        return dir;
    }
    public string ExportPatients()
    {
        string path = Path.Combine(GetReportDirectory(), "patients.txt");

        using StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);
        writer.WriteLine("=== Пацієнти - Звіт ===");
        writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
        writer.WriteLine(new string('-', 40));
        Patient[] patients = _clinic.Patients.GetAll();

        for (int i = 0; i < patients.Length; i++)
        {
            Patient p = patients[i];
            writer.WriteLine($"{i + 1,3}. [{p.Id}] {p.FullName} | {p.Age} р. | {ClinicFormatter.FormatBloodType(p.BloodType)} | {p.Phone}");
        }
        writer.WriteLine(new string('-', 40));
        writer.WriteLine($"Всього: {patients.Length}");

        return path;
    }
    public string ExportAppointments()
    {
        string path = Path.Combine(GetReportDirectory(), "appointments.txt");

        using StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);
        writer.WriteLine("=== Записи - звіт ===");
        writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
        writer.WriteLine(new string('-', 40));
        Appointment[] appointments = _clinic.Appointments.GetAll();

        for (int i = 0; i < appointments.Length; i++)
        {
            Appointment app = appointments[i];
            writer.WriteLine($"{i + 1,3}. [{app.Id}] пацієнт #{app.PatientId} | лікар #{app.DoctorId} | {app.DurationMinutes} хв | {app.ScheduledAt:dd.MM.yyyy HH:mm}");        }
        writer.WriteLine(new string('-', 40));
        writer.WriteLine($"Всього: {appointments.Length}");

        return path;
    }
    public string ExportBilling()
    {
        string path = Path.Combine(GetReportDirectory(), "billing.txt");

        using StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);
        writer.WriteLine("=== Рахунки - Звіт ===");
        writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
        writer.WriteLine(new string('-', 40));
        IPayable[] billings = _clinic.Billing.GetAllUnpaid();

        for (int i = 0; i < billings.Length; i++)
        {
            IPayable b = billings[i];
            writer.WriteLine($"{i + 1,3}. {b.GetCost():F2} грн");
        }
        writer.WriteLine(new string('-', 40));
        writer.WriteLine($"Всього неоплачених: {billings.Length}");
        writer.WriteLine($"Загальний борг: {_clinic.Billing.GetTotalDebt():F2} грн");

        return path;
    }
    public string ExportTreatmentPlans()
    {
        string path = Path.Combine(GetReportDirectory(), "treatment_plans.txt");

        using StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8);
        writer.WriteLine("=== План Догляду - звіт ===");
        writer.WriteLine($"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}");
        writer.WriteLine(new string('-', 40));
        TreatmentPlan[] plans = _clinic.TreatmentPlans.GetAll();

        for (int i = 0; i < plans.Length; i++)
        {
            TreatmentPlan p = plans[i];
            writer.WriteLine($"{i + 1,3}. [{p.Id}] {p.PatientId} | {p.Status} | {p.Diagnosis} | {p.DurationDays}");
        }
        writer.WriteLine(new string('-', 40));
        writer.WriteLine($"Всього: {plans.Length}");

        return path;
    }
    public string[] ExportAll()
    {
        string[] paths = new string[4];
        paths[0] = ExportPatients();
        paths[1] = ExportAppointments();
        paths[2] = ExportBilling();
        paths[3] = ExportTreatmentPlans();
        return paths;
    }
}