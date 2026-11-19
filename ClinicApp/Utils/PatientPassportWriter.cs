using ClinicApp.Events;
using ClinicApp.Models;

namespace ClinicApp.Utils;

internal class PatientPassportWriter
{
    private readonly Clinic clinic;
    private readonly string directory;

    public PatientPassportWriter(Clinic clinic, string directory = "patients")
    {
        this.clinic = clinic;
        this.directory = directory;
        Directory.CreateDirectory(directory);
    }

    private void WritePassport(int patientId)
    {
        var patient = clinic.Patients.FindById(patientId);
        if (patient == null)
            return;

        var records = clinic.MedicalRecords.GetByPatient(patientId);

        var lines = new List<string>
        {
            $"=== Паспорт пацієнта #{patient.Id} ===",
            $"Згенеровано: {DateTime.Now:dd.MM.yyyy HH:mm}",
            "",
            $"Ім'я: {patient.FullName}",
            $"Дата народження: {patient.DateOfBirth:dd.MM.yyyy} ({patient.Age} рік)",
            $"Група крові: {patient.BloodType}   Телефон: {patient.Phone}",
            "",
            "-- Діагнози --"
        };

        foreach (var r in records)
        {
            if (r is Diagnosis)
                lines.Add(r.ToString() ?? "");
        }

        lines.Add("-- Аналізи --");
        foreach (var r in records)
        {
            if (r is LabResult)
                lines.Add(r.ToString() ?? "");
        }

        lines.Add("-- Рецепти --");
        foreach (var r in records)
        {
            if (r is Prescription)
                lines.Add(r.ToString() ?? "");
        }
        lines.Add("");

        lines.Add("-- Записи --");
        foreach (var a in clinic.Appointments.GetByPatient(patientId))
        {
            lines.Add(a.ToString() ?? "");
        }
        lines.Add("");

        lines.Add("-- Плани лікування --");
        foreach (var p in clinic.TreatmentPlans.GetByPatient(patientId))
        {
            lines.Add(p.ToString() ?? "");
        }
        lines.Add("");

        lines.Add($"Заборгованість: {clinic.Billing.GetPatientDebt(patientId):F2} грн");

        string filePath = Path.Combine(directory, $"passport_{patientId}.txt");
        File.WriteAllLines(filePath, lines);
    }
    
    public void PatientAdded(object? sender, PatientEventArgs e)
    {
        WritePassport(e.PatientId);
    }

    public void AppointmentCompleted(object? sender, AppointmentEventArgs e)
    {
        WritePassport(e.PatientId);
    }

    public void PlanCompleted(object? sender, TreatmentPlanEventArgs e)
    {
        WritePassport(e.PlanId);
    }
}