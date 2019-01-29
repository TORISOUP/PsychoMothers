using System.Collections;
using PsycthoMothers.Battle.ActionEffects;
using PsycthoMothers.Battle.ActionEffects.Impls;
using PsycthoMothers.Battle.Players;
using UniRx.Triggers;
using UnityEngine;
using UniRx;

namespace PsycthoMothers.Battle.Traps.EntityImpls
{
    /// <summary>
    /// 爆風
    /// </summary>
    public class Explosion : MonoBehaviour
    {
        private CircleCollider2D _collider;

        [SerializeField] private float _seconds;

        [SerializeField] private float _power = 10;

        private PlayerId _id;

        public void Initialize(PlayerId id)
        {
            _id = id;
        }

        void Start()
        {
            _collider = GetComponent<CircleCollider2D>();

            Destroy(gameObject, _seconds);

            this.OnTriggerEnter2DAsObservable()
                .Subscribe(c =>
                {
                    var affectable = c.gameObject.GetComponent<IActionEffectAffectable>();
                    if (affectable == null) return;
                    var bomb = transform.position;
                    var target = c.gameObject.transform.position;
                    var d = target - bomb;

                    affectable.Affect(new BlowOffEffect(_power * d.normalized, 7, _id));
                });
        }
    }
}
