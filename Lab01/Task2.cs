using System;

public static class Task2
{
    public static void Run()
    {
        Console.WriteLine("-- Task 2 --");

        Console.WriteLine("Enter price: ");
        double price = double.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter quantity: ");
        double quantity = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter discount in percent: ");
        double discount = int.Parse(Console.ReadLine()!);

        Console.WriteLine($"The summ is:{CalculateCost(price, quantity, discount):F2} UAH");
    }
    static double CalculateCost(double price, double quantity, double discount)
    {
        double sum = price * quantity * (1 - discount / 100);

        return sum;
    }
}
