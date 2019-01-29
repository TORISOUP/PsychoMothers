using System;
using System.Collections;
using PsycthoMothers.Battle.Players;
using UniRx;
using UniRx.Async;
using UnityEngine;

namespace PsycthoMothers.Battle.Traps.EntityImpls
{
    [RequireComponent(typeof(Animator))]
    public class BombTrapEntity : TrapEntity
    {
        public override TrapType Type => TrapType.Bomb;

        [SerializeField] private GameObject _explosionEffect;
        
        public ReactiveProperty<bool> IsIgnite = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> IsExplosion = new ReactiveProperty<bool>();

        async void Start()
        {
            var anim = GetComponent<Animator>();
            IsIgnite.SetValueAndForceNotify(true);

            await UniTask.Delay(TimeSpan.FromSeconds(2));

            anim.Play("BombExplosion");
            IsExplosion.SetValueAndForceNotify(true);

            await UniTask.Yield(PlayerLoopTiming.PostLateUpdate);

            transform.localScale = new Vector3(3, 3, 3);

            var go = Instantiate(_explosionEffect, transform.position, Quaternion.identity, null);
            go.GetComponent<Explosion>().Initialize(Attacker);

            //    StartCoroutine(HitStop());

            await UniTask.Delay(TimeSpan.FromSeconds(1));
            Destroy(gameObject);
        }

        private IEnumerator HitStop()
        {
            Time.timeScale = 0f;
            yield return new WaitForSecondsRealtime(1);
            Time.timeScale = 1;
        }
    }
}
