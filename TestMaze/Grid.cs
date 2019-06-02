using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaze
{
    class Grid
    {
        //Rows et Columns permettent de générer la grille de jeu
        public int Rows { get; }
        public int Columns { get; }
        //*X et *Y permettent de gérer les déplacements du joueur
        public int MinX { get; private set; }
        public int MinY { get; private set; }
        public int Size => Rows * Columns;
        public int MaxX => (Columns * 4) - 2;
        public int MaxY => (Rows * 2) - 1;

        private List<List<Cell>> _grid;

        public virtual Cell this[int row, int column]
        {
            get
            {
                if(row < 0 || row >= Rows)
                {
                    return null;
                }
                if (column < 0 || column >= Columns)
                {
                    return null;
                }
                return _grid[row][column];
            }
        }

        public IEnumerable<List<Cell>> Row
        {
            get
            {
                foreach(var row in _grid)
                {
                    yield return row;
                }
            }
        }

        public IEnumerable<Cell> Cells
        {
            get
            {
                foreach(var row in Row)
                {
                    foreach(var cell in row)
                    {
                        yield return cell;
                    }
                }
            }
        }

        public Grid(int rows, int cols)
        {
            Rows = rows;
            Columns = cols;
            MinX = 2;
            MinY = 1;

            PrepareGrid();
            ConfigureCells();
        }
        //Prépare la grille de jeu
        //Crée les cellules, leur assigne une row, une column, un x et un y
        private void PrepareGrid()
        {
            int x = 0;
            int y = 0;
            bool firstX = true;
            bool firstY = true;
            _grid = new List<List<Cell>>();
            for(var r = 0; r < Rows; r++)
            {
                if (firstY)
                {
                    y = MinY;
                    firstY = false;
                }
                else
                {
                    y += 2;
                }
                var row = new List<Cell>();
                for(var c = 0; c < Columns; c++)
                {
                    if (firstX)
                    {
                        x = MinX;
                        firstX = false;
                    }
                    else
                    {
                        x += 4;
                    }
                    row.Add(new Cell(r, c, x, y));
                }
                _grid.Add(row);
                firstX = true;
            }
        }
        //Crée les cellules voisines
        private void ConfigureCells()
        {
            foreach(var cell in Cells)
            {
                var row = cell.Row;
                var col = cell.Column;
                var x = cell.PositionX;
                var y = cell.PositionY;

                cell.North = this[row - 1, col];
                cell.South = this[row + 1, col];
                cell.West = this[row, col - 1];
                cell.East = this[row, col + 1];
            }
        }
        //Affiche la grille de jeu
        public override string ToString()
        {
            var output = new StringBuilder("+");
            for (var i = 0; i < Columns; i++)
            {
                output.Append("---+");
            }
            output.AppendLine();

            foreach(var row in Row)
            {
                var top = "|";
                var bottom = "+";
                foreach(var cell in row)
                {
                    var east = "";
                    const string body = "   ";
                    if(cell.Row == Rows - 1 && cell.Column == Columns - 1)
                    {
                        east = " ";
                    }
                    else
                    {
                        east = cell.IsLinked(cell.East) ? " " : "|";
                    }
                    if(cell.Row == 0 && cell.Column == 0)
                    {
                        top = " ";
                    }
                    top += body + east;

                    var south = cell.IsLinked(cell.South) ? "   " : "---";
                    const string corner = "+";
                    bottom += south + corner;
                }
                output.AppendLine(top);
                output.AppendLine(bottom);
            }

            return output.ToString();
        }
        //Vérifie à l'aide des coordonnées x et y dans quelle cellule le joueur se trouve
        //Vérifie si la cellule est liée avec celle de la direction choisie par le joueur
        public bool IsLinked(int x, int y, string direction)
        {
            bool deplacementOk = false;
            foreach(var row in Row)
            {
                foreach (var cell in row)
                {
                    if(cell.PositionX == x && cell.PositionY == y)
                    {
                        switch (direction)
                        {
                            case "n":
                                if (cell.IsLinked(cell.North))
                                {
                                    deplacementOk = true;
                                }
                                break;
                            case "s":
                                if (cell.IsLinked(cell.South))
                                {
                                    deplacementOk = true;
                                }
                                break;
                            case "w":
                                if (cell.IsLinked(cell.West))
                                {
                                    deplacementOk = true;
                                }
                                break;
                            case "e":
                                if (cell.IsLinked(cell.East))
                                {
                                    deplacementOk = true;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    }

                }
                if (deplacementOk)
                {
                    break;
                }
            }
            return deplacementOk;
        }
    }
}
