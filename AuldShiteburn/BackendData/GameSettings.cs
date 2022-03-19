using System;

namespace AuldShiteburn.BackendData
{
    [Serializable]
    internal class GameSettings
    {
        public static GameSettings Instance = new GameSettings();
        public static int sexRatio = 50;
    }
}
