using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PsycthoMothers.TitleMenu
{
    public class TitleMenuViewManager : MonoBehaviour
    {
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private GameObject _default;

        private void Start()
        {
            _eventSystem.ObserveEveryValueChanged(x => x.currentSelectedGameObject)
                .Where(x => x == null)
                .Subscribe(_ => { _eventSystem.SetSelectedGameObject(_default); }).AddTo(this);
        }
    }
}
