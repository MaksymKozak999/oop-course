using System;

public static class Task8
{
    public static void Run()
    {
        Console.WriteLine("-- Task 8 --");

        double weight = double.Parse(Console.ReadLine()!);
        double height = double.Parse(Console.ReadLine()!);

        double price = double.Parse(Console.ReadLine()!);
        int visits = int.Parse(Console.ReadLine()!);
        int discount = int.Parse(Console.ReadLine()!);

        int birthYear = int.Parse(Console.ReadLine()!);

        int systolic = int.Parse(Console.ReadLine()!);
        int diastolic = int.Parse(Console.ReadLine()!);

        double bmi = CalculateBMI(weight, height);
        Console.WriteLine($"BMI: {bmi:F2} -> {GetBMICategory(bmi)}");

        double cost = CalculateCost(price, visits, discount);
        Console.WriteLine($"Cost: {cost:F2} UAH");

        int age = 2026 - birthYear;
        Console.WriteLine($"Age: {age} years, category: {GetAgeCategory(age)}");

        Console.WriteLine(GetPressureStatus(systolic, diastolic));
    }

    static double CalculateBMI(double weight, double height)
    {
        return weight / (height * height);
    }

    static string GetBMICategory(double bmi)
    {
        if (bmi < 18.5)
        {
            return "Underweight";
        }
        else if (bmi < 25)
        {
            return "Normal";
        }
        else if (bmi < 30)
        {
            return "Overweight";
        }
        else
        {
            return "Obesity";
        }
    }

    static double CalculateCost(double price, int visits, int discount)
    {
        return price * visits * (1 - discount / 100.0);
    }

    static string GetAgeCategory(int age)
    {
        if (age <= 17)
        {
            return "Child";
        }
        else if (age <= 59)
        {
            return "Adult";
        }
        else
        {
            return "Pensioner";
        }
    }

    static string GetPressureStatus(int systolic, int diastolic)
    {
        string category;

        if (systolic < 120 && diastolic < 80)
        {
            category = "Normal";
        }
        else if (systolic < 130 && diastolic < 80)
        {
            category = "Elevated";
        }
        else if (systolic < 140 || diastolic < 90)
        {
            category = "Hypertension stage 1";
        }
        else
        {
            category = "Hypertension stage 2";
        }

        return $"Blood pressure: {systolic}/{diastolic} — {category}";
    }
}