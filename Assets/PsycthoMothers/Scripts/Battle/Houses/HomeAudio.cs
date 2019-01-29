using PsycthoMothers.Battle.Audio;
using PsycthoMothers.Battle.Kids;
using PsycthoMothers.Battle.Manager;
using PsycthoMothers.Battle.Players;
using UniRx;
using UniRx.Triggers;
using Zenject;
using UnityEngine;

namespace PsycthoMothers.Battle.Houses
{
    public class HomeAudio: MonoBehaviour
    {
        [Inject] private AudioManager _audioManager;
        
        
        
        protected void Start()
        {
            var h = GetComponent<House>();

            h.Score.Subscribe(x =>
            {
                if (x == 0) return;
                
                switch (Random.Range(0, 14))
                {
                    case 0:
                        _audioManager.PlaySE(SfxType.Baby01_BOUNCE);
                        break;
                    case 1:
                        _audioManager.PlaySE(SfxType.Baby02_BOUNCE);
                        break;
                    case 2:
                        _audioManager.PlaySE(SfxType.Baby03_BOUNCE);
                        break;
                    case 3:
                        _audioManager.PlaySE(SfxType.Baby04_BOUNCE);
                        break;
                    case 4:
                        _audioManager.PlaySE(SfxType.Baby05_BOUNCE);
                        break;
                    case 5:
                        _audioManager.PlaySE(SfxType.Baby06_BOUNCE);
                        break;
                    case 6:
                        _audioManager.PlaySE(SfxType.Baby07_BOUNCE);
                        break;
                    case 7:
                        _audioManager.PlaySE(SfxType.Baby08_BOUNCE);
                        break;
                    case 8:
                        _audioManager.PlaySE(SfxType.Baby09_BOUNCE);
                        break;
                    case 9:
                        _audioManager.PlaySE(SfxType.Baby10_BOUNCE);
                        break;
                    case 10:
                        _audioManager.PlaySE(SfxType.Baby11_BOUNCE);
                        break;
                    case 11:
                        _audioManager.PlaySE(SfxType.Baby12_BOUNCE);
                        break;
                    case 12:
                        _audioManager.PlaySE(SfxType.Baby13_BOUNCE);
                        break;
                    case 13:
                        _audioManager.PlaySE(SfxType.Baby14_BOUNCE);
                        break;
                    case 14:
                        _audioManager.PlaySE(SfxType.Baby15_BOUNCE);
                        break;
                    
                }
            });
        }
    }
}