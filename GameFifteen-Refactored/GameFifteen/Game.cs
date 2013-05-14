using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFifteen
{
    class Game
    {        
        public Field Field { get; set; }
        public Player Player { get; set; }
        public int Complexity { get; set; }

        public Game()
            :this(1000)
        {
        }

        public Game(int complexity)
        {
            this.Complexity = complexity;
            this.Restart();
        }

        public void Restart()
        {
            this.Field = Field.GetRandomField(this.Complexity);
            this.Player = new Player();
        }

        public bool IsSolved()
        {
            int index = 0;
            foreach (int value in this.Field.Cells)
            {
                index = (++index) % (Field.MaxValue + 1);
                if (value != index)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
