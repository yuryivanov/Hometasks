using System;
using System.Threading;

namespace Project
{
    class CalculatorMethods
    {        
        internal double GetSecondNumber()
        {
            Console.WriteLine("Type second number and press Enter button: ");
            double number2;
            try
            {
                number2 = double.Parse(Console.ReadLine());
                return number2;
            }
            catch (Exception)
            {
                throw new Exception("Number in incorrect format entered, please try again");
            }
        }
        internal double Add(double x, double y)
        {
            if ((-9223372036854775808 < (x + y)) && ((x + y) < 9223372036854775807))
            { return x + y; }
            else
            { throw new Exception("Result is outside the minimum / maximum values"); }            
        }
        internal double Subtract(double x, double y)
        {
            if (-9223372036854775808 < (x - y) && (x - y) < 9223372036854775807)
            { return x - y; }
            else
            { throw new Exception("Result is outside the minimum / maximum values"); }
        }
        internal double Multiply(double x, double y)
        {
            if (-9223372036854775808 < (x * y) && (x * y) < 9223372036854775807)
            { return x * y; }
            else
            { throw new Exception("Result is outside the minimum / maximum values"); }
        }
        internal double Divide(double x, double y)
        {
            if (y == 0)
            {
                throw new Exception("Cannot be divided by zero, please try again");
            }
            else if (-9223372036854775808 < (x / y) && (x / y) < 9223372036854775807)
            { return x / y; }
            else
            { throw new Exception("Result is outside the minimum / maximum values"); }
        }
        internal double Elevate(double x, double y)
        {
            if (x < 0 && y != 0 && y > -1 && y < 1)
            { throw new Exception("Invalid input, please try again"); }
            else if (-9223372036854775808 < Math.Pow(x, y) && Math.Pow(x, y) < 9223372036854775807)
            { return Math.Pow(x, y); }
            else
            { throw new Exception("Result is outside the minimum / maximum values"); }
        }
        internal long Parse(double x)
        {
            if (x > 0 && x % 1 == 0)
            {
                long res = checked((long)x);
                return res;
            }
            else
            {
                throw new Exception("Cannot calculate the factorial of a negative/fractional/too large number, please try again");
            }
        }        
        internal long Factorial(long res)
        {            
                if (res == 1)
                {
                    return 1;
                }
                return res * Factorial(res - 1);         
        }
    }
}
