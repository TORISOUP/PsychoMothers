using PsycthoMothers.Common;
using Zenject;

namespace PsycthoMothers.Resources.Installer
{
    public class BattleInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            if (!Container.HasBinding<BattleMenuInfo>())
            {
                Container.Bind<BattleMenuInfo>()
                    .FromInstance(new BattleMenuInfo(2))
                    .AsCached();
            }
        }
    }
}
