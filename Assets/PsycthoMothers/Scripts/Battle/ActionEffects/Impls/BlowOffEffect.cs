using PsycthoMothers.Battle.Players;
using UnityEngine;

namespace PsycthoMothers.Battle.ActionEffects.Impls
{
    /// <summary>
    /// ふっとばす
    /// </summary>
    public class BlowOffEffect : ActionEffect
    {
        public override PlayerId Attacker { get; }
        public readonly Vector3 Power;
        public readonly float Duration;

        public BlowOffEffect(Vector3 power, float duration, PlayerId attacker)
        {
            Power = power;
            Attacker = attacker;
            Duration = duration;
        }
    }
}
