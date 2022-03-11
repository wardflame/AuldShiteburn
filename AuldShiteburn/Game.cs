using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using AuldShiteburn.MapData.Areas;
using System;

namespace AuldShiteburn
{
    public class Game
    {
        public static bool running = true;

        public void GameRunning()
        {
            Map shiteburn = new Map();
            shiteburn.Zones.Add(new StartArea());

            PlayerEntity.GenerateCharacter();
            PlayerEntity.Instance.PosX = 1;
            PlayerEntity.Instance.PosY = 1;

            Console.CursorVisible = false;

            while (running)
            {
                shiteburn.UpdateMap();
                InputSystem.GetInput();
            }
        }
    }
}
