using PsycthoMothers.Battle.Inputs;
using PsycthoMothers.Battle.Manager;
using PsycthoMothers.Battle.Traps;
using UniRx;
using UnityEngine;
using Zenject;

namespace PsycthoMothers.Battle.Players
{
    class ItemUser : MonoBehaviour
    {
        [Inject] private TrapGenerator _trapGenerator;
        private Vector3 _direction = Vector3.down;

        private void Start()
        {
            var itemHolder = GetComponent<ItemHolder>();
            var input = GetComponent<IInputEventProvider>();
            var core = GetComponent<PlayerCore>();

            // 向いている方向
            input.MoveDirection
                .Where(x => x.magnitude > 0.5f)
                .Subscribe(x => _direction = x);

            // アイテムを使う
            input.OnUseButtonPushed
                .Where(_ => itemHolder.IsItemHold.Value)
                .Subscribe(_ =>
                {
                    if (!itemHolder.TrapType.HasValue) return;
                    var type = itemHolder.TrapType.Value;
                    var lengh = type == TrapType.SandBox ? 2f : 0.5f;
                    _trapGenerator.CreateTrap(core.PlayerId, type, transform.position + _direction * lengh);
                    itemHolder.IsItemHold.Value = false;
                });
        }
    }
}
