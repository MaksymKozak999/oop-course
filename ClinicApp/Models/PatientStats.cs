using System.Diagnostics.CodeAnalysis;

namespace ClinicApp.Models;

public class PatientStats : IComparable<PatientStats>
{
    public int PatientId { get; }
    public string FullName { get; }
    public int VisitCount { get; }
    public decimal TotalSpent { get; }
    public DateTime LastVisitDate { get; }
    
    public PatientStats(int patientId, string fullName, int visitCount, decimal totalSpent, DateTime lastVisitDate)
    {
        this.PatientId = patientId;
        this.FullName = fullName;
        this.VisitCount = visitCount;
        this.TotalSpent = totalSpent;
        this.LastVisitDate = lastVisitDate;
    }

    public int CompareTo(PatientStats? other)
    {
        if (other == null)
            return 1;
        
        return other.VisitCount.CompareTo(VisitCount);
    }
    public override string ToString()
    {
        return $"ID: {PatientId}, Ім'я: {FullName}, Відвідування: {VisitCount},  Cума витрат: {TotalSpent}, Останній візит: {LastVisitDate}";
    }
    
    
}