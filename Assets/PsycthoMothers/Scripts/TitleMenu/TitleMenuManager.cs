using PsycthoMothers.Common;
using UniRx.Async;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace PsycthoMothers.TitleMenu
{
    public class TitleMenuManager : MonoBehaviour
    {
        public void MoveToBattleScene(int players)
        {
            var battleMenuInfo = new BattleMenuInfo(playerCount: players);
            SceneLoader.LoadScene("Battle", container =>
            {
                container.Bind<BattleMenuInfo>()
                    .FromInstance(battleMenuInfo).AsCached();
            });
        }
    }
}
