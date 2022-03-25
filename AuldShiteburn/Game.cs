using AuldShiteburn.MapData;
using AuldShiteburn.MenuData;
using AuldShiteburn.MenuData.Menus;
using AuldShiteburn.SaveData;
using System;

namespace AuldShiteburn
{
    public class Game
    {
        public static bool mainMenu = true;
        public static bool running = true;
        public static bool playing = true;

        public void GameRunning()
        {
            Directories.SaveDirectoryInit();
            Load.LoadGameSettings();
            Console.WriteLine("Use the Arrow Up and Arrow Down keys to navigate menus." +
                "\nUse the Enter and Backspace keys to enter/exit menus." +
                "\nPress any key to continue...");
            Console.ReadKey();
            while (running)
            {
                while (mainMenu)
                {
                    Menu.Instance = new MainMenu();
                    Menu.Instance.RunMenu();
                }
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
