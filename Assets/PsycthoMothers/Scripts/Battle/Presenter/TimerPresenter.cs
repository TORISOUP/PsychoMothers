using PsycthoMothers.Battle.Audio;
using PsycthoMothers.Battle.Manager;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;

namespace PsycthoMothers.Battle.Presenter
{
    class TimerPresenter : MonoBehaviour
    {
        [Inject] private TimeManager _timeManager;
        
        [Inject] private AudioManager _audioManager;

        [SerializeField] private Text _readyText;
        [SerializeField] private Text _mainText;


        private void Start()
        {
            _timeManager.ReadyTimer
                .Subscribe(x =>
                {
                    if (x < 0)
                    {
                        _readyText.text = "";
                        _mainText.text = _timeManager.MainTimer.Value.ToString();
                    }
                    else if (x == 0)
                    {
                        _readyText.text = "START!";
                        _audioManager.PlaySE(SfxType.StartGame_BOUNCE);
                        
                    }
                    else
                    {
                        if (x == 3)
                        {
                            switch (Random.Range(0, 2))
                            {
                                case 0:
                                    _audioManager.PlayBGM(BfxType.MusicFor2min);
                                    break;
                                case 1:
                                    _audioManager.PlayBGM(BfxType.CutePiano);
                                    break;
                            }

                            
                        }
                        _readyText.text = x.ToString();
                    }
                });


            _timeManager.MainTimer
                .Subscribe(x => { _mainText.text = x.ToString(); });

            _readyText.text = _timeManager.ReadyTimer.Value.ToString();
            _readyText.text = "";
            _mainText.text = "";
        }
    }
}
