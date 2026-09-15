using System;
using ClinicApp;

class Program
{
    static void Main()
    {
        Patient p1 = new Patient("Іван", "Петренко", new DateTime(1985, 4, 12), "A+", "0501234567");
        Patient p2 = new Patient("Аарон", "Кляйне", new DateTime(1993, 8, 25), "B-", "0672345678");
        Patient p3 = new Patient("Максим", "Козак", new DateTime(2010, 1, 15), "O+", "0933456789");
        Patient p4 = new Patient();
        Patient p5 = new Patient("Матвій", "Клаузер");

        Console.WriteLine(p1);
        Console.WriteLine(p2);
        Console.WriteLine(p3);
        Console.WriteLine(p4);
        Console.WriteLine(p5);
    }
}