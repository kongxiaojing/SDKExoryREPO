using LeagueSharp;

namespace ExorAIO.Champions.Sivir
{
    /// <summary>
    ///     The methods class.
    /// </summary>
    internal class Methods
    {
        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public static void Initialize()
        {
            Game.OnUpdate += Sivir.OnUpdate;
            Obj_AI_Base.OnDoCast += Sivir.OnDoCast;
            Obj_AI_Base.OnProcessSpellCast += Sivir.OnProcessSpellCast;
        }
    }
}