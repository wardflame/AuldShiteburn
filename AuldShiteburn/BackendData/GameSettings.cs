using System;

namespace AuldShiteburn.BackendData
{
    [Serializable]
    internal class GameSettings
    {
        public static GameSettings Instance = new GameSettings();
        private int sexRatio = 50;
        public int SexRatio
        {
            get { return sexRatio; }
            set
            {
                sexRatio = value;
                if (value > 100)
                {
                    sexRatio = 100;
                }
                else if (value < 0)
                {
                    sexRatio = 0;
                }
            }
        }
    }
}
