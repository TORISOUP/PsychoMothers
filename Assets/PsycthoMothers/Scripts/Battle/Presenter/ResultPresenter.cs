using System.Text;
using PsycthoMothers.Battle.Audio;
using PsycthoMothers.Battle.Manager;
using PsycthoMothers.Battle.Players;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PsycthoMothers.Battle.Presenter
{
    /// <summary>
    /// ResultPresenter
    /// リザルトの描画クラス
    /// </summary>
    public class ResultPresenter : MonoBehaviour
    {
        /// <summary>
        /// ResultManager
        /// </summary>
        [Inject] private ResultManager _resultManager;

        /// <summary>
        /// AudioManager
        /// </summary>
        [Inject] private AudioManager _audioManager;

        /// <summary>
        /// Text
        /// </summary>
        [SerializeField] private Image _winnerImage;

        [SerializeField] private Sprite _draw;
        [SerializeField] private Sprite player1Win;
        [SerializeField] private Sprite player2Win;
        [SerializeField] private Sprite player3Win;
        [SerializeField] private Sprite player4Win;

        [SerializeField] private Text _resultText;

        /// <summary>
        /// Start
        /// </summary>
        private void Start()
        {
            _winnerImage.enabled = false;

            _resultManager.Score
                .Subscribe(x =>
                {
                    if (x[0].Score != x[1].Score)
                    {
                        switch (x[0].PlayerId)
                        {
                            case PlayerId.Player1:
                                _winnerImage.sprite = player1Win;
                                break;
                            case PlayerId.Player2:
                                _winnerImage.sprite = player2Win;
                                break;
                            case PlayerId.Player3:
                                _winnerImage.sprite = player3Win;
                                break;
                            case PlayerId.Player4:
                                _winnerImage.sprite = player4Win;
                                break;
                        }
                    }
                    else
                    {
                        // 1位が０の場合はドロー
                        _winnerImage.sprite = _draw;
                    }

                    var stringBuilder = new StringBuilder();
                    foreach (var resultScore in x)
                    {
                        stringBuilder.Append($"{resultScore.PlayerId.ToString()}: {resultScore.Score}\n");
                    }

                    _resultText.text = stringBuilder.ToString();
                    _winnerImage.enabled = true;
                    _audioManager.PlaySE(SfxType.Victory_BOUNCE);
                });
        }
    }
}
