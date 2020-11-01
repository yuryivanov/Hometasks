using System;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {

            CalculatorMethods method = new CalculatorMethods();

            for (; ; )
            {
                try
                {
                    Console.WriteLine("\n\nType first number and press Enter button: ");
                    double number1;
                    try
                    {
                        number1 = double.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {
                        throw new Exception("Number in incorrect format entered, please try again");                        
                    }

                    Console.WriteLine("Type one of the available actions:  + - * / ^ ! and press Enter: ");
                    var action = Console.ReadLine();

                    switch (action)
                    {
                        case "+":
                            Console.WriteLine("Result: \n" + method.Add(number1, method.GetSecondNumber()) + "\n");
                            break;
                        case "-":
                            Console.WriteLine("Result: \n" + method.Subtract(number1, method.GetSecondNumber()) + "\n");
                            break;
                        case "*":
                            Console.WriteLine("Result: \n" + method.Multiply(number1, method.GetSecondNumber()) + "\n");
                            break;
                        case "/":
                            Console.WriteLine("Result: \n" + method.Divide(number1, method.GetSecondNumber()) + "\n");
                            break;
                        case "^":
                            Console.WriteLine("Result: \n" + method.Elevate(number1, method.GetSecondNumber()) + "\n");
                            break;
                        case "!":
                            if (number1 == 0)
                            {
                                Console.WriteLine("Result: \n1");
                                break;
                            }
                            else if (number1 <= 65 && method.Factorial(method.Parse(number1)) != 0)
                            {
                                Console.WriteLine("Result: \n" + method.Factorial(method.Parse(number1)) + "\n");
                                break;
                            }                            
                            else
                            {
                                Console.WriteLine("\nNumber is too large to calculate factorial, please try again");
                                break;
                            }                            
                        default:
                            throw new Exception("Incorrect operation entered, please try again");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\n{e.Message}");
                }

                Console.WriteLine("\nIf you want to exit, type \"exit\" and press Enter. If you want to continue press Enter: ");
                var ShouldContinue = Console.ReadLine();
                if (ShouldContinue == "exit")
                {
                    break;
                }
            }
        }
    }
}
