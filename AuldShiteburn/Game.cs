using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using AuldShiteburn.MapData.Maps;
using AuldShiteburn.MenuData.Menus;
using System;
using System.Threading;

namespace AuldShiteburn
{
    public class Game
    {
        public static bool running = true;
        public static bool playing = true;
        public static bool newGame = true;

        public void GameRunning()
        {
            while (running)
            {
                new PauseMenu();
                AuldShiteburnMap shiteburn = new AuldShiteburnMap();
                if (newGame)
                {
                    Console.Clear();
                    shiteburn.RandomiseAreas();
                    Map.Instance = shiteburn;
                    PlayerEntity.GenerateCharacter();
                    shiteburn.PrintMap();
                    newGame = false;
                }

                playing = true;
                while (playing)
                {
                    shiteburn.UpdateArea();
                    if (playing)
                    {
                        InputSystem.GetInput();
                    }
                }
            }            
        }
    }
}
