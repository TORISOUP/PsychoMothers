using System.Collections;
using System.Collections.Generic;
using PsycthoMothers.Battle.ActionEffects;
using PsycthoMothers.Battle.ActionEffects.Impls;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PsycthoMothers.Battle.Traps.EntityImpls
{
    public class BlackHoleTrapEntity : TrapEntity
    {
        public override TrapType Type => TrapType.BlackHole;

        //[SerializeField] private float _range;
        //[SerializeField] private float _speed;
        //[SerializeField] private float _suctionpower;

        private float _radius;

        HashSet<Transform> _formList = new HashSet<Transform>();

        [SerializeField] private float _maxPower = 1;

        private void Start()
        {
            var col = GetComponent<CircleCollider2D>();
            _radius = col.radius;
            StartCoroutine(AffectCoroutine());

            StartCoroutine(DestroyCoroutine());
        }

        private void OnDestroy()
        {
            _formList.Clear();
        }

        private IEnumerator AffectCoroutine()
        {
            while (true)
            {
                foreach (var t in _formList)
                {
                    if (t == null) continue;
                    var a = t.GetComponent<IActionEffectAffectable>();
                    if (a == null) continue;

                    var hole = transform.position;
                    var target = t.position;
                    var d = hole - target;
                    
                    var lengthRate = d.magnitude / _radius;

                    var power = _maxPower * lengthRate;
                    var effect = new BlowOffEffect((d.normalized * power) , 0.1f, Attacker);

                    a.Affect(effect);
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

        private IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(10.0f);
            var anim = GetComponent<Animator>();
            anim.Play("BlackHole_End");
            Destroy(gameObject, 0.8f);
        }

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            var a = collision.gameObject.GetComponent<IActionEffectAffectable>();
            if (a == null) return;
            _formList.Add(collision.transform);
        }

        private void OnTriggerExit2D(UnityEngine.Collider2D collision)
        {
            var a = collision.gameObject.GetComponent<IActionEffectAffectable>();
            if (a == null) return;
            _formList.Remove(collision.transform);
        }
    }

}
