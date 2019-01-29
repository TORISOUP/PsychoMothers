using System;
using System.Collections;
using PsycthoMothers.Battle.ActionEffects;
using PsycthoMothers.Battle.ActionEffects.Impls;
using PsycthoMothers.Battle.Inputs;
using UnityEngine;
using UniRx;
using UniRx.Async;
using UniRx.Async.Triggers;
using UniRx.Triggers;

namespace PsycthoMothers.Battle.Kids
{
    public class KidsMover : MonoBehaviour
    {
        private Vector3 _moveDirection;
        private Rigidbody2D _rigidbody2D;
        [SerializeField] private float DefaultAccelation = 5;
        [SerializeField] private float MaxMoveSpeed = 5;
        [SerializeField] private float InitializeMoveSpeed = 6;

        private float _currentMoveSpeed;

        private bool _initialized = false;

        private float _slowDuration = 0;
        private float _blowOffTime = 0;

        public async UniTaskVoid InitMove(Vector3 direction)
        {
            _currentMoveSpeed = MaxMoveSpeed;

            var collider = GetComponent<Collider2D>();
            collider.isTrigger = true;

            _moveDirection = direction.normalized;
            await UniTask.Delay(TimeSpan.FromSeconds(1.5),
                cancellationToken: this.GetCancellationTokenOnDestroy());

            _moveDirection = Vector3.zero;
            collider.isTrigger = false;

            _initialized = true;

            // 入場後の動き
            this.FixedUpdateAsObservable()
                .Where(_ => _blowOffTime <= 0)
                .Subscribe(_ =>
                {
                    if (_moveDirection == Vector3.zero)
                    {
                        _rigidbody2D.velocity = Vector2.zero;
                        return;
                    }

                    var dot = Vector2.Dot(_moveDirection.normalized, _rigidbody2D.velocity);
                    if (dot <= _currentMoveSpeed)
                    {
                        _rigidbody2D.AddForce(_moveDirection * DefaultAccelation, ForceMode2D.Impulse);
                    }
                });

            var affecter = GetComponent<KidsActionEffectAffecter>();

            // effect
            affecter.CurrentActionEffect
                .BatchFrame(0, FrameCountType.FixedUpdate)
                .Subscribe(x =>
                {
                    foreach (var actionEffect in x)
                    {
                        ApplyEffect(actionEffect);
                    }
                }).AddTo(this);
        }

        private void ApplyEffect(ActionEffect e)
        {
            switch (e)
            {
                case BlowOffEffect blowOffEffect:
                    _blowOffTime = blowOffEffect.Duration;
                    _rigidbody2D.AddForce(blowOffEffect.Power * 100, ForceMode2D.Impulse);
                    break;
                case SlowMoveEffect slowMoveEffect:
                    _currentMoveSpeed = MaxMoveSpeed * 0.1f;
                    if (_rigidbody2D.velocity.magnitude > _currentMoveSpeed)
                    {
                        _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * _currentMoveSpeed;
                    }
                    _slowDuration = slowMoveEffect.DurationSeconds;
                    break;
            }
        }

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            var i = GetComponent<IInputEventProvider>();

            i.MoveDirection
                .Where(_ => _initialized)
                .Subscribe(x => { _moveDirection = x; });

            // 入場前
            this.FixedUpdateAsObservable()
                .TakeWhile(_ => !_initialized)
                .Subscribe(_ =>
                {
                    var dot = Vector2.Dot(_moveDirection.normalized, _rigidbody2D.velocity);
                    if (dot <= InitializeMoveSpeed)
                    {
                        _rigidbody2D.AddForce(_moveDirection * DefaultAccelation, ForceMode2D.Impulse);
                    }
                });

            StartCoroutine(SlowCoroutine());
        }

        IEnumerator SlowCoroutine()
        {
            while (true)
            {
                _blowOffTime -= Time.deltaTime;
                if (_slowDuration > 0)
                {
                    _slowDuration -= Time.deltaTime;
                    if (_slowDuration <= 0)
                    {
                        _currentMoveSpeed = MaxMoveSpeed;
                    }
                }

                yield return null;
            }
        }
    }
}
