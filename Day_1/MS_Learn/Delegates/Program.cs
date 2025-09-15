using System;
public class Calculator
{
    public int Add(int x, int y)
    {
        return x + y;
    }

    public int Subtract(int x, int y)
    {
        return x - y;
    }

    public int Multiply(int x, int y)
    {
        return x * y;
    }

    public int Divide(int x, int y)
    {
        if (y == 0)
            throw new DivideByZeroException();
        return x / y;
    }
}

public delegate int PerformCalculation(int x, int y);

public class CalculatorApp
{
    public static void Main()
    {
        Calculator calculator = new Calculator();

        // Create delegate instances using the defined delegate type
        PerformCalculation add = calculator.Add;
        PerformCalculation subtract = calculator.Subtract;
        PerformCalculation multiply = calculator.Multiply;
        PerformCalculation divide = calculator.Divide;

        // Call the methods using the delegates
        Console.WriteLine("Addition: " + add(5, 3)); // Output: 8
        Console.WriteLine("Subtraction: " + subtract(5, 3)); // Output: 2
        Console.WriteLine("Multiplication: " + multiply(5, 3)); // Output: 15
        Console.WriteLine("Division: " + divide(5, 3)); // Output: 1
    }
}
