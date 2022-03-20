using AuldShiteburn.MapData;
using AuldShiteburn.MenuData;
using AuldShiteburn.MenuData.Menus;
using AuldShiteburn.SaveData;

namespace AuldShiteburn
{
    public class Game
    {
        public static bool running = true;
        public static bool mainMenu = true;
        public static bool playing = true;

        public void GameRunning()
        {
            Directories.SaveDirectoryInit();
            Load.LoadGameSettings();
            while (running)
            {
                while (mainMenu)
                {
                    Menu.Instance = new MainMenu();
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
