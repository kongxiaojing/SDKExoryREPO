using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Corki
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Logics
    {
        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void Combo(EventArgs args)
        {
            if (Bools.HasSheenBuff() ||
                !Targets.Target.IsValidTarget() ||
                Invulnerable.Check(Targets.Target))
            {
                return;
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Targets.Target.IsValidTarget(Vars.AARange) &&
                Vars.Menu["spells"]["e"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.E.Cast();
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                Targets.Target.IsValidTarget(Vars.Q.Range) &&
                Vars.Menu["spells"]["q"]["combo"].GetValue<MenuBool>().Value)
            {
                Vars.Q.Cast(Vars.Q.GetPrediction(Targets.Target).CastPosition);
            }

            /// <summary>
            ///     The R Combo Logic.
            /// </summary>
            if (Vars.R.IsReady() &&
                Targets.Target.IsValidTarget(Vars.R.Range) &&
                Vars.Menu["spells"]["r"]["combo"].GetValue<MenuBool>().Value)
            {
                if (!Vars.R.GetPrediction(Targets.Target).CollisionObjects.Any(c => Targets.Minions.Contains(c)))
                {
                    Vars.R.Cast(Vars.R.GetPrediction(Targets.Target).UnitPosition);
                }
            }
        }
    }
}