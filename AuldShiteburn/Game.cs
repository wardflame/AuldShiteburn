using AuldShiteburn.EntityData;
using AuldShiteburn.MapData;
using AuldShiteburn.MapData.Maps;
using AuldShiteburn.MenuData;
using AuldShiteburn.MenuData.Menus;
using AuldShiteburn.SaveData;
using System;
using System.Threading;

namespace AuldShiteburn
{
    public class Game
    {
        public static bool running = true;
        public static bool mainMenu = true;
        public static bool playing = true;

        public void GameRunning()
        {
            Menu.Instance = new MainMenu();
            while (running)
            {
                while (mainMenu)
                {
                    Menu.Instance.InMenu();
                    mainMenu = false;
                }                
                playing = true;
                while (playing)
                {
                    Map.Instance.UpdateArea();
                    if (playing)
                    {
                        InputSystem.GetInput();
                    }
                }
            }            
        }
    }
}
