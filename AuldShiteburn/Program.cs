using AuldShiteburn.MenuData.Menus;
using System;

namespace AuldShiteburn
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Auld Shiteburn";
            Console.WindowWidth = 180;
            Console.WindowHeight = 60;
            Console.CursorVisible = false;

            Game game = new Game();
            game.GameRunning();
        }
    }
}
