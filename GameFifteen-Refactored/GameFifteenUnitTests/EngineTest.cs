using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameFifteen;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace GameFifteenUnitTests
{
    [TestClass]
    public class EngineTest
    {
        public const string FirstExpectedStart = @"

Welcome to the game “15”. 

Please try to arrange the numbers sequentially. 

Use: 

'top' to view the top scoreboard, 

'restart' to start a new game, 

'exit' to quit the game.


Input the level of complexity (integer value bigger than 1):
-------------
|  1  2  3  4 |
|  5  6  7  8 |
|  9 10 11 12 |
| 13 14    15 |
-------------

Enter a number to move: 
";

        public const string SecondExpectedStart = @"

Welcome to the game “15”. 

Please try to arrange the numbers sequentially. 

Use: 

'top' to view the top scoreboard, 

'restart' to start a new game, 

'exit' to quit the game.


Input the level of complexity (integer value bigger than 1):
-------------
|  1  2  3  4 |
|  5  6  7  8 |
|  9 10 11    |
| 13 14 15 12 |
-------------

Enter a number to move: 
";

        [TestMethod]
        public void TestStartAndExitMessagesOutput()
        {
            StringBuilder output = new StringBuilder();
            Queue<string> commands = new Queue<string>();
            Engine engine = new Engine(
                (x) =>  output.AppendLine(x.ToString()),
                () => { return commands.Dequeue(); }
            );
            commands.Enqueue("1");
            commands.Enqueue("exit");

            engine.Start();
            string resultString = output.ToString().Replace("\r\n", "").Replace("\n\r", "");
            string firstChoice = FirstExpectedStart.Replace("\r\n", "").Replace("\n\r", "");
            string secondChoice = SecondExpectedStart.Replace("\r\n", "").Replace("\n\r", "");

            Assert.AreEqual(firstChoice == resultString || secondChoice == resultString, true);
        }

        [TestMethod]
        public void TestFieldCells()
        {
            StringBuilder output = new StringBuilder();
            Queue<string> commands = new Queue<string>();
            Engine engine = new Engine(
                (x) => output.AppendLine(x.ToString()),
                () => { return commands.Dequeue(); }
            );
            commands.Enqueue("1");
            commands.Enqueue("exit");

            engine.Start();

            int[,] firstExpectedCells = {
                                       {1,2,3,4},
                                       {5,6,7,8},
                                       {9,10,11,12},
                                       {13,14,0,15}
                                   };
            int[,] secondExpectedCells = {
                                       {1,2,3,4},
                                       {5,6,7,8},
                                       {9,10,11,0},
                                       {13,14,15,12}
                                   };

            int[,] expected;

            bool emptyCellIsInPlace = true;
            if (engine.EmptyCellX == 2)
            {
                if (engine.EmptyCellY != 3)
                {
                    emptyCellIsInPlace = false;
                }
                expected = secondExpectedCells;
            }
            else
            {
                if (engine.EmptyCellX != 3 || engine.EmptyCellY != 2)
                {
                    emptyCellIsInPlace = false;
                }
                expected = firstExpectedCells;
            }
            Assert.AreEqual(emptyCellIsInPlace, true, "Empty cell is not in place");
            CollectionAssert.AreEqual(expected, engine.Cells, "All cells are in place");
        }

        [TestMethod]
        public void TestGameWinScenario()
        {
            StringBuilder output = new StringBuilder();
            Queue<string> commands = new Queue<string>();            
            Engine engine = new Engine(
                (x) => output.AppendLine(x.ToString()),
                () => { return commands.Dequeue(); }
            );

            string valueToMove;
            if (engine.EmptyCellX == 2)
            {
                valueToMove = "12";
            }
            else
            {
                valueToMove = "15";
            }

            engine.ExecuteCommand(valueToMove);
            Game game = new Game();
            for (int i = 0; i < Field.Width; i++)
            {
                for (int j = 0; j < Field.Height; j++)
                {
                    game.Field.Cells[i, j] = engine.Cells[i, j];
                }
            }            
            
            Assert.AreEqual(game.IsSolved(), true, "Game solved scenario failed - engine doesn't seem to be moving correctly the fields.");
        }

        [TestMethod]
        public void TestGameInvalidCellValue()
        {
            StringBuilder output = new StringBuilder();
            Queue<string> commands = new Queue<string>();
            Engine engine = new Engine(
                (x) => output.AppendLine(x.ToString()),
                () => { return commands.Dequeue(); }
            );

            engine.TryMove(0);
            string message = output.ToString().Replace("\r\n","").Replace("\n\r","");

            Assert.AreEqual(message, Messages.CellValueOutOfRange, "Player can't move the empty cell!");
        }

        [TestMethod]
        public void TestGameIllegalMove()
        {
            StringBuilder output = new StringBuilder();
            Queue<string> commands = new Queue<string>();
            Engine engine = new Engine(
                (x) => output.AppendLine(x.ToString()),
                () => { return commands.Dequeue(); }
            );

            engine.TryMove(1);
            string message = output.ToString().Replace("\r\n", "").Replace("\n\r", "");
            string expected = Messages.IllegalMove.Replace("\r\n", "").Replace("\n\r", "");
            Assert.AreEqual(message, expected, "Player can't move cell, which doesn't neighbour the empty cell!");
        }

        [TestMethod]
        public void TestGameFieldCellsConsistency()
        {
            StringBuilder output = new StringBuilder();
            Queue<string> commands = new Queue<string>();
            Engine engine = new Engine(
                (x) => output.AppendLine(x.ToString()),
                () => { return commands.Dequeue(); }
            );

            bool[] visited = new bool[Field.MaxCellValue + 1];
            int differentElements = 0;
            foreach(int element in engine.Cells)
            {
                if (!visited[element])
                {
                    differentElements++;
                }
            }

            Assert.AreEqual(differentElements <= Field.MaxCellValue, false, "Engine field must contain each value once!");
        }

    }
}

