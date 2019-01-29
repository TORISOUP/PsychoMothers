using PsycthoMothers.Battle.Audio;
using UnityEngine;
using UniRx;
using Zenject;
using PsycthoMothers.Battle.Inputs;
using PsycthoMothers.Battle.Manager;


namespace PsycthoMothers.Battle.Players
{
    /// <summary>
    /// ItemAudio
    /// </summary>
    public class ItemAudio : MonoBehaviour
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
            var itemHolder = GetComponent<ItemHolder>();
            var input = GetComponent<IInputEventProvider>();
            
            itemHolder.IsItemHold
                .Subscribe(x =>
                {
                    
                    // アイテムを持った時にSE再生
                    if (x)
                    {
                        _audioManager.PlaySE(SfxType.ItemPick_BOUNCE);
                    }
                });
            
            
            // アイテムを使用した時にSE再生
            input.OnUseButtonPushed
                .Where(_ => itemHolder.IsItemHold.Value)
                .Subscribe(_ =>
                {
                    
                    _audioManager.PlaySE(SfxType.ItemPlace_BOUNCE);
                });            
        }
        
    }
}


