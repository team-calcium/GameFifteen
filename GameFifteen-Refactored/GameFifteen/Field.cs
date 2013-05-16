using System;
using System.Linq;
using System.Text;

namespace GameFifteen
{
    /// <summary>
    /// Game field, containing the field cells
    /// </summary>
    public class Field
    {
        public const int Width = 4;
        public const int Height = 4;
        public const int MaxCellValue = Width * Height - 1;

        public int[,] Cells;
        public int EmptyX;
        public int EmptyY;

        /// <summary>
        /// Creating new field (with cells ordered as solved puzzle)
        /// </summary>
        public Field()
        {
            this.Cells = new int[4, 4] { 
                { 1, 2, 3, 4 }, 
                { 5, 6, 7, 8 }, 
                { 9, 10, 11, 12 }, 
                { 13, 14, 15, 0 } 
            };

            this.EmptyX = 3;
            this.EmptyY = 3;
        }

        /// <summary>
        /// Gets randomized field
        /// </summary>
        /// <param name="numberOfMoves">Number of moves made to scramble the field</param>
        /// <returns>The randomized fields</returns>
        public static Field GetRandomField(int numberOfMoves)
        {
            Field result = new Field();
            Random rand = new Random();
            Direction direction = Direction.Right;
            int neighbourX = result.EmptyX;
            int neighbourY = result.EmptyY;

            for (int i = 0; i < numberOfMoves; i++)
            {
                direction = (Direction)rand.Next(4);
                result.CalculateNeighbourCoordinatesFromDirection(direction, out neighbourX, out neighbourY);
                while (!IsCellInRange(neighbourX, neighbourY))
                {
                    direction = (Direction)(((int)direction + 1) % 4);
                    result.CalculateNeighbourCoordinatesFromDirection(direction, out neighbourX, out neighbourY);
                }

                result.MoveEmptyCell(neighbourX, neighbourY);
            }

            return result;
        }

        /// <summary>
        /// Moves the empty cell to new coordinates
        /// </summary>
        /// <param name="newX">new x coordinate</param>
        /// <param name="newY">new y coordinate</param>
        public void MoveEmptyCell(int newX, int newY)
        {
            this.Cells[this.EmptyX, this.EmptyY] = this.Cells[newX, newY];
            this.Cells[newX, newY] = 0;
            this.EmptyX = newX;
            this.EmptyY = newY;
        }

        /// <summary>
        /// Finds the coordinates of a cell by a given cell value
        /// </summary>
        /// <param name="cellValue">The value of the searched cell</param>
        /// <param name="x">x coordinate to the found cell</param>
        /// <param name="y">y coordinate to the found cell</param>
        /// <exception cref="ArgumentOutOfRangeException">Cell value must be in range [0; Field.MaxCellValue]</exception>
        /// <exception cref="ArgumentException">Cell value not found! Cells must contain each value from 0 to Field.MaxCellValue exactly once!</exception>
        public void GetCellCoordinates(int cellValue, out int x, out int y)
        {
            if (cellValue < 0 || cellValue > Field.MaxCellValue)
            {
                throw new ArgumentOutOfRangeException(
                    String.Format("cellValue must be in range [0; {0}]", Field.MaxCellValue));
            }
            x = 0;
            y = 0;
            for (int i = 0; i < Field.Height; i++)
            {
                for (int j = 0; j < Field.Width; j++)
                {
                    if (cellValue == this.Cells[i, j])
                    {
                        x = i;
                        y = j;
                    }
                }
            }
            if (cellValue != this.Cells[x, y])
            {
                throw new ArgumentException(
                    String.Format("Cell value not found! Cells must contain each value from 0 to {0} exactly once!", Field.MaxCellValue));
            }
        }

        private void RecalculateEmptyCellPosition()
        {
            GetCellCoordinates(0, out this.EmptyX, out this.EmptyY);
        }

        private static bool IsCellInRange(int x, int y)
        {
            return x >= 0 && x < Field.Width && y >= 0 && y < Field.Height;
        }

        private void CalculateNeighbourCoordinatesFromDirection(Direction direction, out int x, out int y)
        {
            switch (direction)
            {
                case Direction.Down:
                    x = this.EmptyX;
                    y = this.EmptyY + 1;
                    break;
                case Direction.Up:
                    x = this.EmptyX;
                    y = this.EmptyY - 1;
                    break;
                case Direction.Left:
                    x = this.EmptyX - 1;
                    y = this.EmptyY;
                    break;
                case Direction.Right:
                    x = this.EmptyX + 1;
                    y = this.EmptyY;
                    break;
                default:
                    throw new ArgumentException("direction must be Up, Left, Down or Right!");
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("-------------");

            for (int i = 0; i < Field.Height; i++)
            {
                result.Append("| ");
                for (int j = 0; j < Field.Width; j++)
                {
                    result.AppendFormat("{0,2} ", this.Cells[i, j] != 0 ? this.Cells[i, j].ToString() : " ");
                }
                result.AppendLine("|");
            }

            result.AppendLine("-------------");
            return result.ToString();
        }
    }
}
