using System;
using ClinicApp.Enums;
using ClinicApp.Utils;
using ClinicApp.Interfaces;

namespace ClinicApp.Models;

internal class Doctor : ISchedulable,IIdentifiable
{
    private static int _nextId = 1;
    public int Id { get; }

    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string _licenseNumber = string.Empty;
    private string _phone = string.Empty;

    public Speciality Speciality { get; }
    public WorkSchedule Schedule { get; set; }
    
    public bool CanSchedule(DateTime at)
    {
        return at.Hour >= Schedule.Start && at.Hour < Schedule.End;
    }
    public DateTime[] GetAvailableSlots(DateTime date, int slotCount)
    {
        ClinicValidator.ValidatePositive(slotCount, nameof(slotCount));

        int totalHours = Schedule.End -  Schedule.Start;
        int count = Math.Min(slotCount, totalHours);

        DateTime[] slots = new DateTime[count];
        for (int i = 0; i < count; i++)
        {
            slots[i] = date.Date.AddHours(Schedule.Start + i);
        }

        return slots;
    }

    public string FirstName
    {
        get => _firstName;
        private set
        {
            ClinicValidator.ValidateName(value, nameof(FirstName));
            _firstName = value;
        }
    }

    public string LastName
    {
        get => _lastName;
        private set
        {
            ClinicValidator.ValidateName(value, nameof(LastName));
            _lastName = value;
        }
    }

    public string LicenseNumber
    {
        get => _licenseNumber;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Номер ліцензії не може бути порожнім.", nameof(LicenseNumber));

            _licenseNumber = value;
        }
    }

    public string Phone
    {
        get => _phone;
        private set
        {
            ClinicValidator.ValidatePhone(value);
            _phone = value;
        }
    }
    public string FullName => $"{FirstName} {LastName}";

    public bool IsAvailableNow => Schedule.IsNow;

    public int WorkStartHour => Schedule.Start;
    public int WorkEndHour => Schedule.End;
    public int WorkingHoursPerDay => Schedule.HoursPerDay;

    public Doctor(string firstName, string lastName, string licenseNumber, string phone, WorkSchedule schedule, Speciality speciality = Speciality.General)
    {
        FirstName = firstName;
        LastName = lastName;
        LicenseNumber = licenseNumber;
        Phone = phone;
        Speciality = speciality;
        Schedule = schedule;

        Id = _nextId++;
    }
    public Doctor(string firstName, string lastName, Speciality speciality, string licenseNumber, string phone)
        : this(firstName, lastName, licenseNumber, phone, new WorkSchedule(8, 17), speciality) { }
    public bool CanAcceptAt(int hour)
    {
        return Schedule.Contains(hour);
    }
    public override string ToString()
    {
        string formattedPhone = ClinicFormatter.FormatPhone(Phone);
        string formattedSpeciality = ClinicFormatter.FormatSpeciality(Speciality);
        return $"[{Id}] Dr. {FullName} | {formattedSpeciality} | Ліцензія: {LicenseNumber} | Тел: {formattedPhone} | {Schedule}";
    }
}