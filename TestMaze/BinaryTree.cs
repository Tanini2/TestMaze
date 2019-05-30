using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaze
{
    class BinaryTree
    {
        public static Grid Maze(Grid grid, int seed = -1)
        {
            var rand = seed >= 0 ? new Random(seed) : new Random();
            foreach (var cell in grid.Cells)
            {
                var neighbors = new[] { cell.North, cell.East }.Where(c => c != null).ToList();
                if (!neighbors.Any())
                {
                    continue;
                }
                //var neighbor = neighbors.Sample(rand);
                if (neighbor != null)
                {
                    cell.Link(neighbor);
                }
            }
            return grid;
        }
    }
}
