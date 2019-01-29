using PsycthoMothers.Battle.Inputs;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace PsycthoMothers.Battle.Players.InputImpls
{
    public class MultiPlayerInputEventProvider : MonoBehaviour, IInputEventProvider
    {
        private readonly ReactiveProperty<bool> _onUse = new BoolReactiveProperty();
        private readonly ReactiveProperty<Vector3> _moveDirection = new ReactiveProperty<Vector3>();
        public IReadOnlyReactiveProperty<Vector3> MoveDirection => _moveDirection;
        public IReadOnlyReactiveProperty<bool> OnUseButtonPushed => _onUse;

        private async void Start()
        {
            var core = GetComponent<PlayerCore>();
            await core.OnInitialized;
            SetPlayerId(1 + (int) core.PlayerId);
        }

        private void SetPlayerId(int playerId)
        {
            var id = playerId;

            var useButton = "Use" + id;
            var hori = "Horizontal" + id;
            var vert = "Vertical" + id;

            this.UpdateAsObservable()
                .Select(_ => Input.GetButton(useButton))
                .DistinctUntilChanged()
                .Subscribe(x => _onUse.Value = x);

            this.UpdateAsObservable()
                .Select(_ => new Vector3(Input.GetAxis(hori), -Input.GetAxis(vert), 0))
                .Subscribe(x => { _moveDirection.SetValueAndForceNotify(x); });

        }
    }
}
