using System.Collections.Generic;
using System.Linq;
using ExorAIO.Utilities;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.Utils;

namespace ExorAIO.Champions.Cassiopeia
{
    /// <summary>
    ///     The targets class.
    /// </summary>
    internal class Targets
    {
        /// <summary>
        ///     The main hero target.
        /// </summary>
        public static Obj_AI_Hero Target => Variables.TargetSelector.GetTarget(Vars.W.Range, DamageType.Magical);

        /// <summary>
        ///     The minions target.
        /// </summary>
        public static List<Obj_AI_Minion> Minions
            =>
                GameObjects.EnemyMinions.Where(
                    m =>
                        m.IsMinion() &&
                        m.IsValidTarget(Vars.W.Range)).ToList();

        /// <summary>
        ///     The jungle minion targets.
        /// </summary>
        public static List<Obj_AI_Minion> JungleMinions
            =>
                GameObjects.Jungle.Where(
                    m =>
                        m.IsValidTarget(Vars.W.Range) &&
                        !GameObjects.JungleSmall.Contains(m)).ToList();

        /// <summary>
        ///     The ultimate targets.
        /// </summary>
        public static List<Obj_AI_Hero> RTargets
            =>
                GameObjects.EnemyHeroes.Where(
                    t =>
                        t.IsValidTarget(Vars.R.Range-100f) &&
                        t.IsFacing(GameObjects.Player)).ToList();
    }
}