using System;

public static class Task3
{
    public static void Run()
    {
        Console.WriteLine("-- Task 3 --");
        Console.WriteLine("Enter your birth year:");

        int birthYear = int.Parse(Console.ReadLine()!);
        int age = DateTime.Now.Year - birthYear;

        Console.WriteLine($"Your age:{age}");
        Console.WriteLine($"Your age category is:{GetAgeCategory(age)}");
    }
    static string GetAgeCategory(int age)
    {
        if (age <= 17)
        {
            return $"Child";
        }
        else if (age <= 59)
        {
            return $"Adult";
        }
        else
        {
            return $"Pensioner";
        }

    }
}