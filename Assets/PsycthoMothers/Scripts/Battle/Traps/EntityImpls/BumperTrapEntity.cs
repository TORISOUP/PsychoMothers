using System;
using PsycthoMothers.Battle.ActionEffects;
using PsycthoMothers.Battle.ActionEffects.Impls;
using UniRx.Triggers;
using UnityEngine;
using UniRx;
using UniRx.Async;
using Random = UnityEngine.Random;

namespace PsycthoMothers.Battle.Traps.EntityImpls
{
    public class BumperTrapEntity : TrapEntity
    {
        public override TrapType Type => TrapType.Bumper;

        [SerializeField] private Transform _centerBar;

        [SerializeField] private float _startRotateSpeed;
        [SerializeField] private float _endRotateSpeed;
        [SerializeField] private float _startPower;
        [SerializeField] private float _endPower;

        [SerializeField] private AnimationCurve _curve;

        private float _startTime;
        private float _currentPower;

        [SerializeField] private float _lifeTime = 15;

        public ReactiveProperty<bool> IsBump = new ReactiveProperty<bool>();

        private bool _isClockwise = false;

        private void Start()
        {
            _currentPower = _startPower;

            _startTime = Time.time;

            _isClockwise = Random.Range(0, 100) > 50;
            IsBump.SetValueAndForceNotify(false);

            _centerBar.OnCollisionEnter2DAsObservable()
                .Subscribe(x =>
                {
                    IsBump.SetValueAndForceNotify(true);
                    var a = x.gameObject.GetComponent<IActionEffectAffectable>();
                    if (a == null) return;
                    var d = (x.transform.position - transform.position).normalized;
                    var n = Quaternion.AngleAxis(90 * (_isClockwise ? 1 : -1), Vector3.forward) * d;
                    a.Affect(new BlowOffEffect(n * _currentPower, 1, Attacker));
                });

            _centerBar.OnCollisionExit2DAsObservable()
                .Subscribe(x =>
                {
                    IsBump.SetValueAndForceNotify(false);
                });

            Destroy(gameObject, _lifeTime);
        }

        void Update()
        {
            var rate = _curve.Evaluate((Time.time - _startTime) / _lifeTime);
            var s = Mathf.Lerp(_startRotateSpeed, _endRotateSpeed, rate);

            _currentPower = Mathf.Lerp(_startPower, _endPower, rate);

            _centerBar.transform.rotation =
                Quaternion.AngleAxis(s * Time.deltaTime * (_isClockwise ? 1 : -1), Vector3.forward) *
                _centerBar.transform.rotation;
        }
    }
}
