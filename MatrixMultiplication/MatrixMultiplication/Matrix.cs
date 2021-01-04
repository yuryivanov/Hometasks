using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MatrixMultiplication
{
    public class Matrix
    {
        public int Id { get; set; }

        public int[,] Size { get; set; }

        public List<int> allValues = new List<int>();

        public Matrix(int rows, int columns)
        {
            try
            {
                Random rnd = new Random();

                int numberOfCells = rows * columns;

                Size = new int[rows, columns];

                for (int i = 0; i < numberOfCells; i++)
                {
                    allValues.Add(rnd.Next(1, 10));
                    //allValues.Add(2);
                    //It's for testing. Using "2" for all cells in the 1st matrix 11x11 and in the 2nd matrix 11x11 - we will get 3rd matrix 11x11 with "44" in each cell
                }
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong.");
            }
        }

        public async Task<Matrix> MultiplyAsync(Matrix matrix2)
        {
            try
            {
                int matrix1Rows = this.Size.GetUpperBound(0) + 1;
                int matrix1Columns = this.Size.Length / matrix1Rows;

                int matrix2Rows = matrix2.Size.GetUpperBound(0) + 1;
                int matrix2Columns = matrix2.Size.Length / matrix2Rows;

                Matrix matrix3 = new Matrix(matrix1Rows, matrix2Columns);

                Task[] tasks = new Task[matrix1Rows * matrix2Columns];

                int variable = 0;

                for (int i = 0; i < matrix1Rows; i++)
                {
                    for (int j = 0; j < matrix2Columns; j++)
                    {
                        var res = new Task((parameter) =>
                        {
                            int[] ij = (int[])parameter;

                            int ii = ij[0];

                            int jj = ij[1];

                            int sum = 0;

                            for (int k = 0; k < matrix1Columns; k++)
                            {
                                sum += this.allValues[ii * matrix1Columns + k] * matrix2.allValues[k * matrix2Columns + jj];
                            }

                            matrix3.allValues[ii * matrix2Columns + jj] = sum;
                        }, new int[2] { i, j });

                        tasks[variable++] = res;

                        res.Start();
                    }
                }

                await Task.Run(() => Console.WriteLine("All cells for the 3rd matrix have been counted."));

                return matrix3;
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong.");
            }
        }
    }
}
