namespace ClinicApp;

internal class AppointmentManager
{
    private const int MaxAppointments = 500;
    private int _count;

    private Appointment[] _appointments { get; }
    private PatientManager _patients { get; }
    private DoctorManager _doctors { get; }

    public int Count => _count;

    public AppointmentManager(PatientManager patients,DoctorManager doctors)
    {
        _patients = patients;
        _doctors = doctors;
        _appointments = new Appointment[MaxAppointments];
    }

    public bool Book(int patientId,int doctorId,DateTime scheduledAt,int durationMinutes = 30)
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

        Appointment appointment = new Appointment(patientId,doctorId,scheduledAt,durationMinutes);

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

        string patientName;
        string doctorName;

        if (patient != null)
        {
            patientName = patient.FullName;
        }
        else
        {
            patientName = $"Пацієнт #{appointment.PatientId}";
        }

        if (doctor != null)
        {
            doctorName = doctor.FullName;
        }
        else
        {
            doctorName = $"Лікар #{appointment.DoctorId}";
        }

        Console.WriteLine(
            $"[{appointment.Id}] {patientName} → {doctorName} | " +
            $"{appointment.ScheduledAt:dd.MM.yyyy HH:mm}-" +
            $"{appointment.EndAt:HH:mm} | " +
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