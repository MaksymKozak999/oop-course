using System;
using ClinicApp.Utils;
using ClinicApp.Enums;

namespace ClinicApp.Models;

internal class Appointment
{
    private static int _nextId = 1;

    public int Id { get; }
    public int PatientId { get;}
    public int DoctorId { get; }
    public DateTime ScheduledAt { get; set; }
    private int _durationMinutes;
    public AppointmentStatus Status {  get; private set; }
    public string Notes {  get; private set; }
    public DateTime EndsAt => ScheduledAt.AddMinutes(DurationMinutes);
    public bool IsUpcoming => ScheduledAt > DateTime.Now && Status == AppointmentStatus.Scheduled;
    public int DurationMinutes
    {
        get => _durationMinutes;
        set
        {
            ClinicValidator.ValidatePositive(value, nameof(DurationMinutes));
            _durationMinutes = value;
        }
    }

    public Appointment(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
    {
        PatientId = patientId;
        DoctorId = doctorId;
        ScheduledAt = scheduledAt;
        DurationMinutes = durationMinutes;
        Status = AppointmentStatus.Scheduled;
        Notes = "";

        Id = _nextId++;
    }
    public bool Cancel(string reason = "")
    {
        if (Status == AppointmentStatus.Scheduled)
        {
            Status = AppointmentStatus.Cancelled;

            if (reason != null && reason.Trim() != "")
                Notes = reason;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool Complete()
    {
        if (Status == AppointmentStatus.Scheduled)
        {
            Status = AppointmentStatus.Completed;
            return true;
        }
        else
        {
            return false;
        }
    }

    public override string ToString()
    {
        string result = $"[{Id}] Пацієнт #{PatientId} → Лікар #{DoctorId} | {ScheduledAt:dd.MM.yyyy HH:mm}-{EndsAt:HH:mm} | {Status}";
            
        if (Notes.Length > 0)
        {
            result += $" | {Notes}";
        }

        return result;
    }
}