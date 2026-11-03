using System;
using ClinicApp.Utils;
using ClinicApp.Enums;
using ClinicApp.Interfaces;

namespace ClinicApp.Models;

internal class Appointment : IPayable, ICancellable,IIdentifiable
{
    private static int _nextId = 1;

    public int Id { get; }
    private bool _isPaid = false;
    public int PatientId { get;}
    public int DoctorId { get; }
    public DateTime ScheduledAt { get; set; }
    private int _durationMinutes;
    public AppointmentStatus Status {  get; private set; }
    public string Notes {  get; private set; }
    public DateTime EndsAt => ScheduledAt.AddMinutes(DurationMinutes);
    public bool IsUpcoming => ScheduledAt > DateTime.Now && Status == AppointmentStatus.Scheduled;
    public bool IsPaid => _isPaid;
    public bool IsCancelled => Status == AppointmentStatus.Cancelled;
    // Варіант А: множник знижки в базовому класі Appointment.
    // Знижка не залежить від типу прийому, тому вона знаходиться в базовій ціні.Підкласи не змінюються
    // (їх base.GetCost() вже включають знижку), а створення нових видів знижок не потребує створення нових класів
    // а тільки нове значення множника.
    private decimal _discountMultiplier = 1m;

    public decimal DiscountMultiplier
    {
        get => _discountMultiplier;
        set
        {
            if (value <= 0m || value > 1m)
            {
                throw new ArgumentOutOfRangeException(nameof(DiscountMultiplier),
                    "Множник знижки має бути в межах (0; 1].");
            }

            _discountMultiplier = value;
        }
    }
    public string CancellationReason
    {
        get
        {
            if (IsCancelled)
            {
                return Notes;
            }

            return "";
        }
    }

    public virtual decimal GetCost()
    {
        return DurationMinutes * 10m * DiscountMultiplier;
    }

    public virtual string GetDescription()
    {
        return "Прийом";
    }

    public int GetPriority()
    {
        return 3;
    }

    public void MarkPaid()
    {
        if (!IsCancelled)
        {
            _isPaid = true;
        }
    }
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
        string result = $"[{Id}] {GetDescription()} | Пацієнт #{PatientId} → Лікар #{DoctorId} | " +
                        $"{ScheduledAt:dd.MM.yyyy HH:mm}-{EndsAt:HH:mm} | {Status}";

        if (Notes.Length > 0)
        {
            result += $" | {Notes}";
        }

        result += $" | {GetCost().ToString("F2")} грн";
        return result;
    }
}