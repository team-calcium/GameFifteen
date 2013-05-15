using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameFifteen;

namespace GameFifteenUnitTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void TestPlayerToString()
        {
            Player player = new Player();
            Assert.AreEqual(player.ToString(), "Unknown: 0 moves", "ToString of Empty Player not working correctly");
        }

        [TestMethod]
        public void TestPlayerMoveMethod()
        {
            Player player = new Player("Pesho",4);
            player.Move();
            Assert.AreEqual(player.Moves, 5, "Player move not working correctly");
        }
    }
}
