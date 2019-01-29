using PsycthoMothers.Battle.ActionEffects;
using PsycthoMothers.Battle.ActionEffects.Impls;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace PsycthoMothers.Battle.Players
{
    class PlayerActionEffectAffecter : MonoBehaviour, IActionEffectAffectable
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
                        Affect(new BlowOffEffect(Vector3.up, 5, PlayerId.Player1));
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
