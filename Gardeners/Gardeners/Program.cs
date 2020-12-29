using System;

namespace Gardeners
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("All cells with number \"-1\" are impassable and shouldn't be cultivated\n");

                int[,] gardenCellCoordinates =
                {
                    {0, 1, 2, 3, 4, 5},
                    {6, -1, 8, 9, 10, 11},
                    {12, 13, 14, -1, 16, 17},
                    {18, 19, 20, 21, 22, 23}
                };

                Garden garden = new Garden(gardenCellCoordinates);

                Gardener gardener1 = new Gardener() {Id = 1};
                Gardener gardener2 = new Gardener() {Id = 2};

                gardener2.CultivateUpwardsAsync(garden, gardenCellCoordinates);

                gardener1.CultivateFromLeftToRightAsync(garden);

                Console.ReadLine();
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong");
            }
        }
    }
}
