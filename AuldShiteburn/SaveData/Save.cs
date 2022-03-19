using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AuldShiteburn.SaveData
{
    internal class Save
    {
        /// <summary>
        /// If the main save directory Saves doesn't exist,
        /// create it.
        /// </summary>
        public static void Directories()
        {
            if (!Directory.Exists($"Saves"))
            {
                Directory.CreateDirectory($"Saves");
            }
        }
    }
}
