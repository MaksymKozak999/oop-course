using ClinicApp.Models;

namespace ClinicApp.Managers;

public class MedicalRecordManager
{
    private const int MaxRecords = 1000;
    private MedicalRecord[] _records = new MedicalRecord[MaxRecords];
    private int _count;

    public int Count => _count;

    public void Add(MedicalRecord record)
    {
        if (_count >= MaxRecords)
        {
            Console.WriteLine($"Досягнуто ліміт записів ({MaxRecords}).");
            return;
        }
        _records[_count++] = record;
        Console.WriteLine($"Запис [{record.Id}] {record.GetRecordType()} додано.");
    }

    public MedicalRecord? FindById(int id)
    {
        for (int i = 0; i < _count; i++)
            if (_records[i].Id == id)
                return _records[i];
        return null;
    }

    public MedicalRecord[] GetByPatient(int patientId)
    {
        int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId) n++;

        var result = new MedicalRecord[n];
        int k = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId)
                result[k++] = _records[i];
        return result;
    }

    public MedicalRecord[] GetByDoctor(int doctorId)
    {
        int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].DoctorId == doctorId) n++;

        var result = new MedicalRecord[n];
        int k = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].DoctorId == doctorId)
                result[k++] = _records[i];
        return result;
    }

    public void DisplayAll()
    {
        if (_count == 0)
        {
            Console.WriteLine("Записів немає.");
            return;
        }
        for (int i = 0; i < _count; i++)
            Console.WriteLine(_records[i]);   // поліморфізм: ToString() → GetSummary() нащадка
    }

    public void DisplayList(MedicalRecord[] list)
    {
        if (list.Length == 0)
        {
            Console.WriteLine("Записів немає.");
            return;
        }
        foreach (var r in list)
            Console.WriteLine(r);
    }

    public MedicalRecord? this[int index] =>
        index >= 0 && index < _count ? _records[index] : null;
}