using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameFifteen;
using System.Text;
using System.Collections.Generic;

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

            Assert.AreEqual(FirstExpectedStart == output.ToString() || SecondExpectedStart == output.ToString(), true);
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

            int[,] FirstExpectedCells = {
                                       {1,2,3,4},
                                       {5,6,7,8},
                                       {9,10,11,12},
                                       {13,14,0,15}
                                   };
            int[,] SecondExpectedCells = {
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
                expected = SecondExpectedCells;
            }
            else
            {
                if (engine.EmptyCellX != 3 || engine.EmptyCellY != 2)
                {
                    emptyCellIsInPlace = false;
                }
                expected = FirstExpectedCells;
            }
            Assert.AreEqual(emptyCellIsInPlace, true, "Empty cell is not in place");
            CollectionAssert.AreEqual(expected, engine.Cells, "All cells are in place");
        }

        [TestMethod]
        public void TestGameWinScreen()
        {
            int[,] newCells = {
                                    {1,2,3,4},
                                    {5,6,7,8},
                                    {9,10,11,12},
                                    {13,14,0,15}
                                };
            StringBuilder output = new StringBuilder();
            Queue<string> commands = new Queue<string>();
            int count = 0;
            Engine engine = new Engine(
                (x) => {
                    if (++count == 3)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                engine.Cells[i, j] = newCells[i, j];
                            }
                        }
                    }
                    if (count > 5)
                    {
                        output.AppendLine(x.ToString());
                    }
                },
                () => { return commands.Dequeue(); }
            );
            commands.Enqueue("1");
            commands.Enqueue("15");

            commands.Enqueue("1");
            commands.Enqueue("exit");
            engine.Start();
            
            Assert.AreEqual(false, true, "Empty cell is not in place");
        }

    }
}

