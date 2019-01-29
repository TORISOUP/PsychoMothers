using PsycthoMothers.Battle.Players;

namespace PsycthoMothers.Battle.ActionEffects.Impls
{
    public class SlowMoveEffect : ActionEffect
    {
        public override PlayerId Attacker { get; }

        public float DurationSeconds { get; }

        public SlowMoveEffect(PlayerId attacker, float durationSeconds)
        {
            Attacker = attacker;
            DurationSeconds = durationSeconds;
        }
    }
}
