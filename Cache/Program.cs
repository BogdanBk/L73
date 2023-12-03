using System;
using System.Threading;

class Program
{
    public static void Main()
    {
        FunctionCache<string, int> cache = new FunctionCache<string, int>();

        ExpensiveFunction expensiveFunction = key =>
        {
            Console.WriteLine($"Executing expensive function for key '{key}'");
            Thread.Sleep(2000);
            return key.Length;
        };

        int result1 = cache.GetOrCompute("asd", expensiveFunction, TimeSpan.FromSeconds(2));
        Console.WriteLine($"Result 1: {result1}");

        int result2 = cache.GetOrCompute("asd", expensiveFunction, TimeSpan.FromSeconds(2));
        Console.WriteLine($"Result 2: {result2}");

        Thread.Sleep(3000);

        int result3 = cache.GetOrCompute("asd", expensiveFunction, TimeSpan.FromSeconds(2));
        Console.WriteLine($"Result 3: {result3}");
    }

    // Define the expensive function type
    public delegate int ExpensiveFunction(string key);
}
