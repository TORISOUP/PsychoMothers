using PsycthoMothers.Battle.Players;

namespace PsycthoMothers.Battle.Manager
{
    /// <summary>
    /// ResultScore
    /// </summary>
    public struct ResultScore
    {
        public int Score { get; }
        public PlayerId PlayerId { get; }

        public ResultScore(int score, PlayerId playerId)
        {
            Score = score;
            PlayerId = playerId;
        }      
    }
}