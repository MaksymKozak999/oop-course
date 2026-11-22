using ClinicApp.Models;

namespace ClinicApp.Managers;

internal class AppointmentProcessor
{
    private readonly List<Action<Appointment>> _appointmentsActions = new();

    public AppointmentProcessor Run(Action<Appointment> action)
    {
        _appointmentsActions.Add(action);
        return this;
    }

    public AppointmentProcessor RunIf(Func<Appointment, bool> predicate, Action<Appointment> action)
    {
        return Run(a =>
        {
            if (predicate(a))
            {
                action(a);
            }
        });
    }

    public AppointmentProcessor Combine(Action<Appointment> first, Action<Appointment> second)
    {
        return Run(a =>
        {
            first(a);
            second(a);
        });
    }
    
    public void Execute(IEnumerable<Appointment> source)
    {
        foreach (var appointment in source)
        {
            foreach (var action in _appointmentsActions)
            {
                action(appointment);
            }
        }
    }
    
    public void Clear()
    {
        _appointmentsActions.Clear();
    }
}