using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using AuldShiteburn.MapData.AreaData;
using AuldShiteburn.MapData.AreaData.Areas;
using AuldShiteburn.MapData.Maps;
using System;

namespace AuldShiteburn
{
    public class Game
    {
        public static bool running = true;

        public void GameRunning()
        {
            AuldShiteburnMap shiteburn = new AuldShiteburnMap();
            shiteburn.RandomiseAreas();

            Map.Instance = shiteburn;

            PlayerEntity.GenerateCharacter();
            PlayerEntity.Instance.PosX = 1;
            PlayerEntity.Instance.PosY = 1;

            Console.CursorVisible = false;
            shiteburn.PrintArea();
            shiteburn.PrintEntities();

            while (running)
            {
                shiteburn.UpdateMap();
                InputSystem.GetInput();
            }
        }
    }
}
