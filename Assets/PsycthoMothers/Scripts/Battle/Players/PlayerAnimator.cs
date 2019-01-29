using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UniRx;
using PsycthoMothers.Battle.Inputs;
using PsycthoMothers.Battle.Manager;
using Zenject;

namespace PsycthoMothers.Battle.Players
{
    /// <summary>
    /// PlayerAnimator
    /// アニメーション管理
    /// </summary>
    [RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
    public class PlayerAnimator : MonoBehaviour
    {
        /// <summary>
        /// Rigidbody2D
        /// </summary>
        [SerializeField] private Rigidbody2D _rigidbody2D;

        /// <summary>
        /// Animator
        /// </summary>
        [SerializeField] private Animator _anim;

        [Inject] private GameStateManager _gameStateManager;

        private Vector3 _moveVelocity;

        /// <summary>
        /// Start
        /// </summary>
        protected void Start()
        {
            var inputEvent = GetComponent<IInputEventProvider>();

            //移動
            inputEvent.MoveDirection.Subscribe(x => _moveVelocity = x);

            this.UpdateAsObservable()
                .Where(_ => _gameStateManager.CurrentState.Value == GameState.Battle)
                .Subscribe(_ =>
                {
                    if (_moveVelocity == Vector3.zero) return;
                    _anim.SetFloat("x", _moveVelocity.x);
                    _anim.SetFloat("y", _moveVelocity.y);
                });
        }
    }
}
