using ClinicApp.Enums;
using ClinicApp.Models;

namespace ClinicApp.Managers;

internal class ReportManager
{
    private AppointmentManager _appointmentManager { get;}
    private DoctorManager _doctorManager { get;}
    private PatientManager _patientManager { get;}

    public ReportManager(AppointmentManager appointmentManager, DoctorManager doctors, PatientManager patients)
    {
        _appointmentManager = appointmentManager;
        _doctorManager = doctors;
        _patientManager = patients;
    }
    

    public IEnumerable<SpecialityReport> GetSpecialityStats()
    {
        var appointments = _appointmentManager.GetAll();

        return _doctorManager.GetAll()
            .GroupBy(d => d.Speciality)
            .Select(g =>
            {
                var doctorIds = g.Select(d => d.Id).ToList();
                var specApps = appointments.Where(a => doctorIds.Contains(a.DoctorId)).ToList();

                return new SpecialityReport(
                    g.Key,
                    g.Count(),
                    specApps.Count,
                    specApps.Sum(a => a.GetCost())
                );
            })
            .OrderByDescending(r => r.Totalrevenue);
    }

    public string? FindBusiestDoctorName()
    {
        var appointments = _appointmentManager.GetAll();

        return _doctorManager.GetAll()
            .OrderByDescending(d => appointments.Count(a => a.DoctorId == d.Id))
            .FirstOrDefault()?.FullName;
    }

    public IEnumerable<string> GetPatientsWithMultipleVisits(int minVisits)
    {
        return _appointmentManager.GetAll()
            .GroupBy(a => a.PatientId)
            .Where(g => g.Count() >= minVisits)
            .Join(
                _patientManager.GetAll(),
                g => g.Key,
                p => p.Id,
                (g, p) => p.FullName
            );
    }

    public IEnumerable<DoctorStats> GetTopEarners(int n)
    {
        var appointments = _appointmentManager.GetAll();

        return _doctorManager.GetAll().Select(d =>
        {
            var doctorApps = appointments.Where(a => a.DoctorId == d.Id).ToList();

            return new DoctorStats(
                d.Id,
                d.FullName,
                doctorApps.Count,
                doctorApps.Sum(a => a.GetCost()),
                doctorApps.Any() ? doctorApps.Max(a => a.ScheduledAt) : DateTime.MinValue
            );
        }).OrderByDescending(r => r).Take(n);
    }

    public bool HasAnyUrgentAppointments()
    {
        return _appointmentManager.GetAll()
            .Any(a => a is UrgentAppointment);
    }

    public IEnumerable<Speciality> GetActiveSpecialities()
    {
        return _doctorManager.GetAll().Select(d => d.Speciality).Distinct().OrderBy(s => s);
    }

    public IEnumerable<(int Year, int Month, decimal Total)> GetMonthlyRevenue()
    {
        return _appointmentManager.GetAll()
            .GroupBy(a => new { a.ScheduledAt.Year, a.ScheduledAt.Month })
            .OrderBy(g => g.Key.Year)
            .ThenBy(g => g.Key.Month)
            .Select(g => (g.Key.Year, g.Key.Month, g.Sum(a => a.GetCost())));
    }
}