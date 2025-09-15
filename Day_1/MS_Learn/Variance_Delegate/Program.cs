public class Person { }
public class Employee : Person { }

public static Employee FindEmployee(string title) => new Employee();

public class Program
{
    public static void Main()
    {
        // Func<string, Person> can hold a method that returns Employee
        Func<string, Person> func = FindEmployee;
        Person person = func("Manager");
    }
}