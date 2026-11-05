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
        foreach (var doctor in _doctorManager.GetAll())
        {
            int count = 0;
            decimal sum = 0;
            DateTime lastDate = DateTime.MinValue;

            foreach (var appointment in _appointmentManager.GetAll())
            {
                if (appointment.DoctorId == doctor.Id)
                {
                    count++;
                    sum += appointment.GetCost();
                    if (appointment.ScheduledAt > lastDate)
                    {
                        lastDate =  appointment.ScheduledAt;
                    }
                }
            }

            DoctorStats dstat = new(doctor.Id, doctor.FullName, count,sum, lastDate);
            yield return dstat;
        }
    }

    public IEnumerable<PatientStats> ComputePatientStats()
    {
        foreach (var patient in _patientManager.GetAll())
        {
            int count = 0;
            decimal sum = 0;
            DateTime lastDate = DateTime.MinValue;

            foreach (var appointment in _appointmentManager.GetAll())
            {
                if (appointment.PatientId == patient.Id)
                {
                    count++;
                    sum += appointment.GetCost();
                    if (appointment.ScheduledAt > lastDate)
                    {
                        lastDate = appointment.ScheduledAt;
                    }
                }
            }

            PatientStats pstat = new(patient.Id, patient.FullName, count,sum, lastDate);
            yield return pstat;
        }
    }
}