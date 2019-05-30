﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaze
{
    class Cell
    {
        public int Row { get; }
        public int Column { get; }

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

        public Cell(int row, int col)
        {
            Row = row;
            Column = col;
            _links = new Dictionary<Cell, bool>();
        }
        
        public void Link(Cell cell, bool bidirectional = true)
        {
            _links[cell] = true;
            if (bidirectional)
            {
                cell.Link(this, false);
            }
        }

        public void Unlink(Cell cell, bool bidirectional = true)
        {
            _links.Remove(cell);
            if (bidirectional)
            {
                cell.Unlink(this, false);
            }
        }

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
