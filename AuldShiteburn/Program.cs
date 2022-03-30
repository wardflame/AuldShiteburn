using System;

namespace AuldShiteburn
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Auld Shiteburn";
            Console.WindowWidth = 160;
            Console.WindowHeight = 42;
            Console.CursorVisible = false;

            Game game = new Game();
            game.GameRunning();
        }
    }
}
