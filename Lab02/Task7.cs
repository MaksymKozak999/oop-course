using System;

static public class Task7
{
    static public void Run()
    {
        Console.WriteLine("-- Task 7--");
        Console.WriteLine("Enter patient count");
        int n = int.Parse(Console.ReadLine()!);

        string[] names = new string[n];
        double[] bmi = new double[n];

        for (int i = 0; i < names.Length; i++)
        {
            Console.WriteLine($"Enter name for patient {i + 1}");
            names[i] = Console.ReadLine()!;

            Console.WriteLine($"Enter bmi for patient {i + 1}");
            bmi[i] = double.Parse(Console.ReadLine()!);

        }
        string[] sortedNames = (string[])names.Clone();
        double[] sortedBmi = (double[])bmi.Clone();

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (sortedBmi[j] < sortedBmi[j + 1])
                {
                    (sortedNames[j], sortedNames[j + 1]) =
                        (sortedNames[j + 1], sortedNames[j]);

                    (sortedBmi[j], sortedBmi[j + 1]) =
                        (sortedBmi[j + 1], sortedBmi[j]);
                }
            }
        }

        Console.WriteLine("== Top BMI == ");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"#{i + 1} {sortedNames[i]}: {sortedBmi[i]:F2}");
        }
    }
}