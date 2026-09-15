using System;

public static class Task2
{
    public static void Run()
    {
        Console.WriteLine("-- Task 2 --");
        Console.WriteLine("Enter number of visits:");

        int n = int.Parse(Console.ReadLine()!);

        double[] costs = new double[n];

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Enter cost of visit {i + 1}:");
            costs[i] = double.Parse(Console.ReadLine()!);
        }

        string originalCosts = string.Join(" | ", costs);

        double[] costsSorted = (double[])costs.Clone();

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (costsSorted[j] > costsSorted[j + 1])
                {
                    double temp = costsSorted[j];
                    costsSorted[j] = costsSorted[j + 1];
                    costsSorted[j + 1] = temp;
                }
            }
        }

        string sortedCosts = string.Join(" | ", costsSorted);

        Console.WriteLine($"Unsorted costs: {originalCosts}");
        Console.WriteLine($"Sorted costs: {sortedCosts}");

        Console.WriteLine($"The cheapest: {costsSorted[0]} UAH");
        Console.WriteLine($"The most expensive: {costsSorted[n - 1]} UAH");
    }
}