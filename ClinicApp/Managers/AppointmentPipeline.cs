using ClinicApp.Models;

namespace ClinicApp.Managers;

internal class AppointmentPipeline
{
    private readonly AppointmentProcessor  _appointmentProcessor;
    private readonly AppointmentFilter _appointmentFilter;

    public AppointmentPipeline()
    {
        this._appointmentProcessor = new AppointmentProcessor();
        this._appointmentFilter = new AppointmentFilter();
    }

    public AppointmentPipeline Filter(Func<Appointment, bool> predicate)
    {
        _appointmentFilter.Add(predicate);
        return this;
    }
    
    public AppointmentPipeline Then(Action<Appointment> action)
    {
        _appointmentProcessor.Run(action);
        return this;
    }

    public int Execute(IEnumerable<Appointment> source)
    {
        var filtered = _appointmentFilter.Apply(source).ToArray();
        _appointmentProcessor.Execute(filtered);

        return filtered.Length;
    }
    
    public void Reset()
    {
        _appointmentFilter.Reset();
        _appointmentProcessor.Clear();
    }
}