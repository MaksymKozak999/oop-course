namespace ClinicApp;

using static BloodType;

internal class Patient
{
    private static int _nextId = 1;

    public int Id { get; }
    public string FirstName { get; private set; } = "Невідомий";
    public string LastName { get; private set; } = "Пацієнт";
    public DateTime DateOfBirth { get; private set; }
    public BloodType BloodType { get; private set; } = BloodType.Unknown;
    public string Phone { get; private set; } = "0000000000";
    public string Email { get; private set; } = string.Empty;

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
        this.Id = _nextId++;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.DateOfBirth = dateOfBirth;
        this.BloodType = bloodType;
        this.Phone = phone;
        this.Email = email;
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