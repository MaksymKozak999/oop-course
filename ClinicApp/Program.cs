using System;
using ClinicApp;

class Program
{
    static void Main()
    {
        Doctor d1 = new Doctor("Максим", "Козак", "Кардіологія", "LIC-001", "0441234567", 8, 16);
        Doctor d2 = new Doctor("Аарон", "Кляйне", "Неврологія", "LIC-002", "0442345678", 9, 18);
        Doctor d3 = new Doctor("Матвій", "Клаузер", "Педіатрія", "LIC-003", "0443456789", 13, 23);
        Doctor d4 = new Doctor(); // Тестовий за замовчуванням

        Console.WriteLine(d1);
        Console.WriteLine(d2);
        Console.WriteLine(d3);
        Console.WriteLine(d4);
        
    }
}