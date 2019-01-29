using System.Collections;
using PsycthoMothers.Battle.Manager;
using PsycthoMothers.Battle.Players;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PsycthoMothers.Battle.Presenter
{
    class ScorePresenter : MonoBehaviour
    {
        [Inject] private ScoreManager _scoreManager;

        [Inject] private GameStateManager _gameStateManager;

        [SerializeField] private PlayerId _playerId;

        [SerializeField] private Text _text;

        private Vector3 _defaultPosition;

        private int _currentScore;
        private float _popUpTimer = -1;

        private async void Start()
        {
            await _scoreManager.Initialized;

            _defaultPosition = _text.rectTransform.localPosition;

            if (!_scoreManager.Score.ContainsKey(_playerId))
            {
                _text.text = "";
                return;
            }
            else
            {
                _text.text = "0";
            }

            _scoreManager.Score.ObserveReplace()
                .Where(x => x.Key == _playerId && _gameStateManager.CurrentState.Value == GameState.Battle)
                .Select(x => x.NewValue)
                .Subscribe(x =>
                {
                    _currentScore = x;
                    _popUpTimer = 1;
                    _text.text = _currentScore.ToString();
                    _text.rectTransform.localPosition = _defaultPosition;
                });

#if UNITY_EDITOR
            this.UpdateAsObservable()
                .Where(_ => Input.GetKeyDown(KeyCode.T))
                .Subscribe(_ =>
                {
                    _currentScore++;
                    _popUpTimer = 1;
                    _text.text = _currentScore.ToString();
                    _text.rectTransform.localPosition = _defaultPosition;
                });
#endif
            StartCoroutine(DrawCoroutine());
        }

        private IEnumerator DrawCoroutine()
        {
            while (true)
            {
                if (_popUpTimer >= 0)
                {
                    _popUpTimer -= Time.deltaTime;
                    _text.rectTransform.localPosition =
                        _text.rectTransform.localPosition + new Vector3(0, 50.0f * Time.deltaTime, 0);
                }
                else if (_popUpTimer <= 0)
                {
                    _text.text = "";
                }

                yield return null;
            }
        }
    }
}
