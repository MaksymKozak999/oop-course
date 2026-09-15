using System;

public static class Task3
{
    public static void Run()
    {
        Console.WriteLine("-- Task 3 --");

        string[] daysOfWeek = new string[]
        {
            "Monday",
            "Tuesday",
            "Wednesday",
            "Thursday",
            "Friday",
            "Saturday",
            "Sunday"
        };

        int[] patients = new int[7];
        int sum = 0;
        int maxIdx = 0;
        int minIdx = 0;
        for (int i = 0; i < patients.Length; i++)
        {
            Console.WriteLine($"Enter  number of patients for {daysOfWeek[i]}: ");
            patients[i] = int.Parse(Console.ReadLine()!);
            sum += patients[i];
        }

        for (int i = 0; i < patients.Length; i++)
        {
            Console.WriteLine($"{daysOfWeek[i], -12} | {patients[i]} patients");
        }

        for (int i = 0;i < patients.Length;i++)
        {
            if (patients[i] > patients[maxIdx])
            {
                maxIdx = i;
            }
            if (patients[i] < patients[minIdx])
            {
                minIdx = i;
            }
        }
        
        Console.WriteLine($"Total patients for the week: {sum}");
        Console.WriteLine($"The most: {daysOfWeek[maxIdx]} ({patients[maxIdx]})");
        Console.WriteLine($"Least: {daysOfWeek[minIdx]} ({patients[minIdx]})");


    }
}