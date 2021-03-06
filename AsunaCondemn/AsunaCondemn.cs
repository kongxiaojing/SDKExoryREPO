﻿using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.SDK;
using LeagueSharp.SDK.UI;
using LeagueSharp.SDK.Utils;
using SharpDX;

namespace AsunaCondemn
{
    /// <summary>
    ///     The main class.
    /// </summary>
    internal class Condem
    {
        /// <summary>
        ///     Called on game load.
        /// </summary>
        public static void OnLoad()
        {
            /// <summary>
            ///     Initialize the menus.
            /// </summary>
            Menus.Initialize();

            /// <summary>
            ///     Initialize the spells.
            /// </summary>
            Spells.Initialize();

            /// <summary>
            ///     Initializes the methods.
            /// </summary>
            Methods.Initialize();

            /// <summary>
            ///     Initializes the drawings.
            /// </summary>
            Drawings.Initialize();
        }

        /// <summary>
        ///     Called when the game updates itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public static void OnUpdate(EventArgs args)
        {
            if (ObjectManager.Player.IsDead)
            {
                return;
            }

            if (!Vars.Menu["enable"].GetValue<MenuBool>().Value ||
                !Vars.Menu["keybind"].GetValue<MenuKeyBind>().Active)
            {
                return;
            }

            /// <summary>
            ///     The fixed Condem Logic Kappa.
            /// </summary>
            if (Vars.E.IsReady() &&
                Vars.Flash.IsReady() &&
				!GameObjects.Player.IsDashing())
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        !t.IsDashing() &&
                        t.IsValidTarget(Vars.E.Range) &&
                        !Invulnerable.Check(t, DamageType.Magical, false) &&
                        !t.IsValidTarget(GameObjects.Player.BoundingRadius) &&
                        GameObjects.Player.Distance(GameObjects.Player.ServerPosition.Extend(t.ServerPosition, Vars.Flash.Range)) >
                            GameObjects.Player.Distance(t) + t.BoundingRadius &&
                        Vars.Menu["whitelist"][t.ChampionName.ToLower()].GetValue<MenuBool>().Value))
                {
                    for (var i = 1; i < 10; i++)
                    {
                        if ((target.ServerPosition - Vector3.Normalize(target.ServerPosition - GameObjects.Player.ServerPosition) * (float)(i * 42.5)).IsWall() &&
                            (Vars.E.GetPrediction(target).UnitPosition - Vector3.Normalize(target.ServerPosition - GameObjects.Player.ServerPosition) * (float)(i * 42.5)).IsWall() &&
                            (target.ServerPosition - Vector3.Normalize(target.ServerPosition - GameObjects.Player.ServerPosition) * i * 44).IsWall() &&
                            (Vars.E.GetPrediction(target).UnitPosition - Vector3.Normalize(target.ServerPosition - GameObjects.Player.ServerPosition) * i * 44).IsWall())
                        {
                            Vars.E.CastOnUnit(target);
                            Vars.Flash.Cast(GameObjects.Player.ServerPosition.Extend(target.ServerPosition, Vars.Flash.Range));
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.GapCloserEventArgs" /> instance containing the event data.</param>
        public static void OnGapCloser(object sender, Events.GapCloserEventArgs args)
        {
            if (!Vars.Menu["enable"].GetValue<MenuBool>().Value ||
                !Vars.Menu["keybind"].GetValue<MenuKeyBind>().Active ||
				!Vars.Menu["features"]["dashpred"].GetValue<MenuBool>().Value)
            {
                return;
            }

            /// <summary>
            ///     The Dash-Condemn Prediction Logic.
            /// </summary>
            if (Vars.E.IsReady() &&
                Vars.Flash.IsReady() &&
				!GameObjects.Player.IsDashing() &&
                args.Sender.IsValidTarget(Vars.E.Range) &&
                !Invulnerable.Check(args.Sender, DamageType.Magical, false) &&
                GameObjects.Player.Distance(args.End) >
					GameObjects.Player.BoundingRadius &&
                Vars.Menu["whitelist"][args.Sender.ChampionName.ToLower()].GetValue<MenuBool>().Value)
            {
                for (var i = 1; i < 10; i++)
                {
                    if ((args.End - Vector3.Normalize(args.End - GameObjects.Player.ServerPosition) * (float)(i * 42.5)).IsWall() &&
                        (args.End - Vector3.Normalize(args.End - GameObjects.Player.ServerPosition) * i * 44).IsWall())
                    {
                        Vars.E.CastOnUnit(args.Sender);
                        Vars.Flash.Cast(GameObjects.Player.ServerPosition.Extend(args.Sender.ServerPosition, Vars.Flash.Range));
                    }
                }
            }
        }
    }
}