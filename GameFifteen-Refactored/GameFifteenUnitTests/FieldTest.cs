using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameFifteen;

namespace GameFifteenUnitTests
{
    [TestClass]
    public class FieldTest
    {
        [TestMethod]
        public void TestCreatingNewFieldIsSolved()
        {
            int[,] solved = new int[4, 4] { 
                { 1, 2, 3, 4 }, 
                { 5, 6, 7, 8 }, 
                { 9, 10, 11, 12 }, 
                { 13, 14, 15, 0 } 
            };

            Field field = new Field();
            bool isSolved = true;
            for (int i = 0; i < Field.Width; i++)
            {
                for (int j = 0; j < Field.Height; j++)
                {
                    if (solved[i, j] != field.Cells[i, j])
                    {
                        isSolved = false;
                        break;
                    }
                }
                if (!isSolved)
                {
                    break;
                }
            }

            Assert.AreEqual(isSolved, true, "Empty field constuctor doesn't generate solved field!");
        }

        [TestMethod]
        public void TestMoveEmptyCell()
        {
            Field field = new Field();
            field.MoveEmptyCell(3, 2);

            Assert.AreEqual(field.Cells[3,2]==0 && field.Cells[3,3]==15, true, "Moving empty cell doesn't change values correctly!");
        }

        [TestMethod]
        public void TestGetCellCoordinates()
        {
            Field field = new Field();
            int x;
            int y;
            field.GetCellCoordinates(15, out x, out y);
            
            Assert.AreEqual(x == 3 && y == 2, true, "Finding field cell coordinates by given value doesn't work correctly!");
        }

        [TestMethod]
        public void TestToString()
        {
            Field field = new Field();
            string expected = @"-------------
|  1  2  3  4 |
|  5  6  7  8 |
|  9 10 11 12 |
| 13 14 15    |
-------------
";

            Assert.AreEqual(expected, field.ToString(), "field.ToString() not working correctly!");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void TestInvalidCellRangeException()
        {
            Field field = new Field();
            int x,y;
            field.GetCellCoordinates(Field.MaxCellValue + 1, out x, out y);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test()
        {
            Field field = new Field();
            int x, y;
            field.Cells[Field.Width - 1, Field.Height - 1] = 15;
            field.GetCellCoordinates(0, out x, out y);
        }

    }
}
