using LeagueSharp;
using LeagueSharp.SDK;

namespace NabbActivator
{
    /// <summary>
    ///     The spells class.
    /// </summary>
    internal class ISpells
    {
        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public static void Initialize()
        {
            Vars.W = new Spell(SpellSlot.W);
            Vars.Smite = new Spell(SpellSlots.GetSmiteSlot(), 500f);
        }
    }
}