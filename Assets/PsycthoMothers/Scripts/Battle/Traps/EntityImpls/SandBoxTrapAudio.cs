using PsycthoMothers.Battle.Audio;
using UnityEngine;
using UniRx;
using Zenject;
using PsycthoMothers.Battle.Manager;

namespace PsycthoMothers.Battle.Traps.EntityImpls
{
    public class SandBoxTrapAudio: MonoBehaviour
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
            var sand = GetComponent<SandBoxTrapEntity>();
            _audioManager.PlaySE(SfxType.Sandbox_BOUNCE);
            // 開始時に音再生
            sand.IsSand
                .Subscribe(x =>
                {
                   
                    if (x)
                    {
                        _audioManager.PlaySE(SfxType.Sandbox_BOUNCE);
                    }
                });
                      
        }
        
        
    }
}