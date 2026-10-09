using ClinicApp.Models;

namespace ClinicApp.Managers;


internal class GrowablePatientManager
{
    private Patient[] _patients;
    private int _count;

    public int Count => _count;

    public int Capacity => _patients.Length;

    public GrowablePatientManager()
    {
        _patients = new Patient[4];
        _count = 0;
    }

    public void Add(Patient patient)
    {
        if (_count == Capacity)
        {
            Grow();
        }

        _patients[_count] = patient;
        _count++;

        Console.WriteLine(
            $"Додано [{patient.Id}]. " +
            $"Розмір: {_count} / {Capacity}"
        );
    }

    private void Grow()
    {
        int oldCapacity = _patients.Length;
        int newCapacity = oldCapacity * 2;

        Patient[] newPatients =
            new Patient[newCapacity];

        for (int i = 0; i < _patients.Length; i++)
        {
            newPatients[i] = _patients[i];
        }

        _patients = newPatients;

        Console.WriteLine(
            $"Масив заповнений! Розширення: " +
            $"{oldCapacity} → {newCapacity}"
        );
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

    public bool Remove(int id)
    {
        for (int i = 0; i < _count; i++)
        {
            if (_patients[i].Id == id)
            {
                for (int j = i; j < _count - 1; j++)
                {
                    _patients[j] = _patients[j + 1];
                }

                _patients[_count - 1] = default;
                _count--;

                return true;
            }
        }

        return false;
    }

    public void DisplayAll()
    {
        Console.WriteLine(
            $"Пацієнтів: {_count} / {Capacity}"
        );

        for (int i = 0; i < _count; i++)
        {
            Console.WriteLine(
                $"[{_patients[i].Id}] " +
                $"{_patients[i].FirstName} " +
                $"{_patients[i].LastName}"
            );
        }
    }
}