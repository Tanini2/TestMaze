using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaze
{
    class SideWinder
    {
        //Fonctionnement de l'algorithme
        //1 - Si on est dans la ligne la plus au nord, connecte la cellule avec sa voisine de l'est
        //2- Sinon on ajoute la cellule à la liste de celles déjà procédées dans cette ligne
        //3 - Choisi entre connecter la cellule avec la voisine de l'est ou connecter une des cellules de la ligne précédente avec sa voisine du nord
        //4 - Si on connecte avec une voisine du nord, on efface la liste de cellules de la ligne précédente
        //5 - Passer à la prochaine cellule jusqu'à la fin de la ligne
        //6 - Passer à la prochaine ligne
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
