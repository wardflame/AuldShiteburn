using System;

namespace AuldShiteburn
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Auld Shiteburn";
            Console.WindowWidth = 166;
            Console.WindowHeight = 44;
            Console.CursorVisible = false;

            Game game = new Game();
            game.GameRunning();
        }
    }
}
