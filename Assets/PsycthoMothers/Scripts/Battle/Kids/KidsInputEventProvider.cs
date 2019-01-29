using System;
using System.Collections;
using System.Threading;
using PsycthoMothers.Battle.Inputs;
using UniRx;
using UniRx.Async;
using UniRx.Async.Triggers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PsycthoMothers.Battle.Kids
{
    /// <summary>
    /// Kidsの方向を決定したりする
    /// </summary>
    class KidsInputEventProvider : MonoBehaviour, IInputEventProvider
    {
        public IReadOnlyReactiveProperty<Vector3> MoveDirection => _move;
        public IReadOnlyReactiveProperty<bool> OnUseButtonPushed => _onUse;

        private ReactiveProperty<Vector3> _move = new ReactiveProperty<Vector3>();
        private BoolReactiveProperty _onUse = new BoolReactiveProperty(false); //maybe fixed "false"

        /// <summary>
        /// この距離より近くなったら逃げる
        /// </summary>
        [SerializeField] private float AwayThreshold = 5;

        public Transform[] AwayFromTransforms;
        private Vector3 _randomMoveVector;

        private async void Start()
        {
            RandomMoveCoroutine(this.GetCancellationTokenOnDestroy()).Forget();
        }

        /// <summary>
        /// 適当に移動する
        /// </summary>
        private async UniTaskVoid RandomMoveCoroutine(CancellationToken token)
        {
            while (true)
            {
                // 止まったり移動したり
                if (Random.Range(0, 10) < 3)
                {
                    // Stop
                    _randomMoveVector = Vector3.zero;
                    await UniTask.Delay(Random.Range(500, 1000), cancellationToken: token);
                }
                else
                {
                    // Move random
                    _randomMoveVector =
                        Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward)
                        * Vector3.up;
                    await UniTask.Delay(Random.Range(1000, 4000), cancellationToken: token);
                }
            }
        }


        private void Update()
        {
            if (AwayFromTransforms == null || AwayFromTransforms.Length == 0)
            {
                _move.SetValueAndForceNotify(_randomMoveVector);
                return;
            }

            var awayDirection = new Vector3();
            var p = transform.position;

            // 近いMotherから逃げるベクトルを作る
            foreach (var m in AwayFromTransforms)
            {
                if ((p - m.position).magnitude < AwayThreshold)
                {
                    awayDirection += (p - m.position).normalized;
                }
            }

            // 逃げるベクトルがあるならそっち優先
            if (awayDirection == Vector3.zero)
            {
                _move.Value = _randomMoveVector;
            }
            else
            {
                _move.Value = awayDirection.normalized;
            }
        }
    }
}
