using PsycthoMothers.Battle.Players;

namespace PsycthoMothers.Battle.ActionEffects
{
    public abstract class ActionEffect
    {
        public abstract PlayerId Attacker { get; }
    }
}
