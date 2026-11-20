using ClinicApp.Models;

namespace ClinicApp.Events;

public class TreatmentPlanEventArgs : EventArgs
{
    public int PlanId { get; }
    public int PatienId { get; }
    public string Diagnosis { get; }

    public TreatmentPlanEventArgs(int planId, int patienId, string diagnosis)
    {
        this.PlanId = planId;
        this.PatienId = patienId;
        this.Diagnosis = diagnosis;
    }
}