using System;

public static class Task2
{
    public static void Run()
    {
        Console.WriteLine("-- Task 2 --");
        Console.WriteLine("Enter number of visits:");

        int n = int.Parse(Console.ReadLine()!);

        int[] costs = new int[n];

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Enter cost of visit {i + 1}:");
            costs[i] = int.Parse(Console.ReadLine()!);
        }

        string originalCosts = string.Join(" ", costs);

        int[] costsSorted = (int[])costs.Clone();

        // Bubble sort
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (costsSorted[j] > costsSorted[j + 1])
                {
                    int temp = costsSorted[j];
                    costsSorted[j] = costsSorted[j + 1];
                    costsSorted[j + 1] = temp;
                }
            }
        }

        string sortedCosts = string.Join(" ", costsSorted);

        Console.WriteLine($"Queue before: {originalCosts}");
        Console.WriteLine($"Queue after: {sortedCosts}");

        Console.WriteLine($"Cheapest: {costsSorted[0]} UAH");
        Console.WriteLine($"Most expensive: {costsSorted[n - 1]} UAH");
    }
}