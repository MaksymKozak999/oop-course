namespace ClinicApp.Events;

public class PatientEventArgs : EventArgs
{
    public int PatientId { get; }
    public string FullName { get; }
    
    public PatientEventArgs(int patientId, string fullName)
    {
        this.PatientId = patientId;
        this.FullName = fullName;
    }
}