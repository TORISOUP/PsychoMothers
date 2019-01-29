using System.Collections;
using System.Collections.Generic;
using PsycthoMothers.Battle.ActionEffects;
using PsycthoMothers.Battle.Inputs;
using UniRx;
using UnityEngine;
using PsycthoMothers.Battle.ActionEffects.Impls;
using PsycthoMothers.Battle.Manager;
using UniRx.Triggers;
using Zenject;

namespace PsycthoMothers.Battle.Players
{
    [RequireComponent(typeof(Rigidbody2D))]
    class PlayerMover : MonoBehaviour
    {
        /// <summary>
        /// Rigidbody2D
        /// </summary>
        [SerializeField] private Rigidbody2D _rigidbody2D;

        [SerializeField] private float DefaultMoveSpeed = 3;
        private float _currentMoveSpeed;

        [Inject] private GameStateManager _gameStateManager;

        private Vector3 _moveVelocity;

        private float _ignoreTime = 0;
        private float _slowTime = 0;

        /// <summary>
        /// Start
        /// </summary>
        protected void Start()
        {
            var inputEvent = GetComponent<IInputEventProvider>();
            var actionEffect = GetComponent<PlayerActionEffectAffecter>();
            _currentMoveSpeed = DefaultMoveSpeed;
            StartCoroutine(TimerCoroutine());

            //移動
            inputEvent.MoveDirection.Subscribe(x => _moveVelocity = x);

            this.FixedUpdateAsObservable()
                .Where(_ => _gameStateManager.CurrentState.Value != GameState.Battle)
                .Subscribe(_ => { _rigidbody2D.velocity = Vector2.zero; });

            this.FixedUpdateAsObservable()
                .Where(_ => _gameStateManager.CurrentState.Value == GameState.Battle
                            && _ignoreTime <= 0)
                .Subscribe(_ =>
                {
                    _rigidbody2D.MovePosition(transform.position + _moveVelocity
                                              * _currentMoveSpeed * Time.fixedDeltaTime);
                });

            actionEffect.CurrentActionEffect
                .BatchFrame(0, FrameCountType.FixedUpdate)
                .Subscribe(effect =>
                {
                    foreach (var a in effect)
                    {
                        AffectActionEffect(a);
                    }
                });
        }

        private void AffectActionEffect(ActionEffect effect)
        {
            switch (effect)
            {
                case BlowOffEffect blowOffEffect:

                    _rigidbody2D.AddForce(blowOffEffect.Power * 20, ForceMode2D.Impulse);
                    _ignoreTime = blowOffEffect.Duration * 0.1f;
                    break;
                case SlowMoveEffect slowMoveEffect:
                    _currentMoveSpeed = DefaultMoveSpeed * 0.3f;
                    _slowTime = slowMoveEffect.DurationSeconds;
                    break;
            }
        }


        IEnumerator TimerCoroutine()
        {
            while (true)
            {
                _ignoreTime -= Time.deltaTime;
                _slowTime -= Time.deltaTime;
                if (_slowTime <= 0)
                {
                    _currentMoveSpeed = DefaultMoveSpeed;
                }

                yield return null;
            }
        }
    }
}
