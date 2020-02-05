
using System;
using static System.Console;
// Grabbed this code from a Microsoft example console calculator. Made a few edits to play with testing a bit more, but any changes I've made are free to reference.


namespace Calculator
{
    public class CalculatorClass
    {
        public static double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "not-a-number" which we use if an operation, such as division, could result in an error.

            // Use a switch statement to do the math.
            switch (op)
            {
                case "a":
                case "+":
                    result = num1 + num2;
                    break;
                case "s":
                case "-":
                    result = num1 - num2;
                    break;
                case "m":
                case "*":
                    result = num1 * num2;
                    break;
                case "d":
                case "/":
                    // Ask the user to enter a non-zero divisor.
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                    }
                    else
                    {
                        Error.WriteLine("Can't Divide By Zero!");
                    }
                    break;
                // Return text for an incorrect option entry.
                default:
                    Error.WriteLine("Not able to do that. Try +(a), s(-), m(*), or d(/)");
                    break;
            }
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            // Display title as the C# console calculator app.
            WriteLine("Console Calculator in C#\r");
            WriteLine("------------------------\n");

            while (!endApp)
            {
                // Declare variables and set to empty.
                string numInput1 = "";
                string numInput2 = "";
                double result = 0;

                // Ask the user to type the first number.
                Write("Type a number, and then press Enter: ");
                numInput1 = ReadLine();

                double cleanNum1 = 0;
                while (!double.TryParse(numInput1, out cleanNum1))
                {
                    Write("This is not valid input. Please enter an integer value: ");
                    numInput1 = ReadLine();
                }

                // Ask the user to type the second number.
                Write("Type another number, and then press Enter: ");
                numInput2 = ReadLine();

                double cleanNum2 = 0;
                while (!double.TryParse(numInput2, out cleanNum2))
                {
                    Write("This is not valid input. Please enter an integer value: ");
                    numInput2 = ReadLine();
                }

                // Ask the user to choose an operator.
                WriteLine("Choose an operator from the following list:");
                WriteLine("\ta - Add");
                WriteLine("\ts - Subtract");
                WriteLine("\tm - Multiply");
                WriteLine("\td - Divide");
                Write("Your option? ");

                string op = ReadLine();

                try
                {
                    result = CalculatorClass.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        WriteLine("This operation will result in a mathematical error.\n");
                    }
                    else WriteLine("Your result: {0:0.##}\n", result);
                }
                catch (Exception e)
                {
                    WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                }

                WriteLine("------------------------\n");

                // Wait for the user to respond before closing.
                Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (ReadLine() == "n") endApp = true;

                WriteLine("\n"); // Friendly linespacing.
            }
            return;
        }
    }
}

