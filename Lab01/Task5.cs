using System;

public static class Task5
{
    public static void Run()
    {
        Console.WriteLine("-- Task 5 --");
        Console.WriteLine("Enter day number (1-7)");
        int day = int.Parse(Console.ReadLine()!);

        string info = day switch
        {
            1 => "Monday: 08:00–18:00",
            2 => "Tuesday: 09:00–17:00",
            3 => "Wednesday: 09:00–17:00",
            4 => "Thursday: 08:00–18:00",
            5 => "Friday: 08:00–16:00",
            6 => "Saturday: 09:00–14:00",
            7 => "Sunday: day off",
            _ => "Undefined Day"
        };
        Console.WriteLine(info);
    }
}