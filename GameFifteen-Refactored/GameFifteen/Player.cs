using System;
using System.Linq;

namespace GameFifteen
{
    /// <summary>
    /// Object containing current player stats (Name and Moves). This stats are used in TopScores.
    /// </summary>
    public class Player
    {
        public string Name { get; set; }
        public int Moves { get; private set; }

        /// <summary>
        /// Creates new Player with Name="Unknown" and Moves=0
        /// </summary>
        public Player()
            :this ("Unknown", 0)
        {
        }

        /// <summary>
        /// Creates new Player
        /// </summary>
        /// <param name="name">Players name</param>
        /// <param name="moves">The number of moves the player has made</param>
        public Player(string name, int moves)
        {
            this.Name = name;
            this.Moves = moves;
        }

        /// <summary>
        /// Increases the Moves property with 1
        /// </summary>
        public void Move()
        {
            this.Moves++;
        }
        
        public override string ToString()
        {
            return String.Format("{0}: {1} moves", this.Name, this.Moves);
        }
    }
}
