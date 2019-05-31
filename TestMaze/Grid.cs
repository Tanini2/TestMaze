using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaze
{
    class Grid
    {
        public int Rows { get; }
        public int Columns { get; }
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

        //public Cell RandomCell()
        //{
        //    var rand = new Random();
        //    var row = rand.Next(Rows);
        //    var col = rand.Next(Columns);
        //    var randomCell = this[row, col];
        //    if (randomCell == null) {
        //        throw new InvalidOperationException("random cell is null");
        //    }
        //    return randomCell;
        //}

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

        private void ConfigureCells()
        {
            foreach(var cell in Cells)
            {
                var row = cell.Row;
                var col = cell.Column;
                var x = cell.PositionX;
                var y = cell.PositionY;

                cell.North = this[row - 1, col];
                //cell.North.PositionX = x;
                //cell.North.PositionY = y - 2;
                cell.South = this[row + 1, col];
                //cell.South.PositionX = x;
                //cell.South.PositionY = y + 2;
                cell.West = this[row, col - 1];
                //cell.West.PositionX = x - 4;
                //cell.West.PositionY = y;
                cell.East = this[row, col + 1];
                //cell.East.PositionX = x + 4;
                //cell.East.PositionY = y;
            }
        }

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
                    const string body = "   ";
                    var east = cell.IsLinked(cell.East) ? " " : "|";

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
