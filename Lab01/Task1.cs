using System;

public static class Task1
{
    public static void Run()
    {
        Console.WriteLine("-- Task 1 --");
        Console.WriteLine("Enter your height in meters: ");
        double height = double.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter your weight: ");
        double weight = double.Parse(Console.ReadLine()!);

        double bmi = CalculateBMI(weight, height);

        Console.WriteLine($"BMI: {bmi:F2} -> {GetBMICategory(bmi)}");

    }
    static double CalculateBMI(double weight, double height)
    {
        double bmi = weight / (height * height);

        return bmi;
    }
    static string GetBMICategory(double bmi)
    {
        if (bmi <= 18.4)
        {
            return "Underweight";
        }
        else if (bmi >= 18.5 && bmi <= 24.99)
        {
            return "Congratulations! Ideal Weight!";
        }
        else if (bmi >= 25.0 && bmi <= 29.99)
        {
            return "Overweight";
        }
        else if (bmi >= 30.0 && bmi <= 34.99)
        {
            return "Class I Obesity";
        }
        else if (bmi >= 35.0 && bmi <= 39.99)
        {
            return "Class II Obesity";
        }
        else
        {
            return "Class III Obesity";
        }
    }
}
