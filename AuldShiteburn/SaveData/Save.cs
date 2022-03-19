using AuldShiteburn.BackendData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AuldShiteburn.SaveData
{
    internal class Save
    {
        /// <summary>
        /// Save game settings, things such as text speed etc.
        /// </summary>
        public static void SaveGameSettings()
        {
            if (!Directory.Exists($"GameSettings"))
            {
                Directory.CreateDirectory($"GameSettings");
            }

            string save = JsonConvert.SerializeObject(GameSettings.Instance);

            File.WriteAllText($"GameSettings\\GameSettings.json", save);
        }
    }
}
