using ClinicApp.Enums;
using ClinicApp.Utils;

namespace ClinicApp.Models;

public class SpecialityReport
{
    private Speciality speciality { get; }
    private int DoctorCount { get; }
    private int AppointmentCount { get; }
    private int Totalrevenue { get; }

    public SpecialityReport(Speciality speciality, int doctorCount, int appointmentCount,int totalrevenue)
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