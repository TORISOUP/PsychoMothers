using System;
using System.Linq;
using System.Threading;
using PsycthoMothers.Battle.Kids;
using UniRx;
using UniRx.Async;
using UniRx.Async.Triggers;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace PsycthoMothers.Battle.Manager
{
    public class KidsSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] _normalKids;

        [SerializeField] private GameObject[] _goldenKids;

        [Inject] private GameStateManager _gameStateManager;

        [Inject] private PlayerSpawner _playerSpawner;

        [SerializeField] private Transform _rightUpEntrance;

        [SerializeField] private Transform _leftDownEntrance;

        [SerializeField] private Transform _kidsRoot;

        [SerializeField] private int _maxKidsCount = 15;

        [SerializeField] private Transform[] _cornerTransforms;

        private void Start()
        {
            _gameStateManager.CurrentState
                .FirstOrDefault(x => x == GameState.Battle)
                .Subscribe(_ => SpawnKids(this.GetCancellationTokenOnDestroy())
                    .Forget());
        }

        private async UniTaskVoid SpawnKids(CancellationToken token)
        {
            var awayTransfroms =
                _playerSpawner.Players.Select(x => x.transform)
                    .Concat(_cornerTransforms)
                    .ToArray();

            while (!token.IsCancellationRequested)
            {
                if (_kidsRoot.transform.childCount < _maxKidsCount
                    && Random.Range(0, 100) <= 50)
                {
                    var p = ChoiceKidsPrfab();

                    if (Random.Range(0, 100) <= 50)
                    {
                        var go = Instantiate(p, _rightUpEntrance.position, Quaternion.identity, _kidsRoot);
                        var k = go.GetComponent<KidsMover>();
                        go.GetComponent<KidsInputEventProvider>().AwayFromTransforms = awayTransfroms;
                        k.InitMove(new Vector3(-1, -1, 0)).Forget();
                    }
                    else
                    {
                        var go = Instantiate(p, _leftDownEntrance.position, Quaternion.identity, _kidsRoot);
                        var k = go.GetComponent<KidsMover>();
                        go.GetComponent<KidsInputEventProvider>().AwayFromTransforms = awayTransfroms;
                        k.InitMove(new Vector3(1, 1, 0)).Forget();
                    }
                }

                await UniTask.Delay(TimeSpan.FromMilliseconds(500),
                    cancellationToken: token);
            }
        }

        private GameObject ChoiceKidsPrfab()
        {
            if (Random.Range(0, 100) <= 5)
            {
                return _goldenKids[Random.Range(0, _normalKids.Length)];
            }
            else
            {
                return _normalKids[Random.Range(0, _normalKids.Length)];
            }
        }
    }
}
