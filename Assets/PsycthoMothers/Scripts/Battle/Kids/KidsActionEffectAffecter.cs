using PsycthoMothers.Battle.ActionEffects;
using PsycthoMothers.Battle.ActionEffects.Impls;
using PsycthoMothers.Battle.Players;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace PsycthoMothers.Battle.Kids
{
    public class KidsActionEffectAffecter : MonoBehaviour, IActionEffectAffectable
    {
        private ReactiveProperty<ActionEffect> _currentActionEffect = new ReactiveProperty<ActionEffect>();
        public IReadOnlyReactiveProperty<ActionEffect> CurrentActionEffect => _currentActionEffect;

        public void Affect(ActionEffect effect)
        {
            _currentActionEffect.Value = effect;
        }

#if UNITY_EDITOR
        private void Start()
        {
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        Affect(new BlowOffEffect(new Vector3(1, 1, 0).normalized,3, PlayerId.Player1));
                    }
                    else if (Input.GetKeyDown(KeyCode.E))
                    {
                        Affect(new SlowMoveEffect(PlayerId.Player1, 2));
                    }
                });
        }
#endif
    }
}
