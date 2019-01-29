using UniRx;
using UnityEngine;

namespace PsycthoMothers.Battle.Inputs
{
    public interface IInputEventProvider
    {
        IReadOnlyReactiveProperty<Vector3> MoveDirection { get; }
        IReadOnlyReactiveProperty<bool> OnUseButtonPushed { get; }
    }
}
