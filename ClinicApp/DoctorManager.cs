using System;

namespace ClinicApp;

internal class DoctorManager
{
    private const int MaxDoctors = 50;
    private Doctor[] _doctors = new Doctor[MaxDoctors];
    private int _count;

    public int Count => _count;

    public Doctor? this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                return null;
            }

            return _doctors[index];
        }
    }

    public void Add(Doctor doctor)
    {
        if (_count >= MaxDoctors)
        {
            Console.WriteLine("Помилка: досягнуто ліміт докторів.");
            return;
        }

        _doctors[_count] = doctor;
        _count++;
        Console.WriteLine($"Доктора [{doctor.Id}] {doctor.FirstName} {doctor.LastName} додано.");
    }

    public Doctor? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Id == id)
            {
                return _doctors[i];
            }
        }
        return null;
    }

    public bool TryFindById(int id, out Doctor doctor)
    {
        Doctor? found = FindById(id);
        if (found != null)
        {
            doctor = found;
            return true;
        }

        doctor = null!;
        return false;
    }

    public Doctor[] FindBySpeciality(string speciality)
    {
        if (speciality == null || speciality.Trim() == "")
        {
            return new Doctor[0];
        }

        string query = speciality.Trim().ToLower();
        int matchCount = 0;

        for (int i = 0; i < _count; i++)
        {
            string s = _doctors[i].Speciality.ToString().ToLower();

            if (s.Contains(query))
            {
                matchCount++;
            }
        }

        Doctor[] result = new Doctor[matchCount];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            string s = _doctors[i].Speciality.ToString().ToLower();

            if (s.Contains(query))
            {
                result[index] = _doctors[i];
                index++;
            }
        }

        return result;
    }

    public Doctor[] FindBySpeciality(Speciality speciality)
    {
        int matchCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality == speciality)
            {
                matchCount++;
            }
        }

        Doctor[] result = new Doctor[matchCount];
        int index = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].Speciality == speciality)
            {
                result[index] = _doctors[i];
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
            if (_doctors[i].Id == id)
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
            _doctors[i] = _doctors[i + 1];
        }

        Array.Clear(_doctors, _count - 1, 1);
        _count--;
        return true;
    }

    public Doctor[] GetAll()
    {
        Doctor[] result = new Doctor[_count];
        Array.Copy(_doctors, result, _count);
        return result;
    }

    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("Список лікарів порожній.");
            return;
        }

        Console.WriteLine($"=== Лікарі ({_count} / {MaxDoctors}) ===");

        for (int i = 0; i < _count; i++)
        {
            Doctor d = _doctors[i];

            string status;
            if (d.IsAvailableNow)
                status = "доступний зараз";
            else
                status = "не в робочий час";

            Console.WriteLine(
                $"[{d.Id}] {d.FirstName} {d.LastName} | " +
                $"{d.Speciality} | {d.LicenseNumber} | " +
                $"Тел: {d.Phone} | " +
                $"{d.WorkStartHour:D2}:00-{d.WorkEndHour:D2}:00 " +
                $"({d.WorkingHoursPerDay} год) | {status}"
            );
        }
    }

    public void DisplayStats()
    {
        if (_count == 0)
        {
            Console.WriteLine("Немає даних для відображення статистики.");
            return;
        }

        int availableCount = 0;

        for (int i = 0; i < _count; i++)
        {
            if (_doctors[i].IsAvailableNow)
            {
                availableCount++;
            }
        }

        Console.WriteLine("=== Статистика лікарів ===");
        Console.WriteLine($"Всього:           {_count}");
        Console.WriteLine($"Доступні зараз:   {availableCount}");
        Console.WriteLine("По спеціальностях:");

        for (int i = 0; i < _count; i++)
        {
            bool alreadyShown = false;

            for (int j = 0; j < i; j++)
            {
                if (_doctors[j].Speciality == _doctors[i].Speciality)
                {
                    alreadyShown = true;
                    break;
                }
            }

            if (alreadyShown)
            {
                continue;
            }

            int specialityCount = 0;

            for (int j = 0; j < _count; j++)
            {
                if (_doctors[j].Speciality == _doctors[i].Speciality)
                {
                    specialityCount++;
                }
            }

            Console.WriteLine($"    {_doctors[i].Speciality}: {specialityCount}");
        }

        Console.WriteLine("==========================");
    }
}