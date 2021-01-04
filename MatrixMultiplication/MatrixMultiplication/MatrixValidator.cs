using System;

namespace MatrixMultiplication
{
    public class MatrixValidator
    {
        public bool Validate(Matrix matrix1, Matrix matrix2)
        {
            int matrix1Rows = matrix1.Size.GetUpperBound(0) + 1;
            int matrix1Columns = matrix1.Size.Length / matrix1Rows;

            int matrix2Rows = matrix2.Size.GetUpperBound(0) + 1;
            int matrix2Columns = matrix2.Size.Length / matrix2Rows;

            if (matrix1Columns == matrix2Rows && matrix1Rows < 1001 && matrix1Columns < 1001 && matrix2Rows < 1001 && matrix2Columns < 1001)
            {
                Console.WriteLine("\nMatrices have been validated successfully for multiplication...");
                return true;
            }

            Console.WriteLine(@"
Error! Such matrices cannot be multiplied, since the number of  
the 1st matrix columns is not equal to the number of the 2nd matrix rows
or size of any matrix is more than 1000x1000.
Please try again.
");
            return false;
        }
    }
}
