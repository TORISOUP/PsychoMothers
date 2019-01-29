using PsycthoMothers.Battle.Audio;
using PsycthoMothers.Battle.Manager;
using PsycthoMothers.Battle.View;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PsycthoMothers.Battle.Presenter
{
    /// <summary>
    /// FinishPresenter
    /// 終了時のプレゼンタークラス。ゲーム終了を知らせる
    /// </summary>
    public class FinishPresenter : MonoBehaviour
    {
        /// <summary>
        /// Text
        /// </summary>
        [SerializeField] private Image _finishImage;

        /// <summary>
        /// TimeManager
        /// </summary>
        [Inject] private TimeManager _timeManager;

        /// <summary>
        /// GameStateManager
        /// </summary>
        [Inject] private GameStateManager _gameStateManager;

        [SerializeField] private ElementShaker _shaker;

        [Inject] private AudioManager _audioManager;

        private bool isSEPlay;

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            _finishImage.enabled = false;

            _gameStateManager.CurrentState
                .FirstOrDefault(x => x == GameState.Finished)
                .Subscribe(_ =>
                {
                    _finishImage.enabled = true;
                    _shaker.ShakePosition(3, 60);
                    _shaker.ShakeRotation(3, 60);
                });

            _timeManager.FinishTimer
                .Where(x => _gameStateManager.CurrentState.Value == GameState.Finished)
                .Subscribe(x =>
                {
                    if (!isSEPlay)
                    {
                        _audioManager.PlaySE(SfxType.EndGame_BOUNCE);
                        isSEPlay = true;
                    }
                });

            _gameStateManager.CurrentState
                .Where(x => _gameStateManager.CurrentState.Value == GameState.Result)
                .Subscribe(x => { _finishImage.enabled = false; });
        }
    }
}
