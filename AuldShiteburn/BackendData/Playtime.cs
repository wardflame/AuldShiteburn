using AuldShiteburn.EntityData;
using System;
using System.Diagnostics;

namespace AuldShiteburn.BackendData
{
    public static class Playtime
    {
        public static Stopwatch sessionTimer = new Stopwatch();

        public static void StartPlaytime()
        {
            sessionTimer.Start();
        }

        public static void StopPlaytime()
        {
            sessionTimer.Stop();
        }

        public static void ResetPlaytime()
        {
            sessionTimer.Reset();
        }

        public static long GetTotalPlayTime()
        {
            return (PlayerEntity.Instance.Playtime + sessionTimer.ElapsedTicks);
        }

        public static DateTime GetSessionLengthAsDateTime()
        {
            DateTime gameTime = new DateTime(GetTotalPlayTime());
            return gameTime;
        }
    }
}
