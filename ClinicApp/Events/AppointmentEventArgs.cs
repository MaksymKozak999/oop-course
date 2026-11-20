namespace ClinicApp.Events;

public class AppointmentEventArgs : EventArgs
{
    public int AppointmentId { get; }
    public int PatientId { get; }
    public int DoctorId { get;}
    public DateTime ScheduledAt { get;}
    public string Notes { get;} 

    public AppointmentEventArgs(int appointmentId, int patientId, int doctorId,DateTime sheduleAt, string notes = "")
    {
        this.AppointmentId = appointmentId;
        this.PatientId = patientId;
        this.DoctorId = doctorId;
        this.ScheduledAt = DateTime.Now;
        this.Notes = notes;
        
    }
}