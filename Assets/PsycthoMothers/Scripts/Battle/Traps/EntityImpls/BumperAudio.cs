using PsycthoMothers.Battle.Audio;
using UnityEngine;
using UniRx;
using Zenject;
using PsycthoMothers.Battle.Manager;

namespace PsycthoMothers.Battle.Traps.EntityImpls
{
    
    public class BumperAudio : MonoBehaviour
    {
        /// <summary>
        /// AudioManager
        /// </summary>
        [Inject] private AudioManager _audioManager;

        /// <summary>
        /// Start
        /// </summary>
        protected void Start()
        {
            var bump = GetComponent<BumperTrapEntity>();
            // 開始時に音再生
            bump.IsBump
                .Subscribe(x =>
                {  
                    if (x)
                    {
                        _audioManager.PlaySE(SfxType.Bumber_BOUNCE);
                    }
                }); 
        }
    }
}