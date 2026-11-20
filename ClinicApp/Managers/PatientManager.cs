using System;
using ClinicApp.Models;
using ClinicApp.Enums;
using ClinicApp.Events;
using ClinicApp.Utils;
namespace ClinicApp.Managers;

internal class PatientManager
{
    private List<Patient> _patients = new List<Patient>();
    public int Count => _patients.Count;

    public Patient this[int index]
    {
        get
        {
            if (index < 0 || index >= _patients.Count)
            {
                throw new IndexOutOfRangeException("Індекс виходить за межі масиву пацієнтів.");
            }

            return _patients[index];
        }
    }

    public void Add(Patient patient)
    {
       _patients.Add(patient);
       PatientAdded?.Invoke(this, new PatientEventArgs(patient.Id, patient.FullName));
       Console.WriteLine($"Пацієнта [{patient.Id}] {patient.FirstName} {patient.LastName} додано.");
    }

    public Patient? FindById(int id)
    {
        for (int i = 0; i < _patients.Count; i++)
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
        patient = FindById(id);
        return patient != null;
    }

    public Patient[] FindByName(string name)
    {
        if (name == null || name.Trim() == "")
        {
            return new Patient[0];
        }

        string query = name.Trim().ToLower();
        List<Patient> result = new List<Patient>();
        
        foreach (Patient patient in _patients)
        {
            if (patient.FirstName.ToLower().Contains(query) ||
                patient.LastName.ToLower().Contains(query))
            {
                result.Add(patient);
            }
        }

        return result.ToArray();
    }

    public Patient[] FindByBloodType(BloodType bloodType)
    {
        List<Patient> result = new List<Patient>();
        
        foreach (var patient in _patients)
        {
            if (patient.BloodType == bloodType)
            {
                result.Add(patient);
            }
        }

        return result.ToArray();
    }

    public bool Remove(int id)
    {
        Patient? patient = FindById(id);
 
        if (patient == null)
        {
            return false;
        }
 
        _patients.Remove(patient);
        return true;
    }
    
    public Patient[] GetAll()
    {
        return _patients.ToArray();
    }
    
    public void DisplayAll()
    {
        if (_patients.Count == 0)
        {
            Console.WriteLine("Список пацієнтів порожній.");
            return;
        }

        Console.WriteLine($"=== Пацієнти ({_patients.Count}) ===");
        foreach (var p in _patients)
        {
            string status = "дитина";
            if (p.IsAdult)
            {
                status = "дорослий";
            }
 
            Console.WriteLine($"[{p.Id}] {p.FirstName} {p.LastName} | Вік: {p.Age} ({status}) | кров: {ClinicFormatter.FormatBloodType(p.BloodType)} | Тел: {p.Phone}");
        }
    }

    public void DisplayStats()
    {
        if (_patients.Count == 0)
        {
            Console.WriteLine("Немає даних для відображення статистики.");
            return;
        }

        double totalAge = 0;
        int youngestIndex = 0;
        int oldestIndex = 0;
        int adultCount = 0;

        for (int i = 0; i < _patients.Count; i++)
        {
            totalAge += _patients[i].Age;

            if (_patients[i].Age < _patients[youngestIndex].Age)
                youngestIndex = i;

            if (_patients[i].Age > _patients[oldestIndex].Age)
                oldestIndex = i;

            if (_patients[i].IsAdult)
                adultCount++;
        }

        double averageAge = totalAge / _patients.Count;

        Console.WriteLine("=== Статистика пацієнтів ===");
        Console.WriteLine($"Всього:       {_patients.Count}");
        Console.WriteLine($"Середній вік: {averageAge:F1} р.");
        Console.WriteLine($"Наймолодший:  {_patients[youngestIndex].FirstName} {_patients[youngestIndex].LastName} ({_patients[youngestIndex].Age} р.)");
        Console.WriteLine($"Найстарший:   {_patients[oldestIndex].FirstName} {_patients[oldestIndex].LastName} ({_patients[oldestIndex].Age} р.)");
        Console.WriteLine($"Дорослих:     {adultCount} з {_patients.Count}");
        Console.WriteLine("==============================");
    }
    public event EventHandler<PatientEventArgs>? PatientAdded;
}