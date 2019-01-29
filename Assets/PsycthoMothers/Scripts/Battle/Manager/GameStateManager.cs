using System;
using PsycthoMothers.Common;
using UniRx;
using UniRx.Async;
using UnityEngine;
using Zenject;

namespace PsycthoMothers.Battle.Manager
{
    /// <summary>
    /// ゲーム全体の進行管理を行う
    /// </summary>
    class GameStateManager : MonoBehaviour
    {
        [Inject] private TimeManager _timeManager;

        private ReactiveProperty<GameState> _gameState = new ReactiveProperty<GameState>(GameState.Initializing);
        public IReadOnlyReactiveProperty<GameState> CurrentState => _gameState;

        private void Start()
        {
            _timeManager.ReadyTimer
                .FirstOrDefault(x => x == 0)
                .Subscribe(_ => _gameState.SetValueAndForceNotify(GameState.Battle));

            _timeManager.MainTimer
                .FirstOrDefault(x => x == 0)
                .Subscribe(_ => _gameState.SetValueAndForceNotify(GameState.Finished));
            
            _timeManager.FinishTimer 
                .FirstOrDefault(x => x == 0)
                .Subscribe(_ => _gameState.SetValueAndForceNotify(GameState.Result));
 
            _timeManager.ResultTimer 
                .FirstOrDefault(x => x == 0)
                .Subscribe(_ => MoveToTitleScene());             

            InitializeAsync().Forget();
        }

        private async UniTaskVoid InitializeAsync()
        {
            _gameState.SetValueAndForceNotify(GameState.Initializing);

            // 画面が開くのをまつ
            await SceneLoader.OnTransitionFinished;
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

            _gameState.SetValueAndForceNotify(GameState.Ready);

            // カウントダウン開始
            _timeManager.CountDownStart();
        }
        
        /// <summary>
        /// タイトルへ戻る
        /// </summary>
        public void MoveToTitleScene()
        {
            SceneLoader.LoadScene("TitleMenu", null);
        }         
    }
}
