using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFifteen
{
    class Field
    {
        public const int Width = 4;
        public const int Height = 4;
        public const int MaxValue = Width * Height - 1;

        public int[,] Cells;
        public int EmptyX;
        public int EmptyY;

        public Field()
        {
            this.Cells = new int[4, 4] { 
                { 1, 2, 3, 4 }, 
                { 5, 6, 7, 8 }, 
                { 9, 10, 11, 12 }, 
                { 13, 14, 0, 15 } 
            };

            this.RecalculateEmptyCellPosition();
        }

        public void RecalculateEmptyCellPosition()
        {
            GetCellCoordinates(0, out this.EmptyX, out this.EmptyY);
        }

        public void GetCellCoordinates(int cellValue, out int x, out int y)
        {
            if(cellValue < 0 || cellValue > Field.MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    String.Format("cellValue must be in range [0; {0}]", Field.MaxValue));
            }
            x = 0;
            y = 0;
            for(int i = 0; i < Field.Height; i++)
            {
                for(int j = 0; j < Field.Width; j++)
                {
                    if(cellValue == this.Cells[i,j])
                    {
                        x = i;
                        y = j;
                    }
                }
            }
            if (cellValue != this.Cells[x, y])
            {
                throw new ArgumentException(
                    String.Format("Cell value not found! Cells must contain each value from 0 to {0} exactly once!", Field.MaxValue));
            }
        }

        public static Field GetRandomField()
        {
            Field result = new Field();

            //TODO - have to rewrite random field generator!
            result.RecalculateEmptyCellPosition();
            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine(" -------------");

            for (int i = 0; i < Field.Height; i++)
            {
                result.Append("| ");
                for (int j = 0; j < Field.Width; j++)
                {
                    result.AppendFormat("{0,2} ", this.Cells[i, j] != 0 ? this.Cells[i, j].ToString() : " ");
                }
                result.AppendLine("|");
            }

            result.AppendLine(" -------------");
            return result.ToString();
        }
    }
}
