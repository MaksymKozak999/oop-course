using System;

public static class Task4
{
    public static void Run()
    {
        Console.WriteLine("-- Task 4 --");

        Console.WriteLine("Enter systolic pressure");
        int systolic = int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter diastolic pressure");
        int diastolic = int.Parse(Console.ReadLine()!);

        Console.WriteLine(GetPressureStatus(systolic, diastolic));
    }

    static string GetPressureStatus(int systolic, int diastolic)
    {
        string category;

        if (systolic < 120 && diastolic < 80)
        {
            category = "normal";
        }
        else if (systolic < 130 && diastolic < 80)
        {
            category = "Elevated";
        }
        else if (systolic < 140 || diastolic < 90)
        {
            category = "hypertension 1 stage";
        }
        else
        {
            category = "hypertension 2 stage";
        }

        return $"Blood pressure: {systolic}/{diastolic} — {category}";
    }
}