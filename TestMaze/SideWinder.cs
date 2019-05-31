using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaze
{
    class SideWinder
    {
        public static Grid Maze(Grid grid, int seed = -1)
        {
            var rand = seed >= 0 ? new Random(seed) : new Random();
            foreach (var row in grid.Row)
            {
                var run = new List<Cell>();

                foreach (var cell in row)
                {
                    run.Add(cell);

                    var atEasternBoundary = cell.East == null;
                    var atNorthernBoundary = cell.North == null;

                    var shouldCloseOut = atEasternBoundary || (!atNorthernBoundary && rand.Next(2) == 0);

                    if (shouldCloseOut)
                    {
                        Shuffle.ShuffleList(run);
                        var member = run[0];
                        if (member.North != null)
                        {
                            member.Link(member.North);
                        }
                        run.Clear();
                    }
                    else
                    {
                        cell.Link(cell.East);
                    }
                }
            }
            return grid;
        }
    }
}
