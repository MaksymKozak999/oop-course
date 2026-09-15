namespace System;

public static class Task7
{
    public static void Run()
    {
        Console.WriteLine("-- Task 7 --");

        Console.WriteLine("Enter number of sessions ");
        int n = int.Parse(Console.ReadLine()!);


        decimal[] costs = new decimal[n];
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Enter costs of admission");
            costs[i] = decimal.Parse(Console.ReadLine());
        }

        decimal total = 0;
        decimal min = costs[0];
        decimal max = costs[0];

        foreach (decimal cost in costs)
        {
            total += cost;
            if (cost < min) min = cost;
            if (cost > max) max = cost;
        }

        decimal average = total / n;

        int countAboveAverage = 0;
        for (int i = 0; i < n; i++)
        {
            if (costs[i] > average)
            {
                countAboveAverage++;
            }
        }

        int index = 0;
        int foundIndex = -1;
        while (index < n)
        {
            if (costs[index] > 1000)
            {
                foundIndex = index;
                break;
            }
            index++;
        }

        Console.WriteLine("=== Report on Admissions ===");
        Console.WriteLine($"Quantity:       {n}");
        Console.WriteLine($"Total amount:   {total:F2} UAH");
        Console.WriteLine($"Avg:         {average:F2} UAH");
        Console.WriteLine($"Min / Max:      {min:F2} / {max:F2} UAH");
        Console.WriteLine($"More than average: {countAboveAverage} of {n}");

        if (foundIndex != -1)
        {
            Console.WriteLine($"First > 1000:   #{foundIndex + 1} – {costs[foundIndex]:F2} UAH");
        }
        else
        {
            Console.WriteLine("First > 1000:   ---");
        }

        Console.WriteLine("=======================");
    }
}