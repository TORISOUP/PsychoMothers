using System;
using PsycthoMothers.Battle.Houses;
using PsycthoMothers.Battle.Players;
using PsycthoMothers.Common;
using UniRx;
using UnityEngine;
using Zenject;

namespace PsycthoMothers.Battle.Manager
{
    public class ScoreManager : MonoBehaviour
    {
        private ReactiveDictionary<PlayerId, int> _scoreDictionary
            = new ReactiveDictionary<PlayerId, int>();

        public IReadOnlyReactiveDictionary<PlayerId, int> Score => _scoreDictionary;

        [Inject] private BattleMenuInfo _info;

        /// <summary>
        /// GameStateManager
        /// </summary>
        [Inject] private GameStateManager _gameStateManager;        
        
        [SerializeField] private House[] _houses;

        private AsyncSubject<Unit> _initializAsyncSubject = new AsyncSubject<Unit>();
        public IObservable<Unit> Initialized => _initializAsyncSubject;

        private void Start()
        {
            for (int i = 0; i < _info.PlayerCount; i++)
            {
                var x = i;
                _scoreDictionary[(PlayerId) x] = 0;
                _houses[x].Score
                    .Where(_ => _gameStateManager.CurrentState.Value == GameState.Battle)
                    .Subscribe(s =>
                    {
                        _scoreDictionary[(PlayerId) x] = s;
                    });
            }

            _initializAsyncSubject.OnNext(Unit.Default);
            _initializAsyncSubject.OnCompleted();
        }
    }
}
