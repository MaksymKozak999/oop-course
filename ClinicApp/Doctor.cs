namespace ClinicApp;

internal class Doctor
{
    private static int _nextId = 1;
    public int Id { get; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Speciality { get; private set; }
    public string LicenseNumber { get; private set; }
    public string Phone { get; private set; }
    public int WorkStartHour { get; private set; }
    public int WorkEndHour { get; private set; }

    public string FullName => $"{FirstName} {LastName}";

    public int WorkingHoursPerDay => WorkEndHour - WorkStartHour;
    public string WorkSchedule => $"{WorkStartHour:D2}:00-{WorkEndHour:D2}:00";

    public bool IsAvailableNow => CanAcceptAt(DateTime.Now.Hour);

    public Doctor() : this("Невідомий", "Доктор", "Невідомо", "LIC-000", "0000000000"){ }

    public Doctor(string firstName, string lastName, string speciality) : this(firstName, lastName, speciality, "LIC-000", "0000000000"){ }

    public Doctor(string firstName, string lastName, string speciality, string licenseNumber, string phone, int workStartHour = 8, int workEndHour = 17)
    {
        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
        LicenseNumber = licenseNumber;
        Phone = phone;
        WorkStartHour = workStartHour;
        WorkEndHour = workEndHour;
    }

    public bool CanAcceptAt(int hour)
    {
        return hour >= WorkStartHour && hour < WorkEndHour;
    }
    public override string ToString()
    {
        string status;
        if (IsAvailableNow)
            status = "доступний зараз";
        else
            status = "не в робочий час";

        return $"[{Id}] {FullName} | {Speciality} | {LicenseNumber} | " +
               $"Тел: {Phone} | {WorkSchedule} " +
               $"({WorkingHoursPerDay} год) | {status}";
    }
}