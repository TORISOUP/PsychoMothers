using PsycthoMothers.Battle.Kids;
using PsycthoMothers.Battle.Players;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace PsycthoMothers.Battle.Houses
{
    public class House : MonoBehaviour
    {
        [SerializeField] private PlayerId _playerId;
        public PlayerId PlayerId => _playerId;
        private ReactiveProperty<int> _score = new ReactiveProperty<int>(0);
        public IReadOnlyReactiveProperty<int> Score => _score;

        //public ReactiveProperty<bool> IsIgnite = new ReactiveProperty<bool>();
        
        private void Start()
        {
            this.OnCollisionEnter2DAsObservable()
                .Subscribe(x =>
                {
                    var k = x.gameObject.GetComponent<KidsCore>();
                    if (k == null) return;
                    _score.Value += k.KidsType == KidsType.Normal ? 1 : 5;
                    Destroy(k.gameObject);
                });
        }
    }
}
