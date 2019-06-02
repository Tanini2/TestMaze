using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaze
{
    class Cell
    {
        //Les Row et Column permettent de créer la grille
        public int Row { get; }
        public int Column { get; }
        //La position X et Y permet de gérer le déplacement du joueur dans la grille
        public int PositionX { get; set; }
        public int PositionY { get; set; }

        //Cellules voisines
        public Cell North { get; set; }
        public Cell South { get; set; }
        public Cell East { get; set; }
        public Cell West { get; set; }

        //Liste des cellules voisines
        public List<Cell> Neighbors
        {
            get { return new[] { North, South, East, West }.Where(c => c != null).ToList(); }
        }

        //Dictionnaire des cellules connectées
        private readonly Dictionary<Cell, bool> _links;
        public List<Cell> Links => _links.Keys.ToList();

        public Cell(int row, int col, int x, int y)
        {
            Row = row;
            Column = col;
            PositionX = x;
            PositionY = y;
            _links = new Dictionary<Cell, bool>();
        }
        //Fait le lien entre deux cellules
        public void Link(Cell cell, bool bidirectional = true)
        {
            _links[cell] = true;
            if (bidirectional)
            {
                cell.Link(this, false);
            }
        }
        //Enlève le lien entre deux cellules 
        public void Unlink(Cell cell, bool bidirectional = true)
        {
            _links.Remove(cell);
            if (bidirectional)
            {
                cell.Unlink(this, false);
            }
        }
        //Permet de savoir si la cellule est liée avec celle que l'on passe en paramètre
        public bool IsLinked(Cell cell)
        {
            if (cell == null)
            {
                return false;
            }
            return _links.ContainsKey(cell);
        }
    }
}
