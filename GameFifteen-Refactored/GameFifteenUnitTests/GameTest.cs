using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameFifteen;
using System.Collections.Generic;

namespace GameFifteenUnitTests
{
    [TestClass]
    public class GameTest
    {
        [TestMethod]
        public void TestIsSolvedMethod()
        {
            Game game = new Game();
            game.Field = new Field();

            Assert.AreEqual(game.IsSolved(), true, "IsSolved method not working correctly!");
        }

        [TestMethod]
        public void TestGameFieldConsistency()
        {
            Game game = new Game();
            HashSet<int> values = new HashSet<int>();
            foreach (int cellValue in game.Field.Cells)
            {
                if (!values.Contains(cellValue))
                {
                    values.Add(cellValue);
                }
            }

            Assert.AreEqual(values.Count, Field.MaxCellValue + 1, "Game field doesn't Contain all values!");
        }
    }
}
