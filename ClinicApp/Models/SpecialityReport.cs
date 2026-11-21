using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class SpecialityReport
{
    public Speciality speciality { get; }
    public int DoctorCount { get; }
    public int AppointmentCount { get; }
    public decimal Totalrevenue { get; }

    public SpecialityReport(Speciality speciality, int doctorCount, int appointmentCount,decimal totalrevenue)
    {
        this.speciality = speciality;
        this.DoctorCount = doctorCount;
        this.AppointmentCount = appointmentCount;
        this.Totalrevenue = totalrevenue;
    }

    public override string ToString()
    {
        string text = $"{ClinicFormatter.FormatSpeciality(speciality)} | Лікарів: {DoctorCount} | Прийомів: {AppointmentCount} | Виручка: {Totalrevenue:F2} грн";
        return text;
    }
    
}