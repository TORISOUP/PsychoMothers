using PsycthoMothers.Battle.Inputs;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace PsycthoMothers.Battle.Players.InputImpls
{
    public class DebugKeyInputEventProvider : MonoBehaviour, IInputEventProvider
    {
        private readonly ReactiveProperty<bool> _onUseButtonPushed = new BoolReactiveProperty(false);
        private readonly ReactiveProperty<Vector3> _moveDirection = new ReactiveProperty<Vector3>();
        public IReadOnlyReactiveProperty<Vector3> MoveDirection => _moveDirection;
        public IReadOnlyReactiveProperty<bool> OnUseButtonPushed => _onUseButtonPushed;

        protected void Start()
        {
            this.UpdateAsObservable()
                .Select(_ => Input.GetKey(KeyCode.Z))
                .DistinctUntilChanged()
                .Subscribe(x => _onUseButtonPushed.Value = x);

            this.UpdateAsObservable()
                .Select(_ => 
                    new Vector3(Input.GetAxisRaw("Horizontal1"), Input.GetAxisRaw("Vertical1"), 0))
                .Subscribe(x => _moveDirection.SetValueAndForceNotify(x));
        }
    }
}
