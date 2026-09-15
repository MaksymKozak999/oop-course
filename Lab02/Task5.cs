using System;

public static class Task5
{
    public static void Run()
    {
        Console.WriteLine("-- Task 5 --");

        Console.WriteLine($"Enter n");
        int n = int.Parse(Console.ReadLine()!);
        int[,] matrix = new int[n, n];

        for (int i = 0; i < n; i++)
        {
            string[] parts = Console.ReadLine()!.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = int.Parse(parts[j]);
            }
        }
        int[] mainDiagonal = new int[n];
        int[] sideDiagonal = new int[n];

        int mainDiagonalSum = 0;
        int sideDiagonalSum = 0;

        for (int i = 0; i < n; i++)
        {
            mainDiagonal[i] = matrix[i, i];
            sideDiagonal[i] = matrix[i, n - 1 - i];

            mainDiagonalSum += matrix[i, i];         
            sideDiagonalSum += matrix[i, n - 1 - i]; 
        }
        Console.WriteLine($"Main diagonal: {string.Join(", ", mainDiagonal)}(summ = {mainDiagonalSum})");
        Console.WriteLine($"Side diagonal: {string.Join(", ", sideDiagonal)}(summ = {mainDiagonalSum})");

    }
}