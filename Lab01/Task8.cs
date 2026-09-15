using System;

public static class Task3
{
    public static void Run()
    {
        Console.WriteLine("-- Task 8 --");
       
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
    static double CalculateCost(double price, double quantity, double discount)
    {
        double sum = price * quantity * (1 - discount / 100);

        return sum;
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