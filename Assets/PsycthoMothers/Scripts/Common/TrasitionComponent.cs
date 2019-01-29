using System;
using System.ComponentModel;
using System.Threading;
using UniRx;
using UniRx.Async;
using UniRx.Async.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace PsycthoMothers.Common
{
    public class TrasitionComponent : MonoBehaviour
    {
        [SerializeField] private Image _coverImage;
        [SerializeField] private float _transisionSeconds = 1;
        [Inject] private ZenjectSceneLoader _zenjectSceneLoader;

        private BoolReactiveProperty _isTransition = new BoolReactiveProperty(false);
        public IReadOnlyReactiveProperty<bool> IsTransition => _isTransition;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Transition(string nextScene, Action<DiContainer> bindAction)
        {
            _isTransition.Value = false;
            Transition(nextScene, bindAction, this.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTaskVoid Transition(
            string nextScene,
            Action<DiContainer> bindAction,
            CancellationToken token)
        {
            // 0 -> 1
            var startTime = Time.time;
            while ((Time.time - startTime) < _transisionSeconds)
            {
                var rate = ((Time.time - startTime)) / _transisionSeconds;
                _coverImage.color = _coverImage.color.SetA(rate);
                await UniTask.Yield();
            }

            _coverImage.color = _coverImage.color.SetA(1);

            await _zenjectSceneLoader.LoadSceneAsync(
                nextScene,
                LoadSceneMode.Single, bindAction);

            // 1 -> 0
            startTime = Time.time;
            while ((Time.time - startTime) < _transisionSeconds)
            {
                var rate = 1 - ((Time.time - startTime)) / _transisionSeconds;
                _coverImage.color = _coverImage.color.SetA(rate);
                await UniTask.Yield();
            }

            _coverImage.color = _coverImage.color.SetA(0);
            _isTransition.Value = true;
        }
    }
}
