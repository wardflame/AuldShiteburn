using AuldShiteburn.EntityData;
using System;
using System.Collections.Generic;

namespace AuldShiteburn.MapData.TileData.Tiles
{
    [Serializable]
    internal abstract class InteractionTile : Tile
    {
        public virtual string NPCName { get; }
        protected InteractionTile(string displayChar) : base(displayChar, true)
        {
            InitLines();
        }

        protected abstract void InitLines();
        protected abstract void Interaction();

        /// Put player in menu, unable to move, and run the interaction method.
        public override void OnCollision(Entity entity)
        {
            if (entity is PlayerEntity player)
            {
                player.InMenu = true;
                Interaction();
                player.InMenu = false;
            }
        }

        /// <summary>
        /// Return a custom string. Used for clarity when writing
        /// dialogue and narration into interactions.
        /// </summary>
        /// <param name="description">String to return.</param>
        /// <returns></returns>
        protected string Description(string description)
        {
            return description;
        }

        /// <summary>
        /// Returns a string using Description(), but with
        /// the NPCs name appended to the start to signify
        /// speech.
        /// </summary>
        /// <param name="dialogue">Dialogue string to return.</param>
        /// <returns></returns>
        protected string Dialogue(string dialogue)
        {
            return Description($"{NPCName}: {dialogue}");
        }

        /// <summary>
        /// Get a yes/no response from the player, with custom
        /// yes and no messages if necessary. E.g. Rather than
        /// the default "Accept", one might use "Take" or
        /// "Unlock".
        /// </summary>
        /// <param name="promptYes">Custom yes string.</param>
        /// <param name="promptNo">Custom no string.</param>
        /// <returns></returns>
        protected bool Decision(string promptYes = "Accept", string promptNo = "Refuse")
        {
            Console.Write(Description($"{promptYes} (Y) {promptNo} (N)"));
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

        /// <summary>
        /// Get a list of interactions and allow the player to cycle
        /// through them using the left and right arrow keys. In some
        /// cases, do not allow them to go back or forward depending
        /// on their decisions.
        /// </summary>
        /// <param name="interactions">List of interactions to cycle through.</param>
        /// <param name="decisionRefusal">If they is a decision prompt, provide a string to display if the player refuses.</param>
        /// <returns></returns>
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

                                if (index <= 0)
                                {
                                    Map.Instance.ClearInteractInterface(offsetY: 3);
                                    PrintBrowseUI(true);
                                    Utils.SetCursorInteract();
                                    PrintInteraction(interactions[index]);
                                }
                                else if (index <= interactions.Count - 1 && index > 0)
                                {
                                    Map.Instance.ClearInteractInterface(offsetY: 3);
                                    PrintBrowseUI();
                                    Utils.SetCursorInteract();
                                    PrintInteraction(interactions[index]);
                                }
                                else
                                {
                                    Map.Instance.ClearInteractInterface();
                                    Utils.SetCursorInteract();
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
                                    Utils.SetCursorInteract();
                                    Map.Instance.ClearInteractInterface();
                                    PrintInteraction(interactions[index]);
                                    Utils.SetCursorInteract(0, 1);
                                    if (Decision())
                                    {
                                        index++;
                                        Map.Instance.ClearInteractInterface();
                                        Utils.SetCursorInteract();
                                        PrintInteraction(interactions[index]);
                                    }
                                    else
                                    {
                                        Map.Instance.ClearInteractInterface(offsetY: 2);
                                        Utils.SetCursorInteract();
                                        Console.Write(Dialogue(decisionRefusal));
                                        Utils.SetCursorInteract(offsetY: 3);
                                        Console.Write(new string(' ', Console.WindowWidth - Utils.UIInteractOffset));
                                        quit = true;
                                    }
                                }
                                else
                                {
                                    if (index >= interactions.Count - 1)
                                    {
                                        Map.Instance.ClearInteractInterface(offsetY: 3);
                                        PrintBrowseUI(end: true);
                                        Utils.SetCursorInteract();
                                        PrintInteraction(interactions[index]);
                                    }
                                    else
                                    {
                                        Map.Instance.ClearInteractInterface(offsetY: 3);
                                        PrintBrowseUI();
                                        Utils.SetCursorInteract();
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
                            if (index >= interactions.Count - 1)
                            {
                                Utils.SetCursorInteract(offsetY: 3);
                                Console.Write(new string(' ', Console.WindowWidth - Utils.UIInteractOffset));
                                return true;
                            }
                        }
                        break;
                }
            } while (!quit);
            return false;
        }

        /// <summary>
        /// Set the cursor to the interaction control prompt location
        /// and display simple instruction on how to navigate the menu.
        /// Adapt what's written depending on whether the player is at
        /// the beginning or end of the interaction menu.
        /// </summary>
        /// <param name="start">If true, only display forward-leading prompt.</param>
        /// <param name="end">If true, only display backward-leading prompt or prompt to exit interaction.</param>
        protected void PrintBrowseUI(bool start = false, bool end = false)
        {
            if (start)
            {
                Utils.SetCursorInteract(offsetY: 3);
                Console.Write("Next -->");
            }
            else if (end)
            {
                Utils.SetCursorInteract(offsetY: 3);
                Console.Write("<-- Previous . Press Backspace to Leave");
            }
            else
            {
                Utils.SetCursorInteract(offsetY: 3);
                Console.Write("<-- Previous . Next -->");
            }

        }

        /// <summary>
        /// Get an interaction string and print in its assigned
        /// colour.
        /// </summary>
        /// <param name="interaction"></param>
        protected void PrintInteraction(InteractionData interaction)
        {
            Utils.SetCursorInteract();
            Console.ForegroundColor = interaction.foreground;
            Console.BackgroundColor = interaction.background;
            Console.Write(interaction.line);
            Console.ResetColor();
        }
    }
}
