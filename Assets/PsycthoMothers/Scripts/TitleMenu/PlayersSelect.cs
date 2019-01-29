using System.Collections;
using System.Collections.Generic;
using PsycthoMothers.Battle.Audio;
using PsycthoMothers.Battle.Manager;
using PsycthoMothers.Common;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PsycthoMothers.TitleMenu
{
    public class PlayersSelect : MonoBehaviour
    {

        [Range(2,4)]
        public int PlayerNumber;
        [Inject]
        private TitleMenuManager titleMenuManager;

        [Inject]
        private AudioManager _audioManager;
        

        private void Start()
        {
            var button = GetComponent<Button>();
            button.OnClickAsObservable()
                .Subscribe(_ => OnClick());
        }

        private void OnClick()
        {
            _audioManager.PlaySE(SfxType.ButtonPress_BOUNCE);
            titleMenuManager.MoveToBattleScene(PlayerNumber);
        }

        public void Over()
        {
            _audioManager.PlaySE(SfxType.ButtonOver_BOUNCE);
        }
    }
}

