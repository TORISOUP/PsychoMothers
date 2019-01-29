using PsycthoMothers.Common;
using UniRx;
using UnityEngine;
using Zenject;

namespace PsycthoMothers.Battle.Manager
{
    public class FieldManager : MonoBehaviour
    {
        [SerializeField] private GameObject _gard1;
        [SerializeField] private GameObject _gard2;
        [SerializeField] private GameObject _gard3;
        [SerializeField] private GameObject _gard4;

        [Inject] private BattleMenuInfo _info;

        [Inject] private GameStateManager _stateManager;

        void Start()
        {
            _gard1.SetActive(false);
            _gard2.SetActive(false);

            switch (_info.PlayerCount)
            {
                case 2:
                    _gard3.SetActive(true);
                    _gard4.SetActive(true);
                    break;
                case 3:
                    _gard3.SetActive(false);
                    _gard4.SetActive(true);
                    break;
                case 4:
                    _gard3.SetActive(false);
                    _gard4.SetActive(false);
                    break;
            }

            _stateManager.CurrentState
                .FirstOrDefault(x => x == GameState.Finished)
                .Subscribe(_ =>
                {
                    _gard1.SetActive(true);
                    _gard2.SetActive(true);
                    _gard3.SetActive(true);
                    _gard4.SetActive(true);
                });
        }

    }
}
