namespace ClinicApp.Events;

public class PaymentEventArgs : EventArgs
{
    public int AppointmentId { get; }
    public decimal Amount { get; }
    
    public PaymentEventArgs(int appointmentId, decimal amount)
    {
        this.AppointmentId = appointmentId;
        this.Amount = amount;
    }
}