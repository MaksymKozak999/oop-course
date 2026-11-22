using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Extensions;

internal static class DoctorExtensions
{
    public static IEnumerable<Doctor> BySpeciality(this IEnumerable<Doctor> source, Speciality speciality)
    {
        return source.Where(d => d.Speciality == speciality);
    }
    
    public static IEnumerable<Doctor> Available(this IEnumerable<Doctor> source)
    {
        return source.Where(d => d.IsAvailableNow == true);
    }
    
    public static IEnumerable<Doctor> WithAppointments(this IEnumerable<Doctor> source, IEnumerable<Appointment> appointments)	
    {
        var appointmentList = appointments.ToList();
        return source.Where(d => appointmentList.Any(a => a.DoctorId == d.Id));
    }
}