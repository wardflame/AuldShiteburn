using System;

namespace AuldShiteburn
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Auld Shiteburn";
            Console.WindowWidth = 172;
            Console.WindowHeight = 44;
            Console.CursorVisible = false;

            bool running = true;
            while (running)
            {
                Game game = new Game();
                game.GameRunning();
            }
        }
    }
}
