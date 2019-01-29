using PsycthoMothers.Battle.Players;
using UnityEngine;


namespace PsycthoMothers.Battle.ActionEffects.Impls
{
    /// <summary>
    /// 強制的に１方向に移動させる
    /// </summary>
    public class MoveToOneWayEffect : ActionEffect
    {
        public override PlayerId Attacker { get; }
        public readonly Vector3 Direction;
        public readonly float DurationSeconds;

        public MoveToOneWayEffect(Vector3 direction, float durationSeconds, PlayerId attacker)
        {
            Direction = direction;
            DurationSeconds = durationSeconds;
            Attacker = attacker;
        }
    }
}
