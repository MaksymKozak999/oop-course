using System;

namespace ClinicApp;

internal class Doctor
{
    private static int _nextId = 1;

    public int Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public Speciality Speciality { get; }
    public string LicenseNumber { get; }
    public string Phone { get; }

    public WorkSchedule Schedule { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    public bool IsAvailableNow => Schedule.IsNow;

    public int WorkStartHour => Schedule.Start;
    public int WorkEndHour => Schedule.End;
    public int WorkingHoursPerDay => Schedule.HoursPerDay;

    public Doctor(string firstName, string lastName, Speciality speciality, string licenseNumber, string phone, WorkSchedule schedule)
    {
        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
        Speciality = speciality;
        LicenseNumber = licenseNumber;
        Phone = phone;
        Schedule = schedule;
    }

    public Doctor(string firstName, string lastName, Speciality speciality, string licenseNumber, string phone)
        : this(firstName, lastName, speciality, licenseNumber, phone, new WorkSchedule(8, 17))
    {
    }

    public override string ToString()
    {
        string formattedPhone = ClinicFormatter.FormatPhone(Phone);
        string formattedSpeciality = ClinicFormatter.FormatSpeciality(Speciality);

        return $"[{Id}] Dr. {FullName} | {formattedSpeciality} | Ліцензія: {LicenseNumber} | Тел: {formattedPhone} | {Schedule}";
    }
}