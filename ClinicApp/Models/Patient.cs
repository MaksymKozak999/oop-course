using ClinicApp.Enums;
using System;
using ClinicApp.Utils;
namespace ClinicApp.Models;

internal class Patient
{
    private static int _nextId = 1;

    public int Id { get; }
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private DateTime _dateOfBirth;
    private string _phone = string.Empty;
    public BloodType BloodType { get; private set; } = BloodType.Unknown;
    public string Email { get; private set; } = string.Empty;

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

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        private set
        {
           ClinicValidator.ValidateDate(value, nameof(DateOfBirth));
            _dateOfBirth = value;
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

    public int Age
    {
        get
        {
            DateTime today = DateTime.Today;
            int age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }

    public bool IsAdult => Age >= 18;

    public Patient(): this("Невідомий", "Пацієнт", DateTime.Today.AddYears(-26), BloodType.Unknown, "0000000000",string.Empty) { }


    public Patient(string firstName, string lastName): this(firstName, lastName, DateTime.Today, BloodType.Unknown, "0000000000", string.Empty) { }


    public Patient(string firstName, string lastName, DateTime dateOfBirth, BloodType bloodType, string phone, string email = "")
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        BloodType = bloodType;
        Phone = phone;
        Email = email;

        Id = _nextId++;
    }

    public string GetAgeCategory()
    {
        if (Age < 18)
        {
            return "дитина";
        }
        if (Age < 60)
        {
            return "дорослий";
        }
        return "літній";
    }

    public override string ToString()
    {
        return $"[{Id}] {FullName} | Вік: {ClinicFormatter.FormatAge(Age)} ({GetAgeCategory()}) | Кров: {ClinicFormatter.FormatBloodType(BloodType)} | Тел: {ClinicFormatter.FormatPhone(Phone)}";
    }
}