using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.TileData.Tiles
{
    [Serializable]
    abstract class NPCTile : Tile
    {
        public abstract string NPCName { get; }

        protected NPCTile(string displayChar, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black) : base(displayChar, true, foreground, background)
        {
            InitLines();
        }

        protected abstract void InitLines();
        protected abstract void Interaction();

        public override void OnCollision(Entity entity)
        {
            if (entity is PlayerEntity player)
            {
                player.inMenu = true;
                Interaction();
                player.inMenu = false;
            }
        }

        protected string Narration(string narration)
        {
            return narration;
        }

        protected string Dialogue(string dialogue)
        {
            return Narration($"{NPCName}: {dialogue}");
        }

        protected bool Decision(string promptYes = "Accept", string promptNo = "Refuse")
        {
            Console.Write(Narration($"{promptYes} (Y) {promptNo} (N)"));
            do
            {
                InputSystem.GetInput();
                switch (InputSystem.InputKey)
                {
                    case ConsoleKey.Y:
                        {
                            return true;
                        }
                    case ConsoleKey.N:
                        {
                            return false;
                        }
                }
            } while (InputSystem.InputKey != ConsoleKey.Y && InputSystem.InputKey != ConsoleKey.N);
            return false;
        }

        protected bool CycleInteraction(List<InteractionData> interactions, string decisionRefusal = null)
        {
            int index = 0;
            PrintBrowseUI(true);
            PrintInteraction(interactions[index]);
            bool quit = false;
            do
            {
                InputSystem.GetInput();
                switch (InputSystem.InputKey)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            if (index <= interactions.Count - 1 && index > 0)
                            {
                                index--;
                                if (interactions[index].decision)
                                {
                                    index++;
                                }
                                else
                                {
                                    Map.Instance.ClearInteractInterface();
                                    SetCursorInteract();
                                    PrintInteraction(interactions[index]);
                                }                                
                            }
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        {
                            if (index >= 0 && index < interactions.Count - 1)
                            {
                                index++;
                                if (interactions[index].decision)
                                {
                                    SetCursorInteract();
                                    Map.Instance.ClearInteractInterface();
                                    PrintInteraction(interactions[index]);
                                    SetCursorInteract(0, 1);
                                    if (Decision())
                                    {
                                        index++;
                                        Map.Instance.ClearInteractInterface();
                                        SetCursorInteract();
                                        PrintInteraction(interactions[index]);
                                    }
                                    else
                                    {
                                        Map.Instance.ClearInteractInterface(offsetY: 2);
                                        SetCursorInteract();
                                        Console.Write(Dialogue(decisionRefusal));
                                        SetCursorInteract(offsetY: 3);
                                        Console.Write(new string(' ', Console.WindowWidth - Utils.UIInteractOffset));
                                        quit = true;
                                    }
                                }
                                else
                                {
                                    if (index >= interactions.Count - 1)
                                    {
                                        SetCursorInteract();
                                        PrintInteraction(interactions[index]);
                                        SetCursorInteract(offsetY: 3);
                                        Console.Write(new string(' ', Console.WindowWidth - Utils.UIInteractOffset));
                                        return true;
                                    }
                                    else
                                    {
                                        Map.Instance.ClearInteractInterface(offsetY: 3);
                                        PrintBrowseUI();
                                        SetCursorInteract();
                                        PrintInteraction(interactions[index]);
                                    }                                        
                                }                                    
                            }
                            else if (index >= interactions.Count)
                            {
                                return true;
                            }
                        }
                        break;
                    case ConsoleKey.Backspace:
                        {
                            quit = true;
                        }
                        break;
                }
            } while (!quit);
            return false;
        }

        protected void PrintBrowseUI(bool start = false, bool end = false)
        {
            if (start)
            {
                SetCursorInteract(offsetY: 3);
                Console.Write("Next -->");
            }
            else if (end)
            {
                SetCursorInteract(offsetY: 3);
                Console.Write("<-- Previous");
            }
            else
            {
                SetCursorInteract(offsetY: 3);
                Console.Write("<-- Previous . Next -->");
            }
            
        }

        protected void PrintInteraction(InteractionData interaction)
        {
            SetCursorInteract();
            Console.ForegroundColor = interaction.foreground;
            Console.BackgroundColor = interaction.background;
            Console.Write(interaction.line);
            Console.ResetColor();
        }

        protected void SetCursorInteract(int offsetX = 0, int offsetY = 0)
        {
            Console.SetCursorPosition(Utils.UIInteractOffset + offsetX, Utils.UIInteractHeight + offsetY);
        }
    }
}
