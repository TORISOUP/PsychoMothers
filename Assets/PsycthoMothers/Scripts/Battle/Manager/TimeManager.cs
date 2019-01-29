using System.Collections;
using UniRx;
using UnityEngine;

namespace PsycthoMothers.Battle.Manager
{
    /// <summary>
    /// ゲーム中の時間を管理する
    /// </summary>
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private IntReactiveProperty _readyTimer = new IntReactiveProperty(3);
        [SerializeField] private IntReactiveProperty _mainTimer = new IntReactiveProperty(120);
        [SerializeField] private IntReactiveProperty _finishTimer = new IntReactiveProperty(3);
        [SerializeField] private IntReactiveProperty _resultTimer = new IntReactiveProperty(4);


        public IReadOnlyReactiveProperty<int> ReadyTimer => _readyTimer;
        public IReadOnlyReactiveProperty<int> MainTimer => _mainTimer;
        public IReadOnlyReactiveProperty<int> FinishTimer => _finishTimer;
        public IReadOnlyReactiveProperty<int> ResultTimer => _resultTimer;

        public void CountDownStart()
        {
            StartCoroutine(CountCoroutine());
        }

        IEnumerator CountCoroutine()
        {
            _readyTimer.SetValueAndForceNotify(_readyTimer.Value);
            yield return new WaitForSecondsRealtime(1);

            while (_readyTimer.Value >= 0)
            {
                _readyTimer.SetValueAndForceNotify(_readyTimer.Value - 1);
                yield return new WaitForSecondsRealtime(1);
            }

            while (_mainTimer.Value > 0)
            {
                _mainTimer.Value--;
                yield return new WaitForSecondsRealtime(1);
            }

            while (_finishTimer.Value > 0)
            {
                _finishTimer.Value--;
                yield return new WaitForSecondsRealtime(1);
            }

            while (_resultTimer.Value > 0)
            {
                _resultTimer.Value--;
                yield return new WaitForSecondsRealtime(1);
            }
        }
    }
}
