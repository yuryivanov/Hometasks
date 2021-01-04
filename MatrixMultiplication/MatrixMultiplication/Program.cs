using System;

namespace MatrixMultiplication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Label:
                // Matrix creating:
                Console.WriteLine("Please Enter rows of the 1st matrix:");
                var rows1 = int.Parse(Console.ReadLine());
                Console.WriteLine("Please Enter columns of the 1st matrix:");
                var columns1 = int.Parse(Console.ReadLine());

                Console.WriteLine("Please Enter rows of the 2nd matrix:");
                var rows2 = int.Parse(Console.ReadLine());
                Console.WriteLine("Please Enter columns of the 2nd matrix:");
                var columns2 = int.Parse(Console.ReadLine());

                var matrix1 = new Matrix(rows1, columns1) {Id = 1};
                var matrix2 = new Matrix(rows2, columns2) {Id = 1};

                //Matrix validation for multiplication:
                var validator = new MatrixValidator();
                if (validator.Validate(matrix1, matrix2) == false)
                {
                    goto Label;
                }

                var matrix3 = matrix1.MultiplyAsync(matrix2).Result;

                //Console output of the 3rd matrix:
                var matrix3Rows = matrix3.Size.GetUpperBound(0) + 1;
                var matrix3Columns = matrix3.Size.Length / matrix3Rows;

                Console.WriteLine("\n3rd matrix:\n");
                for (int i = 0; i < matrix3.allValues.Count;)
                {
                    for (int j = 0; j < matrix3Columns; j++)
                    {
                        Console.Write(matrix3.allValues[i++] + "\t");
                    }

                    Console.WriteLine();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Input was not in a correct format.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Value was either too large or too small for input");
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong.");
            }
            finally
            {
                Console.WriteLine();
                Main(args);
            }
        }
    }
}
