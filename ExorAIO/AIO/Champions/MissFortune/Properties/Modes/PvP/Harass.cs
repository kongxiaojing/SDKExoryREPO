using System;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;
using Geometry = ExorAIO.Utilities.Geometry;

namespace ExorAIO.Champions.MissFortune
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
        public static void Harass(EventArgs args)
        {
            if (!GameObjects.EnemyHeroes.Any(
                t =>
                    !Invulnerable.Check(t) &&
                    !t.IsValidTarget(Vars.Q.Range) &&
                    t.IsValidTarget(Vars.Q2.Range-50f) &&
                    Vars.Menu["spells"]["q"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value))
            {
                return;
            }

            /// <summary>
            ///     The Extended Q Mixed Harass Logic.
            /// </summary>
            if (Vars.Q.IsReady() &&
                GameObjects.Player.ManaPercent >
                    ManaManager.GetNeededMana(Vars.Q.Slot, Vars.Menu["spells"]["q"]["extended"]["mixed"]) &&
                Vars.Menu["spells"]["q"]["extended"]["mixed"].GetValue<MenuSliderButton>().BValue)
            {
                /// <summary>
                ///     Through enemy minions.
                /// </summary>
                foreach (var minion 
                    in from minion
                    in Targets.Minions.Where(
                        m =>
                            m.IsValidTarget(Vars.Q.Range) &&
                            Vars.Menu["spells"]["q"]["extended"]["mixedkill"].GetValue<MenuBool>().Value
                                ? m.Health <
                                    (float)GameObjects.Player.GetSpellDamage(m, SpellSlot.Q)
                                : true)

                    let polygon = new Geometry.Sector(
                        (Vector2)minion.ServerPosition,
                        (Vector2)minion.ServerPosition.Extend(GameObjects.Player.ServerPosition, -(Vars.Q2.Range - Vars.Q.Range)),
                        40f * (float)Math.PI / 180f,
                        (Vars.Q2.Range - Vars.Q.Range)-50f)

                    where
                        !polygon.IsOutside((Vector2)GameObjects.EnemyHeroes.FirstOrDefault(
                        t =>
                            !Invulnerable.Check(t) &&
                            !t.IsValidTarget(Vars.Q.Range) &&
                            t.IsValidTarget(Vars.Q2.Range-50f) &&
                            (t.NetworkId == Vars.PassiveTarget.NetworkId ||
                                !Targets.Minions.Any(m => !polygon.IsOutside((Vector2)m.ServerPosition))) &&
                            Vars.Menu["spells"]["q"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value).ServerPosition) &&

                        !polygon.IsOutside((Vector2)Movement.GetPrediction(
                            GameObjects.EnemyHeroes.FirstOrDefault(
                            t =>
                                !Invulnerable.Check(t) &&
                                !t.IsValidTarget(Vars.Q.Range) &&
                                t.IsValidTarget(Vars.Q2.Range-50f) &&
                                (t.NetworkId == Vars.PassiveTarget.NetworkId ||
                                    !Targets.Minions.Any(m => !polygon.IsOutside((Vector2)m.ServerPosition))) &&
                                Vars.Menu["spells"]["q"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value),

                            GameObjects.Player.Distance(GameObjects.EnemyHeroes.FirstOrDefault(
                            t =>
                                !Invulnerable.Check(t) &&
                                !t.IsValidTarget(Vars.Q.Range) &&
                                t.IsValidTarget(Vars.Q2.Range-50f) &&
                                (t.NetworkId == Vars.PassiveTarget.NetworkId ||
                                    !Targets.Minions.Any(m => !polygon.IsOutside((Vector2)m.ServerPosition))) &&
                                Vars.Menu["spells"]["q"]["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value).ServerPosition) / Vars.Q.Speed + Vars.Q.Delay).UnitPosition)

                    select minion)
                {
                    Vars.Q.CastOnUnit(minion);
                }
            }
        }
    }
}