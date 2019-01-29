using System;
using System.Threading;
using UniRx;
using UniRx.Async;
using UniRx.Async.Triggers;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace PsycthoMothers.Battle.Manager
{
    class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] _prefabs;

        [SerializeField] private Transform _root;

        [SerializeField] private Vector3 _leftTop;
        [SerializeField] private Vector3 _rightBottom;

        [Inject] private GameStateManager _stateManager;

        [SerializeField] private int _maxItemCount = 3;

        [Inject] private TimeManager _timeManager;

        private int _itemSpawnRate = 10;

        void Start()
        {
            _itemSpawnRate = Random.Range(10, 40);

            _timeManager.MainTimer.Where(x => x < 30)
                .Subscribe(_ => _itemSpawnRate = 80).AddTo(this);

            _stateManager.CurrentState.FirstOrDefault(x => x == GameState.Battle)
                .Subscribe(_ => Generator(this.GetCancellationTokenOnDestroy()).Forget());
        }

        async UniTaskVoid Generator(CancellationToken token)
        {

            while (!token.IsCancellationRequested && _stateManager.CurrentState.Value == GameState.Battle)
            {
                if (Random.Range(0, 100) <= _itemSpawnRate && _root.transform.childCount < 3)
                {
                    var prefab = _prefabs[Random.Range(0, _prefabs.Length)];

                    var pX = Mathf.Lerp(_leftTop.x, _rightBottom.x, Random.Range(0f, 1f));
                    var pY = Mathf.Lerp(_leftTop.y, _rightBottom.y, Random.Range(0f, 1f));
                    var pos = new Vector3(pX, pY, 0);

                    Instantiate(prefab, pos, Quaternion.identity, _root);
                }

                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);
            }
        }
    }
}
