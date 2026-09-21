namespace ClinicApp;

internal class Patient
{
    private static int _nextId = 1;

    public int Id { get; }
    public string FirstName { get; private set; } = "Невідомий";
    public string LastName { get; private set; } = "Пацієнт";
    public DateTime DateOfBirth { get; private set; }
    public string BloodType { get; private set; } = "Невідомо";
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

    public Patient(): this("Невідомий", "Пацієнт", DateTime.Today.AddYears(-26), "Невідомо", "0000000000") { }


    public Patient(string firstName, string lastName): this(firstName, lastName, DateTime.Today, "Невідомо", "0000000000") { }


    public Patient(string firstName, string lastName, DateTime dateOfBirth, string bloodType, string phone, string email = "")
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
        return $"[{Id}] {FullName} | Вік: {Age} ({GetAgeCategory()}) | Кров: {BloodType} | Тел: {Phone}";
    }
}