using System;
using System.Linq;

namespace GameFifteen
{
    /// <summary>
    /// Game starting point
    /// </summary>
    public class StartGame
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
