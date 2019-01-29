using PsycthoMothers.Battle.Audio;
using UnityEngine;
using UniRx;
using Zenject;
using PsycthoMothers.Battle.Manager;

namespace PsycthoMothers.Battle.Traps.EntityImpls
{
    /// <summary>
    /// BlackHoleAudio
    /// </summary>
    public class BlackHoleAudio : MonoBehaviour
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
            //var blackHole = GetComponent<BlackHoleTrapEntity>();
            _audioManager.PlaySE(SfxType.Blackhole_BOUNCE);
        }

    }
}