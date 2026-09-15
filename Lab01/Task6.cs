using System;

public static class Task6
{
    public static void Run()
    {
        Console.WriteLine("-- Task 6 --");

        Console.WriteLine("Enter your medical card number");
        int cardNumber = int.Parse(Console.ReadLine()!);

        int lastDigit = cardNumber % 10;

        string department = lastDigit switch
        {
            0 or 1 => "general therapy",
            2 or 3 => "surgery",
            4 or 5 => "cardiology",
            6 or 7 => "neurology",
            8 or 9 => "ophthalmology",
            _ => "unknown"
        };

        string isDiscount = (cardNumber % 2 == 0) ? "yes" : "no";
        string isInspection = (cardNumber % 3 == 0) ? "yes" : "no";

        Console.WriteLine($"Department: {department}");
        Console.WriteLine($"Preferential: {isDiscount}");
        Console.WriteLine($"Inspection: {isInspection}");
    }
}