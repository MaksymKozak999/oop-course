namespace ClinicApp.Models;

sealed class SpecialistAppointment  : Appointment
{
    public string ConsultationTopic  { get; }
    
    public SpecialistAppointment(int patientId, int doctorId, DateTime scheduledAt,string consultationTopic = "", int durationMinutes = 30)
        : base(patientId, doctorId, scheduledAt, durationMinutes)
    {
        ConsultationTopic = consultationTopic;
    }

    public sealed override string GetDescription()
    {
        if (string.IsNullOrWhiteSpace(ConsultationTopic))
        {
            return "Консультація спеціалістa";
        }

        return "Консультація спеціалістa: " + ConsultationTopic ;
    }

    public override decimal GetCost()
    {
        return base.GetCost() * 1.3m;
    }

    public new int GetPriority()
    {
        return 1;
    }
}