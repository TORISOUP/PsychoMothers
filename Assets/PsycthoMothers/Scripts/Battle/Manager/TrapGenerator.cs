using System;
using PsycthoMothers.Battle.Players;
using PsycthoMothers.Battle.Traps;
using UnityEngine;

namespace PsycthoMothers.Battle.Manager
{
    public class TrapGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _bombPrefab;
        [SerializeField] private GameObject _bumperPrefab;
        [SerializeField] private GameObject _sandBoxPrefab;
        [SerializeField] private GameObject _blackHolePrefab;
        public void CreateTrap(PlayerId id, TrapType type, Vector3 position)
        {
            switch (type)
            {
                case TrapType.Bomb:
                {
                    var go = Instantiate(_bombPrefab, position, Quaternion.identity);
                    SetParams(go, id);
                }
                    break;
                case TrapType.Bumper:
                {
                    var go = Instantiate(_bumperPrefab, position, Quaternion.identity);
                    SetParams(go, id);
                }
                    break;
                case TrapType.BigArm:
                    break;
                case TrapType.SandBox:
                {
                    var go = Instantiate(_sandBoxPrefab, position, Quaternion.identity);
                    SetParams(go, id);
                }
                    break;
                case TrapType.BlackHole:
                {
                    var go = Instantiate(_blackHolePrefab, position, Quaternion.identity);
                    SetParams(go, id);
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void SetParams(GameObject target, PlayerId id)
        {
            var e = target.GetComponent<TrapEntity>();
            e.SetParams(id);
        }
    }
}
