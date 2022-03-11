using System;

namespace AuldShiteburn
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*PlayerEntity player = new PlayerEntity();
            player.DoDamage();
            Console.WriteLine(player.health2);*/

            Console.WindowWidth = 180;
            Console.WindowHeight = 60;

            Game game = new Game();
            game.GameRunning();
        }
    }
}
