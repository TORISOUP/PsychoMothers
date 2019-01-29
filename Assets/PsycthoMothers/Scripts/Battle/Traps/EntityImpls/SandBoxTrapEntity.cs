using System.Collections;
using System.Collections.Generic;
using PsycthoMothers.Battle.ActionEffects;
using PsycthoMothers.Battle.ActionEffects.Impls;
using UnityEngine;
using UniRx;

namespace PsycthoMothers.Battle.Traps.EntityImpls
{
    class SandBoxTrapEntity : TrapEntity
    {
        public override TrapType Type => TrapType.SandBox;

        HashSet<IActionEffectAffectable> _affectsList = new HashSet<IActionEffectAffectable>();

        public ReactiveProperty<bool> IsSand = new ReactiveProperty<bool>();

        
        private void Start()
        {
            StartCoroutine(AffectCoroutine());

            Destroy(gameObject, 10);
        }

        private void OnDestroy()
        {
            _affectsList.Clear();
        }

        private IEnumerator AffectCoroutine()
        {
            while (true)
            {
                foreach (var a in _affectsList)
                {
                    if (a == null) continue;
                    a.Affect(new SlowMoveEffect(Attacker, 0.2f));
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            var a = collision.gameObject.GetComponent<IActionEffectAffectable>();
            if (a == null) return;
            a.Affect(new SlowMoveEffect(Attacker, 0.2f));
            _affectsList.Add(a);
            IsSand.SetValueAndForceNotify(true);
        }

        private void OnTriggerExit2D(UnityEngine.Collider2D collision)
        {
            var a = collision.gameObject.GetComponent<IActionEffectAffectable>();
            if (a == null) return;
            _affectsList.Remove(a);
            IsSand.SetValueAndForceNotify(false);
        }
    }
}
