using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFifteen
{
    /// <summary>
    /// Class describing current Game containing current Field, current Player and the current starting Complexity of the game (the number of moves needed to solve the puzzle).
    /// </summary>
    public class Game
    {   
        public Field Field { get; set; }
        public Player Player { get; set; }
        public int Complexity { get; set; }

        /// <summary>
        /// Creates new Game with default complexity = 1000
        /// </summary>
        public Game()
            :this(1000)
        {
        }

        /// <summary>
        /// Creating a new Game instance
        /// </summary>
        /// <param name="complexity">The number of moves made to scramble the field (bigger complexity => more complex to solve puzzle)</param>
        public Game(int complexity)
        {
            this.Complexity = complexity;
            this.Restart();
        }

        /// <summary>
        /// Getting new random Field and new Player
        /// </summary>
        public void Restart()
        {
            this.Field = Field.GetRandomField(this.Complexity);
            this.Player = new Player();
        }

        /// <summary>
        /// Checks if the puzzle is solved
        /// </summary>
        /// <returns>True if solved, False if not</returns>
        public bool IsSolved()
        {
            int index = 0;
            foreach (int value in this.Field.Cells)
            {
                index = (++index) % (Field.MaxCellValue + 1);
                if (value != index)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
