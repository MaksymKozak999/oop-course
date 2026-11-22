using ClinicApp.Models;

namespace ClinicApp.Managers;

internal class AppointmentFilter
{
    
    private Func<Appointment, bool>? _predicate;
    
    public AppointmentFilter Add(Func<Appointment, bool> predicate)
    {
        if(_predicate is null)
            _predicate = predicate;
        else
        {
            var curr = this._predicate;
            _predicate = a => curr(a) && predicate(a);
            
        }
        
        return this;
    }
    
    public AppointmentFilter And(Func<Appointment, bool> predicate)
    {
        return Add(predicate);
    }

    public AppointmentFilter Or(Func<Appointment, bool> predicate)
    {
        if (_predicate is null)
        {
            this._predicate = predicate;
        }
        else
        {
            var curr = this._predicate;
            _predicate = a => curr(a) || predicate(a);
        }

        return this;
    }
    
    public AppointmentFilter Negate()
    {
        if (_predicate is not null)
        {
            var current = _predicate;
            _predicate = a => !current(a);
        }

        return this;
    }

    public IEnumerable<Appointment> Apply(IEnumerable<Appointment> source)
    {
        if (_predicate is null)
        {
            return source;
        }
        else
        {
            return source.Where(_predicate);
        }
            
    }
    
    public void Reset()
    {
        _predicate = null;
    }
    
    
}