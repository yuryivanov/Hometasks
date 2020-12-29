using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gardeners
{
    public class Gardener
    {
        public int Id { get; set; }
        
        public void CultivateFromLeftToRight(Garden garden)
        {
            try
            {
                for (int i = 0; i < garden.GardenCells.Count; i++)
                {
                    if (garden.GardenCells[i].WillBeSkipped == true && garden.GardenCells[i].IsSkipped == false)
                    {
                        garden.GardenCells[i].WillBeSkipped = false;
                        garden.GardenCells[i].IsSkipped = true;
                        Console.WriteLine($"Cell with number \"-1\" has been skipped");
                    }
                    else if (garden.GardenCells[i].IsCultivated == false && garden.GardenCells[i].IsSkipped == false)
                    {
                        garden.GardenCells[i].IsCultivated = true;
                        Console.WriteLine($"Garden cell number {garden.GardenCells[i].CellNumber} has been cultivated by {this.Id} gardener");
                        Thread.Sleep(500);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong");
            }
        }

        public void CultivateUpwards(Garden garden, int[,] gardenCellCoordinates)
        {
            try
            {
                for (int i = garden.GetLengthOfEachRow(gardenCellCoordinates) - 1; i >= 0; i--)
                {
                    for (int j = garden.GetRows(gardenCellCoordinates) - 1; j >= 0; j--)
                    {
                        foreach (var VARIABLE in garden.GardenCells)
                        {
                            if (VARIABLE.WillBeSkipped == true && VARIABLE.IsSkipped == false)
                            {
                                VARIABLE.WillBeSkipped = false;
                                VARIABLE.IsSkipped = true;
                                Console.WriteLine($"Cell with number \"-1\" has been skipped");
                            }
                            else if (VARIABLE.CellNumber == gardenCellCoordinates[j, i] && VARIABLE.IsCultivated == false && VARIABLE.IsSkipped == false)
                            {
                                VARIABLE.IsCultivated = true;
                                Console.WriteLine($"Garden cell number {VARIABLE.CellNumber} has been cultivated by {this.Id} gardener");
                                Thread.Sleep(500);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong");
            }
        }

        public async void CultivateFromLeftToRightAsync(Garden garden)
        {
            Console.WriteLine($"{this.Id} gardener started working");
            await Task.Run(() => CultivateFromLeftToRight(garden));
            Console.WriteLine($"{this.Id} gardener completed the work");
        }

        public async void CultivateUpwardsAsync(Garden garden, int[,] gardenCellCoordinates)
        {
            Console.WriteLine($"{this.Id} gardener started working");
            await Task.Run(()=>CultivateUpwards(garden, gardenCellCoordinates));
            Console.WriteLine($"{this.Id} gardener completed the work");
        }
    }
}
