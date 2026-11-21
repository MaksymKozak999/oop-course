using System;
using ClinicApp.Models;

namespace ClinicApp.Managers;

internal class AnalyticsManager
{
    private AppointmentManager _appointmentManager { get;}
    private DoctorManager _doctorManager { get;}
    private PatientManager _patientManager { get;}
    
    public AnalyticsManager(AppointmentManager appointments, DoctorManager doctors, PatientManager patients)
    {
        this._patientManager = patients;
        this._appointmentManager = appointments;
        this._doctorManager = doctors;
    }

    public IEnumerable<DoctorStats> ComputeDoctorStats()
    {
        var appointments = _appointmentManager.GetAll();

        return _doctorManager.GetAll().Select(d =>
        {
            var doctorApps = appointments.Where(a => a.DoctorId == d.Id).ToList();

            return new DoctorStats(
                d.Id,
                d.FullName,
                doctorApps.Count(),
                doctorApps.Sum(a => a.GetCost()),
                doctorApps.Any() ? doctorApps.Max(a => a.ScheduledAt) : DateTime.MinValue
            );
        });
    }

    public IEnumerable<PatientStats> ComputePatientStats()
    {
        var patients = _patientManager.GetAll();

        return _patientManager.GetAll().Select(p =>
            {
                var patientApps = _appointmentManager.GetAll().Where(a => a.PatientId == p.Id).ToList();

                return new PatientStats(
                    p.Id,
                    p.FullName,
                    patientApps.Count(),
                    patientApps.Sum(a => a.GetCost()),
                    patientApps.Any() ? patientApps.Max(a => a.ScheduledAt) : DateTime.MinValue
                    
                );

            }

        );
    }
}