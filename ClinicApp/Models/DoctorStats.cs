using System;

namespace ClinicApp.Models;

public class DoctorStats : IComparable<DoctorStats>
{
    public int DoctorId { get; }
    public string FullName { get; }
    public int AppointmentCount { get; }
    public decimal TotalRevenue { get; }
    public DateTime LastAppointmentDate { get; }

    public DoctorStats(int doctorId, string fullName, int appointmentCount, decimal totalRevenue, DateTime lastAppointmentDate)
    {
        this.DoctorId = doctorId;
        this.FullName = fullName;
        this.TotalRevenue = totalRevenue;
        this.LastAppointmentDate = lastAppointmentDate;
        this.AppointmentCount = appointmentCount;
    }

    public int CompareTo(DoctorStats? other)//from IComparable
    {
        if(other == null)
            return 1;
        
        return other.AppointmentCount.CompareTo(this.AppointmentCount);
    }
    
    public override string ToString()
    {
        if (LastAppointmentDate == DateTime.MinValue)
        {
            return $"[{DoctorId}] | {FullName} | Прийомів: {AppointmentCount} | Виручка: {TotalRevenue.ToString("F2")} | Останній прийом: -- ";
        }
        else
        {
            return $"[{DoctorId}] | {FullName} | Прийомів: {AppointmentCount} | Виручка: {TotalRevenue.ToString("F2")} | Останній прийом: {LastAppointmentDate}";
        }
 
    }
}