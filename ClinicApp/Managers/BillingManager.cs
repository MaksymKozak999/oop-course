using System;
using ClinicApp.Interfaces;
using ClinicApp.Models;

namespace ClinicApp.Managers;

internal class BillingManager
{
    private readonly AppointmentManager _appointments;

    public BillingManager(AppointmentManager appointments)
    {
        _appointments = appointments;
    }

    public IPayable[] GetAllUnpaid()
    {
        return FilterUnpaid(_appointments.GetAll());
    }

    public IPayable[] GetUnpaidByPatient(int patientId)
    {
        return FilterUnpaid(_appointments.GetByPatient(patientId));
    }

    public decimal GetTotalDebt()
    {
        return SumCost(GetAllUnpaid());
    }

    public decimal GetPatientDebt(int patientId)
    {
        return SumCost(GetUnpaidByPatient(patientId));
    }

    public bool PayAppointment(int appointmentId)
    {
        Appointment[] all = _appointments.GetAll();

        for (int i = 0; i < all.Length; i++)
        {
            if (all[i].Id == appointmentId)
            {
                if (all[i].IsPaid || all[i].IsCancelled)
                    return false;

                all[i].MarkPaid();
                return true;
            }
        }

        return false;
    }

    public void DisplayUnpaid(IPayable[] items)
    {
        if (items.Length == 0)
        {
            Console.WriteLine("Немає неоплачених записів.");
            return;
        }

        foreach (IPayable item in items)
        {
            Console.WriteLine($"{item} | {item.GetCost().ToString("F2")} грн");
        }
    }

    private static IPayable[] FilterUnpaid(Appointment[] source)
    {
        IPayable[] result = new IPayable[source.Length];
        int count = 0;

        for (int i = 0; i < source.Length; i++)
        {
            if (!source[i].IsPaid && !source[i].IsCancelled)
            {
                result[count] = source[i];
                count++;
            }
        }

        Array.Resize(ref result, count);
        return result;
    }

    private static decimal SumCost(IPayable[] items)
    {
        decimal total = 0m;

        foreach (IPayable item in items)
        {
            total += item.GetCost();
        }

        return total;
    }
}