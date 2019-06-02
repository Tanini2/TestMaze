using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaze
{
    class BinaryTree
    {
        //Fonctionnement de l'algorithme
        //1 - Va chercher les cellules voisines du nord et de l'est
        //2 - En choisi une au hasard
        //3 - Fait le lien entre les deux cellules
        //4 - Refait le cycle pour chaque cellule
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
                Shuffle.ShuffleList(neighbors);
                var neighbor = neighbors[0];
                if (neighbor != null)
                {
                    cell.Link(neighbor);
                }
            }
            return grid;
        }
    }
}
