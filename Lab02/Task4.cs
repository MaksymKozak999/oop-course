using System;

public static class Task4
{
    public static void Run()
    {
        Console.WriteLine("-- Task 4 --");

        Console.WriteLine("Enter n");
        int n =  int.Parse(Console.ReadLine()!);
        Console.WriteLine("Enter m");
        int m = int.Parse(Console.ReadLine()!);
        int[,] matrix = new int[n, m];

        for (int i = 0; i < n; i++)
        {
            string[] parts = Console.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < m; j++)
            {
                matrix[i, j] = int.Parse(parts[j]);
            }
        }

        int maxValue = matrix[0, 0];
        int maxRow = 0;
        int maxCol = 0;

        for (int i = 0; i < n; i++)
        {
            int rowSum = 0;
            for (int j = 0; j < m; j++)
            {
                rowSum += matrix[i, j];

       
                if (matrix[i, j] > maxValue)
                {
                    maxValue = matrix[i, j];
                    maxRow = i;
                    maxCol = j;
                }
            }
            Console.WriteLine($"Doctor {i + 1}: {rowSum} appointments");
        }

        int[] colSums = new int[m];
        for (int j = 0; j < m; j++)
        {
            for (int i = 0; i < n; i++)
            {
                colSums[j] += matrix[i, j];
            }
        }
        Console.WriteLine($"By the day: {string.Join(", ", colSums)}");

        Console.WriteLine($"Max: {maxValue} (Doctor {maxRow + 1}, Day {maxCol + 1})");
    }
}