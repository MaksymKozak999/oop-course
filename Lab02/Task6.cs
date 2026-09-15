using System;

static public class Task6
{
    static public void Run()
    {
        Console.WriteLine("-- Task 6 --");

        Console.WriteLine("Enter count of doctors");
        int n = int.Parse(Console.ReadLine()!);
       

        int[][] costs = new int[n][];
        
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("Enter quantity of doctor's appointment");
            int k = int.Parse(Console.ReadLine()!);

            costs[i] = new int[k];
            for (int j = 0; j < k; j++)
            {
                costs[i][j] = int.Parse(Console.ReadLine()!);
            }
        }

        int maxDoctorIndex = 0;
        int maxIncome = -1;

        for (int i = 0; i < n; i++)
        {
            int appointmentsCount = costs[i].Length;
            int totalSum = 0;

            for (int j = 0; j < appointmentsCount; j++)
            {
                totalSum += costs[i][j];
            }

            double average = appointmentsCount > 0 ? (double)totalSum / appointmentsCount : 0.0;

            Console.WriteLine($"Doctor {i + 1}: {appointmentsCount} appointments, summ={totalSum} UAH, average={average:F2} UAH");

            if (totalSum > maxIncome)
            {
                maxIncome = totalSum;
                maxDoctorIndex = i + 1;
            }
        }

        Console.WriteLine($"Highest income: doctor {maxDoctorIndex} ({maxIncome} UAH)");
    }
}