using System;

namespace AuldShiteburn
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 180;
            Console.WindowHeight = 60;

            Game game = new Game();
            game.GameRunning();
        }
    }
}
