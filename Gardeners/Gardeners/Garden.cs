using System;
using System.Collections.Generic;

namespace Gardeners
{
    public class Garden
    {
        public List<GardenCell> GardenCells = new List<GardenCell>(); //24
        
        public Garden(int[,] gardenCellCoordinates)
        {
            try
            {
                for (int i = 0; i < GetRows(gardenCellCoordinates); i++)
                {
                    for (int j = 0; j < GetLengthOfEachRow(gardenCellCoordinates); j++)
                    {
                        if (gardenCellCoordinates[i, j] == -1)
                        {
                            GardenCells.Add(new GardenCell { IsCultivated = false, CellNumber = gardenCellCoordinates[i, j], WillBeSkipped = true, IsSkipped = false });
                        }
                        else
                        {
                            GardenCells.Add(new GardenCell { IsCultivated = false, CellNumber = gardenCellCoordinates[i, j], WillBeSkipped = false, IsSkipped = false });
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong");
            }
        }

        public int GetRows(int[,] gardenCellCoordinates)
        {
            try
            {
                int rows = gardenCellCoordinates.GetUpperBound(0) + 1; //4

                return rows;
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong");
            }
        }

        public int GetLengthOfEachRow(int[,] gardenCellCoordinates)
        {
            try
            {
                int lengthOfEachRow = gardenCellCoordinates.Length / GetRows(gardenCellCoordinates); //6

                return lengthOfEachRow;
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong");
            }
        }
    }
}
