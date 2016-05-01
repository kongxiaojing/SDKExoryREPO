using ExorAIO.Utilities;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Ryze
{
    /// <summary>
    ///     The menu class.
    /// </summary>
    internal class Menus
    {
        /// <summary>
        ///     Sets the menu.
        /// </summary>
        public static void Initialize()
        {
            /// <summary>
            ///     Sets the spells menu.
            /// </summary>
            Vars.SpellsMenu = new Menu("spells", "Spells");
            {
                /// <summary>
                ///     Sets the menu for the Q.
                /// </summary>
                Vars.QMenu = new Menu("q", "Use Q to:");
                {
                    Vars.QMenu.Add(new MenuBool("combo",       "Combo",     true));
                    Vars.QMenu.Add(new MenuBool("harass",      "Harass",    true));
                    Vars.QMenu.Add(new MenuBool("killsteal",   "KillSteal", true));
                    Vars.QMenu.Add(new MenuBool("clear",       "Clear",     true));
                    Vars.QMenu.Add(
                        new MenuSlider("manamanager", "Harass/Clear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.QMenu);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                Vars.WMenu = new Menu("w", "Use W to:");
                {
                    Vars.WMenu.Add(new MenuBool("combo",     "Combo",          true));
                    Vars.WMenu.Add(new MenuBool("killsteal", "KillSteal",      true));
                    Vars.WMenu.Add(new MenuBool("gapcloser", "Anti-Gapcloser", true));
                    Vars.WMenu.Add(new MenuBool("clear",     "Clear",          true));
                    Vars.WMenu.Add(
                        new MenuSlider("manamanager", "Clear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.WMenu);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                Vars.EMenu = new Menu("e", "Use E to:");
                {
                    Vars.EMenu.Add(new MenuBool("combo",     "Combo",     true));
                    Vars.EMenu.Add(new MenuBool("killsteal", "KillSteal", true));
                    Vars.EMenu.Add(new MenuBool("clear",     "Clear",     true));
                    Vars.EMenu.Add(
                        new MenuSlider("manamanager", "Clear: Mana >= x%", 50, 0, 99));
                }
                Vars.SpellsMenu.Add(Vars.EMenu);

                /// <summary>
                ///     Sets the menu for the R.
                /// </summary>
                Vars.RMenu = new Menu("r", "Use R to:");
                {
                    Vars.RMenu.Add(new MenuBool("combo", "Combo", true));
                    Vars.RMenu.Add(new MenuBool("clear", "Clear", true));
                }
                Vars.SpellsMenu.Add(Vars.RMenu);
            }
            Vars.Menu.Add(Vars.SpellsMenu);

            /// <summary>
            ///     Sets the miscellaneous menu.
            /// </summary>
            Vars.MiscMenu = new Menu("miscellaneous", "Miscellaneous");
            {
                Vars.MiscMenu.Add(new MenuBool("tear",   "Stack Tear", true));
                Vars.MiscMenu.Add(
                    new MenuSliderButton("stacks", "Keep Passive Stacks:", 3, 1, 4));
                Vars.MiscMenu.Add(
                    new MenuSlider("manamanager", "Keep Passive/Stack Tear: Mana > x%", 75, 1, 95));
            }
            Vars.Menu.Add(Vars.MiscMenu);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            Vars.DrawingsMenu = new Menu("drawings", "Drawings");
            {
                Vars.DrawingsMenu.Add(new MenuBool("q", "Q Range"));
                Vars.DrawingsMenu.Add(new MenuBool("w", "W Range"));
                Vars.DrawingsMenu.Add(new MenuBool("e", "E Range"));
            }
            Vars.Menu.Add(Vars.DrawingsMenu);
        }
    }
}