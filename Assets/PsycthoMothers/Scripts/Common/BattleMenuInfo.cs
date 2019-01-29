namespace PsycthoMothers.Common
{
    /// <summary>
    /// ゲームを開始するために必要な情報
    /// </summary>
    public struct BattleMenuInfo
    {
        public int PlayerCount { get; }

        public BattleMenuInfo(int playerCount)
        {
            PlayerCount = playerCount;
        }
    }
}
