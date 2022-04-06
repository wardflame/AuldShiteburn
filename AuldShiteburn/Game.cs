using AuldShiteburn.ArtData;
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
            Utils.WriteColour(ASCIIArt.INTRO_PAGE1);
            Console.ReadKey(true);
            Utils.WriteColour(ASCIIArt.INTRO_PAGE2);
            Console.ReadKey(true);
            Console.Clear();
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
