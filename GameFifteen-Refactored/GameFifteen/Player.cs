using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFifteen
{
    class Player
    {
        public string Name { get; set; }
        public int Moves { get; private set; }

        public Player()
            :this ("Unknown", 0)
        {
        }

        public Player(string name, int moves)
        {
            this.Name = name;
            this.Moves = moves;
        }

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
