using AuldShiteburn.BackendData;
using AuldShiteburn.EntityData.PlayerData;
using AuldShiteburn.ItemData;
using AuldShiteburn.MenuData;
using AuldShiteburn.MenuData.Menus;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.EntityData
{
    [Serializable]
    internal class PlayerEntity : LivingEntity
    {
        public static PlayerEntity Instance { get; set; }
        public ClassData Class { get; private set; }
        public List<Item> Inventory { get; set; } = new List<Item>();
        public override string EntityChar => "PL";
        public bool InMenu { get; set; }
        public long Playtime;

        public override void Move()
        {
            if (!InMenu)
            {
                switch (InputSystem.InputKey)
                {
                    case ConsoleKey.W:
                        {
                            PosY--;
                        }
                        break;

                    case ConsoleKey.S:
                        {
                            PosY++;
                        }
                        break;

                    case ConsoleKey.A:
                        {
                            PosX--;
                        }
                        break;

                    case ConsoleKey.D:
                        {
                            PosX++;
                        }
                        break;
                    case ConsoleKey.Escape:
                        {
                            Menu.Instance = new PauseMenu();
                            Menu.Instance.InMenu();
                        }
                        break;
                }
            }
        }

        public static PlayerEntity GenerateCharacter()
        {
            Instance = new PlayerEntity();
            Instance.PosX = 1;
            Instance.PosY = 1;
            Random rand = new Random();

            #region Class Generation

            Instance.Class = ClassData.Fighter;
            #endregion Class Generation

            string titleStr;
            string forenameStr;

            #region Name Generation
            int sexChance = rand.Next(1, 101);
            if (sexChance <= GameSettings.Instance.SexRatio)
            {
                forenameStr = PlayerGenerationData.nameMale[rand.Next(PlayerGenerationData.nameMale.Count)];
                int titleIndex = rand.Next(0, Instance.Class.TitlesMale.Count);
                titleStr = Instance.Class.TitlesMale[titleIndex];
                Instance.Name = $"{titleStr} {forenameStr}";
                       
            }
            else
            {
                forenameStr = PlayerGenerationData.nameFemale[rand.Next(PlayerGenerationData.nameMale.Count)];
                int titleIndex = rand.Next(0, Instance.Class.TitlesMale.Count);
                titleStr = Instance.Class.TitlesFemale[titleIndex];
                Instance.Name = $"{titleStr} {forenameStr}";
            }
            #endregion Name Generation

            return Instance;
        }
    }
}
