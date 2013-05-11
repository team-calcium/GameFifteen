using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFifteen
{
    class StartGame
    {
        static void Main()
        {
            Engine engine = new Engine(
            x => Console.WriteLine(x),
            () => Console.ReadLine()
            );
            engine.Start();
        }
    }
}
