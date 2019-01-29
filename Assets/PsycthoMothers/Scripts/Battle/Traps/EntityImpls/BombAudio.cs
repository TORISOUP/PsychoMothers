using PsycthoMothers.Battle.Audio;
using UnityEngine;
using UniRx;
using Zenject;
using PsycthoMothers.Battle.Manager;

namespace PsycthoMothers.Battle.Traps.EntityImpls
{
    /// <summary>
    /// ボムのオーディオ関連再生クラス
    /// </summary>
    public class BombAudio : MonoBehaviour
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
            var bomb = GetComponent<BombTrapEntity>();
            
            // 開始時に音再生
            bomb.IsIgnite
                .Subscribe(x =>
                {
                   
                    if (x)
                    {
                        _audioManager.PlaySE(SfxType.BombIgnite_BOUNCE);
                    }
                });
            
            
            // 爆発した時に音再生
            bomb.IsExplosion
                .Subscribe(x =>
                {
                    
                    if (x)
                    {
                        _audioManager.PlaySE(SfxType.BombExplosion_BOUNCE);
                    }
                });            
        }
        
        
    }
}