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
            if (_records[i].PatientId == patientId) 
				n++;

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
            if (_records[i].DoctorId == doctorId) 
				n++;

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
            Console.WriteLine(_records[i]);   
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
	
	public Diagnosis[] GetDiagnoses(int patientId)
	{
		int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId && _records[i] is Diagnosis)
				n++;

        var result = new Diagnosis[n];
        int k = 0;
        for (int i = 0; i < _count; i++)
    	{
        Diagnosis? d = _records[i] as Diagnosis;   
        if (d != null && d.PatientId == patientId)
            result[k++] = d;
    	}
        return result;
	}
	public LabResult[] GetLabResults(int patientId)
	{
		int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId && _records[i] is LabResult)
				n++;

        var result = new LabResult[n];
        int k = 0;
        for (int i = 0; i < _count; i++)
    	{
        LabResult? l = _records[i] as LabResult;   
        if (l != null && l.PatientId == patientId)
            result[k++] = l;
    	}
        return result;
	}
	public Prescription[] GetPrescriptions(int patientId)
	{
		int n = 0;
        for (int i = 0; i < _count; i++)
            if (_records[i].PatientId == patientId && _records[i] is Prescription)
				n++;

        var result = new Prescription[n];
        int k = 0;
        for (int i = 0; i < _count; i++)
    	{
        	Prescription? p = _records[i] as Prescription;   
        	if (p != null && p.PatientId == patientId)
            	result[k++] = p;
    	}
        return result;
    }
	public Diagnosis[] GetChronicDiagnoses(int patientId)
	{
		int n = 0;
        for (int i = 0; i < _count; i++){
			Diagnosis? d = _records[i] as Diagnosis;
			if (d != null && d.PatientId == patientId && d.IsChronic )
				n++;
		}
		
        var result = new Diagnosis[n];
        int k = 0;
        for (int i = 0; i < _count; i++)
    	{
        	Diagnosis? d = _records[i] as Diagnosis;   
        	if (d != null && d.PatientId == patientId && d.IsChronic)
            	result[k++] = d;
    	}
        return result;
	}
	public Prescription[] GetActivePrescriptions(int patientId){
		int n= 0;
		for (int i = 0; i < _count; i++){
			Prescription? p = _records[i] as Prescription;
			if (p != null && p.PatientId == patientId && p.IsActive())
				n++;
		}

		var result = new Prescription[n];
        int k = 0;
        for (int i = 0; i < _count; i++)
    	{
        	Prescription? p = _records[i] as Prescription;   
        	if (p != null && p.PatientId == patientId && p.IsActive())
            	result[k++] = p;
    	}
        return result;
	
	}
	
	public void DisplayPatientSummary(int patientId)
	{
    	MedicalRecord[] all = GetByPatient(patientId);
    	Diagnosis[] diagnoses = GetDiagnoses(patientId);
    	LabResult[] labs = GetLabResults(patientId);
    	Prescription[] prescriptions = GetPrescriptions(patientId);
    	Diagnosis[] chronic = GetChronicDiagnoses(patientId);
    	Prescription[] active = GetActivePrescriptions(patientId);

    	Console.WriteLine($"=== Медична картка пацієнта #{patientId} ===");
    	Console.WriteLine($"Всього записів: {all.Length} (діагнозів: {diagnoses.Length}, аналізів: {labs.Length}, рецептів: {prescriptions.Length})");

    	if (chronic.Length > 0)
    	{
        	Console.WriteLine($"Хронічні діагнози ({chronic.Length}):");
        	foreach (Diagnosis d in chronic)
           	 Console.WriteLine($"  {d}");
    	}

    	if (active.Length > 0)
    	{
        	Console.WriteLine($"Активні рецепти ({active.Length}):");
        	foreach (Prescription p in active)
            	Console.WriteLine($"  {p}");
    	}
	}
    public MedicalRecord? this[int index] =>
        index >= 0 && index < _count ? _records[index] : null;
}