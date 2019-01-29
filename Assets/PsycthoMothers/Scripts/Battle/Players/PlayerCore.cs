using System;
using UniRx;
using UnityEngine;

namespace PsycthoMothers.Battle.Players
{
    public class PlayerCore : MonoBehaviour
    {
        [SerializeField] private PlayerId _playerId;
        public PlayerId PlayerId => _playerId;

        private AsyncSubject<Unit> _onInitialized = new AsyncSubject<Unit>();
        public IObservable<Unit> OnInitialized => _onInitialized;

        public void Initialize(PlayerId playerId)
        {
            _playerId = playerId;
            _onInitialized.OnNext(Unit.Default);
            _onInitialized.OnCompleted();
        }

        private void OnBecameInvisible()
        {
            transform.position = Vector3.zero;
        }
    }
}
