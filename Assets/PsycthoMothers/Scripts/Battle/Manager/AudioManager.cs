using System.Collections.Generic;
using PsycthoMothers.Battle.Audio;
using UniRx;
using UnityEngine;

namespace PsycthoMothers.Battle.Manager
{
    /// <summary>
    /// オーディオマネージャー
    ///
    /// 以下から流用、一部UniRX対応
    /// http://kan-kikuchi.hatenablog.com/entry/AudioManager
    /// 
    /// Author:kan_kikuchi
    /// Author:Yuki Ogino
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        //ボリューム保存用のkeyとデフォルト値
        private const string BGM_VOLUME_KEY = "BGM_VOLUME_KEY";
        private const string SE_VOLUME_KEY = "SE_VOLUME_KEY";
        private const float BGM_VOLUME_DEFULT = 1.0f;
        private const float SE_VOLUME_DEFULT = 1.0f;

        //BGMがフェードするのにかかる時間
        public const float BGM_FADE_SPEED_RATE_HIGH = 0.9f;
        public const float BGM_FADE_SPEED_RATE_LOW = 0.3f;
        private float _bgmFadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH;

        //次流すBGM名、SE名
        private string _nextBGMName;
        private string _nextSEName;

        //BGMをフェードアウト中か
        public ReactiveProperty<bool> IsFadeOut = new ReactiveProperty<bool>();

        //BGM用、SE用に分けてオーディオソースを持つ
        public AudioSource AttachBGMSource, AttachSESource;

        //全Audioを保持
        private Dictionary<string, AudioClip> _bgmDic, _seDic;

        //=================================================================================
        //初期化
        //=================================================================================

        private void Awake()
        {

            //リソースフォルダから全SE&BGMのファイルを読み込みセット
            _bgmDic = new Dictionary<string, AudioClip>();
            _seDic = new Dictionary<string, AudioClip>();

            object[] bgmList = UnityEngine.Resources.LoadAll("Audio/BGM");
            object[] seList = UnityEngine.Resources.LoadAll("Audio/SE");

            foreach (AudioClip bgm in bgmList) {
            	_bgmDic [bgm.name] = bgm;
            }
            
            foreach (AudioClip se in seList)
            {
                _seDic[se.name] = se;
            }
        }

        private void Start()
        {
            AttachBGMSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
            AttachSESource.volume = PlayerPrefs.GetFloat(SE_VOLUME_KEY, SE_VOLUME_DEFULT);

            IsFadeOut.SetValueAndForceNotify(false);

            // アイテムを使用した時にSE再生
            IsFadeOut
                .Subscribe(x =>
                {
                    if (!x)
                    {
                        return;
                    }

                    //徐々にボリュームを下げていき、ボリュームが0になったらボリュームを戻し次の曲を流す
                    AttachBGMSource.volume -= Time.deltaTime * _bgmFadeSpeedRate;
                    if (AttachBGMSource.volume <= 0)
                    {
                        AttachBGMSource.Stop();
                        AttachBGMSource.volume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, BGM_VOLUME_DEFULT);
                        IsFadeOut.SetValueAndForceNotify(false);

                        if (!string.IsNullOrEmpty(_nextBGMName))
                        {
                            PlayBGM(_nextBGMName);
                        }
                    }
                });
        }

        //=================================================================================
        //SE
        //=================================================================================

        /// <summary>
        /// 指定したファイル名のSEを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
        /// </summary>
        public void PlaySE(string seName, float delay = 0.0f)
        {
            if (!_seDic.ContainsKey(seName))
            {
                Debug.Log(seName + "という名前のSEがありません");
                return;
            }

            _nextSEName = seName;
            Invoke("DelayPlaySE", delay);
        }

        public void PlaySE(SfxType audio, float delay = 0.0f)
        {
            PlaySE(audio.ToString(), delay);
        }


        private void DelayPlaySE()
        {
            AttachSESource.PlayOneShot(_seDic[_nextSEName] as AudioClip);
        }

        //=================================================================================
        //BGM
        //=================================================================================

        /// <summary>
        /// 指定したファイル名のBGMを流す。ただし既に流れている場合は前の曲をフェードアウトさせてから。
        /// 第二引数のfadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
        /// </summary>
        public void PlayBGM(string bgmName, float fadeSpeedRate = BGM_FADE_SPEED_RATE_HIGH)
        {
            if (!_bgmDic.ContainsKey(bgmName))
            {
                Debug.Log(bgmName + "という名前のBGMがありません");
                return;
            }

            //現在BGMが流れていない時はそのまま流す
            if (!AttachBGMSource.isPlaying)
            {
                _nextBGMName = "";
                AttachBGMSource.clip = _bgmDic[bgmName] as AudioClip;
                AttachBGMSource.Play();
            }
            //違うBGMが流れている時は、流れているBGMをフェードアウトさせてから次を流す。同じBGMが流れている時はスルー
            else if (AttachBGMSource.clip.name != bgmName)
            {
                _nextBGMName = bgmName;
                FadeOutBGM(fadeSpeedRate);
            }
        }

        public void PlayBGM(BfxType audio, float fadeSpeedRate = 0.0f)
        {
            PlayBGM(audio.ToString(), fadeSpeedRate);
        }
        
        /// <summary>
        /// 現在流れている曲をフェードアウトさせる
        /// fadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
        /// </summary>
        public void FadeOutBGM(float fadeSpeedRate = BGM_FADE_SPEED_RATE_LOW)
        {
            _bgmFadeSpeedRate = fadeSpeedRate;
            IsFadeOut.SetValueAndForceNotify(true);
        }

        //=================================================================================
        //音量変更
        //=================================================================================

        /// <summary>
        /// BGMとSEのボリュームを別々に変更&保存
        /// </summary>
        public void ChangeVolume(float BGMVolume, float SEVolume)
        {
            AttachBGMSource.volume = BGMVolume;
            AttachSESource.volume = SEVolume;

            PlayerPrefs.SetFloat(BGM_VOLUME_KEY, BGMVolume);
            PlayerPrefs.SetFloat(SE_VOLUME_KEY, SEVolume);
        }
    }
}
