// // Define a delegate that takes two int parameters and returns an int
// public delegate int Calculator(int a, int b);
// class Delegate
// {
//     // Methods matching the delegate signature
//     static int Add(int x, int y) => x + y;
//     static int Multiply(int x, int y) => x * y;
//     static int Subtract(int x, int y) => x - y;
//     static void Main(string[] args)
//     {
//         Console.Write("Enter operation (add, multiply, subtract): ");
//         string input = Console.ReadLine();
//         // Switch expression returns the delegate directly
//         Calculator calc = input switch
//         {
//             "add" => Add,
//             "multiply" => Multiply,
//             "subtract" => Subtract,
//             _ => throw new InvalidOperationException("Invalid operation.")
//         };
//         // Call the selected method through the delegate
//         int result = calc(3, 4);
//         Console.WriteLine("Result: " + result);
//     }
// }
 