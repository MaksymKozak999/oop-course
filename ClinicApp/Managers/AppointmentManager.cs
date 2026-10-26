using System;
using ClinicApp.Models;
using ClinicApp.Interfaces;
namespace ClinicApp.Managers;

internal class AppointmentManager
{
    private const int MaxAppointments = 500;
    private int _count;

    private Appointment[] _appointments { get; }
    private PatientManager _patients { get; }
    private DoctorManager _doctors { get; }

    public int Count => _count;

    public Appointment[] GetAll()
    {
        Appointment[] result = new Appointment[_count];

        for (int i = 0; i < _count; i++)
        {
            result[i] = _appointments[i];
        }

        return result;

    }
    public static int CancelAll(ICancellable[] items, string reason = "")
    {
        int count = 0;

        foreach (ICancellable item in items)
        {
            if (item.Cancel(reason))
                count++;
        }

        return count;
    }
    public Appointment this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                throw new IndexOutOfRangeException("Індекс виходить за межі масиву записів.");
            }

            return _appointments[index];
        }
    }

    public AppointmentManager(PatientManager patients, DoctorManager doctors)
    {
        _patients = patients;
        _doctors = doctors;
        _appointments = new Appointment[MaxAppointments];
    }

    public bool Book(int patientId, int doctorId, DateTime scheduledAt, int durationMinutes = 30)
    {
        if (_count >= MaxAppointments)
            return false;

        if (_patients.FindById(patientId) == null)
        {
            Console.WriteLine("Пацієнта не знайдено.");
            return false;
        }

        if (_doctors.FindById(doctorId) == null)
        {
            Console.WriteLine("Лікаря не знайдено.");
            return false;
        }

        Appointment appointment = new Appointment(patientId, doctorId, scheduledAt, durationMinutes);

        _appointments[_count] = appointment;
        _count++;

        return true;
    }

    public bool Cancel(int id, string reason)
    {
        Appointment? appointment = FindById(id);

        if (appointment == null)
            return false;

        return appointment.Cancel(reason);
    }

    public bool Complete(int id)
    {
        Appointment? appointment = FindById(id);

        if (appointment == null)
            return false;

        return appointment.Complete();
    }

    public Appointment[] GetByPatient(int patientId)
    {
        Appointment[] result = new Appointment[_count];
        int resultCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].PatientId == patientId)
            {
                result[resultCount] = _appointments[i];
                resultCount++;
            }
        }

        Array.Resize(ref result, resultCount);
        return result;
    }

    public Appointment[] GetByDoctor(int doctorId)
    {
        Appointment[] result = new Appointment[_count];
        int resultCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].DoctorId == doctorId)
            {
                result[resultCount] = _appointments[i];
                resultCount++;
            }
        }

        Array.Resize(ref result, resultCount);
        return result;
    }

    public Appointment[] GetByDate(DateTime date)
    {
        Appointment[] result = new Appointment[_count];
        int resultCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].ScheduledAt.Date == date.Date)
            {
                result[resultCount] = _appointments[i];
                resultCount++;
            }
        }

        Array.Resize(ref result, resultCount);
        return result;
    }

    public Appointment[] GetByDate(int year, int month, int day)
    {
        return GetByDate(new DateTime(year, month, day));
    }

    public Appointment[] GetUpcoming()
    {
        Appointment[] result = new Appointment[_count];
        int resultCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].IsUpcoming)
            {
                result[resultCount] = _appointments[i];
                resultCount++;
            }
        }

        Array.Resize(ref result, resultCount);
        return result;
    }

    public void DisplayAppointment(Appointment appointment)
    {
        Patient? patient = _patients.FindById(appointment.PatientId);
        Doctor? doctor = _doctors.FindById(appointment.DoctorId);

        string patientName = patient != null ? patient.FullName : $"Пацієнт #{appointment.PatientId}";
        string doctorName = doctor != null ? doctor.FullName : $"Лікар #{appointment.DoctorId}";

        Console.WriteLine(
            $"[{appointment.Id}] {patientName} → {doctorName} | " +
            $"{appointment.ScheduledAt:dd.MM.yyyy HH:mm}-" +
            $"{appointment.EndsAt:HH:mm} | " +
            $"{appointment.Status}");
    }

    public void DisplayList(Appointment[] appointments)
    {
        if (appointments.Length == 0)
        {
            Console.WriteLine("Записів не знайдено.");
            return;
        }

        foreach (Appointment appointment in appointments)
        {
            DisplayAppointment(appointment);
        }
    }

    private Appointment? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_appointments[i].Id == id)
                return _appointments[i];
        }

        return null;
    }
}