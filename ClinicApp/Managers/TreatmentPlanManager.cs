using ClinicApp.Models;
using ClinicApp.Utils;
using ClinicApp.Enums;

namespace ClinicApp.Managers;

public class TreatmentPlanManager
{
    private readonly List<TreatmentPlan> _plans = new List<TreatmentPlan>();

    public bool Add(TreatmentPlan plan)
    {
        ValidationResult validationResult = ModelValidator.Validate(plan);

        if (!validationResult.IsValid)
        {
            validationResult.Print();
            return false;
        }

        _plans.Add(plan);
        return true;
    }

    public TreatmentPlan GetById(int id)
    {
        foreach (TreatmentPlan plan in _plans)
        {
            if (plan.Id == id)
            {
                return plan;
            }
        }
        return null;
    }

    public TreatmentPlan[] GetByPatient(int patientId)
    {
        List<TreatmentPlan> resultList = new List<TreatmentPlan>();
        foreach (TreatmentPlan plan in _plans)
        {
            if (plan.PatientId == patientId)
            {
                resultList.Add(plan);
            }
        }
        return resultList.ToArray();
    }

    public TreatmentPlan[] GetByStatus(TreatmentStatus status)
    {
        List<TreatmentPlan> resultList = new List<TreatmentPlan>();
        foreach (TreatmentPlan plan in _plans)
        {
            if (plan.Status == status)
            {
                resultList.Add(plan);
            }
        }
        return resultList.ToArray();
    }

    public TreatmentPlan[] GetAll()
    {
        return _plans.ToArray();
    }
}
