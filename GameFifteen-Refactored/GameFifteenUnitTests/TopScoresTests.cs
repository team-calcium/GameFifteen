using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameFifteen;

namespace GameFifteenUnitTests
{
    [TestClass]
    public class TopScoresTests
    {
        [TestMethod]
        public void TestAddPlayer()
        {
            Player pesho = new Player("Pesho", 42);
            TopScores.AddPlayerToScoreBoard(pesho);
            string scoreBoard = TopScores.GetScoreBoard();
            Assert.AreEqual(scoreBoard.Contains(pesho.ToString()), true);
        }
    }
}
