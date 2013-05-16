using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameFifteen;

namespace GameFifteenUnitTests
{
    [TestClass]
    public class MessagesTests
    {
        [TestMethod]
        public void TestCongratulationMessage()
        {
            string expected = "\r\nCongratulations! You won the game in 10 moves.";
            string result = Messages.CongratulationMessage(10);

            Assert.AreEqual(expected, result, "Congratulation message not working as expected!");
        }

        [TestMethod]
        public void TestCellOutOfRange()
        {
            string expected = "Cell value must be in range [1; 15] !";
            string result = Messages.CellValueOutOfRange;

            Assert.AreEqual(expected, result, "Congratulation message not working as expected!");
        }
    }
}
