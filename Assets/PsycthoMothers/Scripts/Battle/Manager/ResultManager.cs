using System;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;

namespace PsycthoMothers.Battle.Manager
{
    
    /// <summary>
    /// リザルトマネージャークラス
    /// </summary>
    public class ResultManager : MonoBehaviour
    {
        /// <summary>
        /// ScoreManager
        /// スコアマネージャー
        /// </summary>
        [Inject] private ScoreManager _scoreManager;
       
        /// <summary>
        /// GameStateManager
        /// </summary>
        [Inject] private GameStateManager _gameStateManager;
        
        private AsyncSubject<ResultScore[]> _scoreAsyncSubject = new AsyncSubject<ResultScore[]>();
        public IObservable<ResultScore[]> Score => _scoreAsyncSubject;

        /// <summary>
        /// Start
        /// </summary>
        protected void Start()
        {
                    
            _gameStateManager.CurrentState
                .Where(_ => _gameStateManager.CurrentState.Value == GameState.Result)
                .Subscribe(x =>
            {
                // 降順に並び替えて順番を確定させる
                var itemTable =  _scoreManager.Score;
                var vs1 = itemTable.OrderByDescending((s) => s.Value).Select(s=>new ResultScore(s.Value,s.Key)).ToArray();
                _scoreAsyncSubject.OnNext(vs1);
                _scoreAsyncSubject.OnCompleted();
            });
        }      
    }
}