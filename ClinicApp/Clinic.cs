namespace ClinicApp;

internal class Clinic
{
    private string _name;
    private PatientManager _patients;
    private DoctorManager _doctors;
    private AppointmentManager _appointments;

    public string Name => _name;
    public PatientManager Patients => _patients;
    public DoctorManager Doctors => _doctors;
    public AppointmentManager Appointments => _appointments;

    public Clinic(string name)
    {
        _name = name;
        _patients = new PatientManager();
        _doctors = new DoctorManager();
        _appointments = new AppointmentManager(_patients, _doctors);
    }

    public void DisplaySchedule(DateTime date)
    {
        Console.WriteLine($"=== Розклад на {date:dd.MM.yyyy} ===");

        Appointment[] appointmentsForDate =
            Appointments.GetByDate(date);

        Appointments.DisplayList(appointmentsForDate);
    }

    public void GenerateReport()
    {
        int upcomingAppointments =
            Appointments.GetUpcoming().Length;

        string output =
            "╔══════════════════════════════════════════════╗\n" +
           $"║  Звіт — {Name,-39}║\n" +
            "╠══════════════════════════════════════════════╣\n" +
           $"║  Пацієнтів:          {Patients.Count,-20}║\n" +
           $"║  Лікарів:            {Doctors.Count,-20}║\n" +
           $"║  Майбутніх записів:  {upcomingAppointments,-20}║\n" +
            "╠══════════════════════════════════════════════╣\n" +
            "║  Навантаження лікарів (майбутні записи):     ║\n";

        for (int i = 1; i <= Doctors.Count; i++)
        {
            Appointment[] doctorAppointments =
                Appointments.GetByDoctor(i);

            int upcomingCount = 0;

            foreach (Appointment appointment in doctorAppointments)
            {
                if (appointment.IsUpcoming)
                    upcomingCount++;
            }

            Doctor? doctor = Doctors.FindById(i);

            if (doctor != null)
            {
                output += $"║  {doctor.FullName,-20}: {upcomingCount,3} записів        ║\n";
            }
        }

        output +=
            "╚══════════════════════════════════════════════╝";

        Console.WriteLine(output);
    }
}