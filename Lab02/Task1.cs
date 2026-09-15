using System;

public static class Task1
{
    public static void Run()
    {
        Console.WriteLine("-- Task 1 --");
        Console.WriteLine("Enter number of patients: ");

        int n = int.Parse(Console.ReadLine()!);

        double[] weight = new double[n];
        for (int i = 0; i < n - 1 ;i++)
        {
            Console.WriteLine($"Enter weight of patient {i+1}: ");
            weight[i] = double.Parse(Console.ReadLine()!);
        }

        double max = weight[0];
        double min = weight[0];

        double sum = 0;
        foreach (double w in weight)
        {
            sum += w;
            if (w > max)
            {
                max = w;
            }

            if (w < min)
            {
                min  = w;
            }

        }
        double avg = sum / weight.Length;
        double moreThanAvg = 0;
        foreach (double w in weight)
        {
            if (w > avg)
                moreThanAvg++;
        }

        Console.WriteLine($"Count fo patints:{n}");
        Console.WriteLine($"Avg wieght:{avg:F1}");
        Console.WriteLine($"Min / Max:{min}/{max}");
        Console.WriteLine($"More then avg {moreThanAvg} of {n}");

    }
}
