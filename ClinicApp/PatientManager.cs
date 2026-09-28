using System;

namespace ClinicApp;

internal class PatientManager
{
    private const int MaxPatients = 100;
    private Patient[] _patients = new Patient[MaxPatients];
    private int _count;

    public int Count => _count;

    public Patient? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                return null;
            }

            return _patients[index];
        }
    }

    public void Add(Patient patient)
    {
        if (_count >= MaxPatients)
        {
            Console.WriteLine("Помилка: досягнуто ліміт пацієнтів.");
            return;
        }

        _patients[_count] = patient;
        _count++;
        Console.WriteLine($"Пацієнта [{patient.Id}] {patient.FirstName} {patient.LastName} додано.");
    }

    public Patient? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].Id == id)
            {
                return _patients[i];
            }
        }
        return null;
    }

    public bool TryFindById(int id, out Patient patient)
    {
        Patient? found = FindById(id);
        if (found != null)
        {
            patient = found;
            return true;
        }

        patient = null!;
        return false;
    }

    public Patient[] FindByName(string name)
    {
        if (name == null || name.Trim() == "")
        {
            return new Patient[0];
        }

        string query = name.Trim().ToLower();
        int matchCount = 0;

        for (int i = 0; i < _count; i++)
        {
            string firstName = _patients[i].FirstName.ToLower();
            string lastName = _patients[i].LastName.ToLower();

            if (firstName.Contains(query) || lastName.Contains(query))
            {
                matchCount++;
            }
        }

        Patient[] result = new Patient[matchCount];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            string firstName = _patients[i].FirstName.ToLower();
            string lastName = _patients[i].LastName.ToLower();

            if (firstName.Contains(query) || lastName.Contains(query))
            {
                result[index] = _patients[i];
                index++;
            }
        }

        return result;
    }

    public Patient[] FindByBloodType(BloodType bloodType)
    {
        int matchCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].BloodType == bloodType)
            {
                matchCount++;
            }
        }

        Patient[] result = new Patient[matchCount];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].BloodType == bloodType)
            {
                result[index] = _patients[i];
                index++;
            }
        }

        return result;
    }

    public bool Remove(int id)
    {
        int indexToRemove = -1;

        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].Id == id)
            {
                indexToRemove = i;
                break;
            }
        }

        if (indexToRemove == -1)
        {
            return false;
        }

        for (int i = indexToRemove; i < _count - 1; i++)
        {
            _patients[i] = _patients[i + 1];
        }

        Array.Clear(_patients, _count - 1, 1);
        _count--;
        return true;
    }

    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("Список пацієнтів порожній.");
            return;
        }

        Console.WriteLine($"=== Пацієнти ({_count} / {MaxPatients}) ===");
        for (int i = 0; i < _count; i++)
        {
            Patient p = _patients[i];
            string status = p.IsAdult ? "дорослий" : "дитина";
            Console.WriteLine($"[{p.Id}] {p.FirstName} {p.LastName} | Вік: {p.Age} ({status}) | кров: {p.BloodType} | Тел: {p.Phone}");
        }
    }

    public void DisplayStats()
    {
        if (_count == 0)
        {
            Console.WriteLine("Немає даних для відображення статистики.");
            return;
        }

        double totalAge = 0;
        int youngestIndex = 0;
        int oldestIndex = 0;
        int adultCount = 0;

        for (int i = 0; i < _count; i++)
        {
            totalAge += _patients[i].Age;

            if (_patients[i].Age < _patients[youngestIndex].Age)
                youngestIndex = i;

            if (_patients[i].Age > _patients[oldestIndex].Age)
                oldestIndex = i;

            if (_patients[i].IsAdult)
                adultCount++;
        }

        double averageAge = totalAge / _count;

        Console.WriteLine("=== Статистика пацієнтів ===");
        Console.WriteLine($"Всього:       {_count}");
        Console.WriteLine($"Середній вік: {averageAge:F1} р.");
        Console.WriteLine($"Наймолодший:  {_patients[youngestIndex].FirstName} {_patients[youngestIndex].LastName} ({_patients[youngestIndex].Age} р.)");
        Console.WriteLine($"Найстарший:   {_patients[oldestIndex].FirstName} {_patients[oldestIndex].LastName} ({_patients[oldestIndex].Age} р.)");
        Console.WriteLine($"Дорослих:     {adultCount} з {_count}");
        Console.WriteLine("==============================");
    }
}