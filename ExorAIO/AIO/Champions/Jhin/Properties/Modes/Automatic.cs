using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Utils;
using LeagueSharp.SDK.Enumerations;
using LeagueSharp.SDK.UI;

namespace ExorAIO.Champions.Jhin
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
        public static void Automatic(EventArgs args)
        {
            if (GameObjects.Player.IsRecalling() ||
                Vars.R.Instance.Name.Equals("JhinRShot"))
            {
                return;
            }

            /// <summary>
            ///     The Automatic Q LastHit Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.HasBuff("JhinPassiveReload") &&
                Variables.Orbwalker.ActiveMode != OrbwalkingMode.Combo &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["lasthit"]) &&
                Vars.Menu["spells"]["q"]["lasthit"].GetValue<MenuSliderButton>().BValue)
            {
                foreach (var minion in Targets.Minions.Where(
                    m =>
                        m.IsValidTarget(Vars.Q.Range) &&
                        Vars.GetRealHealth(m) <
                            (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.Q)))
                {
                    Vars.Q.CastOnUnit(minion);
                }
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (Vars.W.IsReady() &&
                !GameObjects.Player.IsUnderEnemyTurret() &&
                Vars.Menu["spells"]["w"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        !Invulnerable.Check(t) &&
                        t.HasBuff("jhinespotteddebuff") &&
                        t.IsValidTarget(Vars.W.Range-150f) &&
                        !t.IsValidTarget(Vars.AARange+50f) &&
                        Vars.Menu["spells"]["w"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value))
                {
                    if (!Vars.W.GetPrediction(target).CollisionObjects.Any(c => !c.HasBuff("jhinespotteddebuff")))
                    {
                        if (Bools.IsImmobile(target))
                        {
                            Vars.W.Cast(target.ServerPosition);
                            return;
                        }

                        Vars.W.Cast(Vars.W.GetPrediction(target).UnitPosition);
                    }
                }
            }

            /// <summary>
            ///     The Automatic E Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Vars.Menu["spells"]["e"]["logical"].GetValue<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        Bools.IsImmobile(t) &&
                        !Invulnerable.Check(t) &&
                        t.IsValidTarget(Vars.E.Range)))
                {
                    Vars.E.Cast(GameObjects.Player.ServerPosition.Extend(
                        target.ServerPosition,
                        GameObjects.Player.Distance(target) + target.BoundingRadius*2));
                }
            }
        }
    }
}