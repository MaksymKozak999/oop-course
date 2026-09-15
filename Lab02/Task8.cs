using System;

static public class Task8
{
    static public void Run()
    {
        Console.WriteLine("-- Task 8 --");
        Console.WriteLine("Enter department count");
        int depart = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter week count");
        int week = int.Parse(Console.ReadLine()!);

        int shift = 2;
        int[,,] data = new int[depart, week, shift];

        int[] totals = new int[depart];

        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
            {
                for (int k = 0; k < data.GetLength(2); k++)
                {
                    Console.Write($"Enter element [{i}, {j}, {k}]: ");
                    data[i, j, k] = int.Parse(Console.ReadLine()!);

                    totals[i] += data[i, j, k];
                }
            }
        }
        
        for (int i = 0; i < data.GetLength(0); i++)
        {
            int currentSum = 0;

            for (int j = 0; j < data.GetLength(1); j++)
            {
                for (int k = 0; k < data.GetLength(2); k++)
                {
                    currentSum += data[i, j, k];
                }
            }

            totals[i] = currentSum;
        }

        for (int i = 0; i < data.GetLength(0); i++)
        {
            Console.WriteLine($"Department {i + 1}:");

            for (int j = 0; j < data.GetLength(1); j++)
            {
                Console.WriteLine($"-- Week {j + 1}:");

                for (int k = 0; k < data.GetLength(2); k++)
                {
                    if (k == 0)
                    {
                        Console.Write($"Morning: {data[i, j, k]}\t");
                    }
                    else
                    {
                        Console.Write($"Evening: {data[i, j, k]}\t");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine($"In total: {totals[i]}");
            Console.WriteLine();
        }


        int maxTotal = totals[0];
        int maxInd = 0;

        for (int i = 1; i < totals.Length; i++)
        {
            if (totals[i] > maxTotal)
            {
                maxTotal = totals[i];
                maxInd = i;
            }
        }

        Console.WriteLine($"The busiest department: {maxInd + 1} ({maxTotal})");
    }
}