using PsycthoMothers.Battle.Players;
using PsycthoMothers.Common;
using UniRx;
using UnityEngine;
using Zenject;

namespace PsycthoMothers.Battle.Manager
{
    public class PlayerSpawner : MonoBehaviour
    {
        private ReactiveCollection<PlayerCore> _players = new ReactiveCollection<PlayerCore>();
        public IReadOnlyReactiveCollection<PlayerCore> Players => _players;

        [SerializeField] private GameObject _playerPrefab;

        [Inject] private BattleMenuInfo _info;

        [SerializeField] private Transform[] _startPositions;

        void Start()
        {
            for (int i = 0; i < _info.PlayerCount; i++)
            {
                var go = Instantiate(_playerPrefab, _startPositions[i].position, Quaternion.identity);
                var core = go.GetComponent<PlayerCore>();
                core.Initialize((PlayerId) i);
                _players.Add(core);
            }
        }
    }
}
