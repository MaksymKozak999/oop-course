namespace ClinicApp;

internal class Clinic
{
    public string Name;
    public PatientManager Patients;
    public DoctorManager Doctors;
    public AppointmentManager Appointments;

    public Clinic(string name)
    {
        Name = name;
        Patients = new PatientManager();
        Doctors = new DoctorManager();
        Appointments = new AppointmentManager(Patients, Doctors);
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