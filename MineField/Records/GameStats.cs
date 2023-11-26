
namespace MineField.Records
{
    /// <summary>
    /// Represents the statistics of games.
    /// </summary>
    public record GameStats(int GamesCompleted, int GamesWon, int GamesLost, int LowestMovesToWin, int LongestWinStreak)
    {
        /// <summary>
        /// Gets a formatted string with game statistics suitable for console output.
        /// </summary>
        /// <returns>A string containing game statistics.</returns>
        public string GetStatsForConsole()
        {
            return $"You have played {GamesCompleted} games. " +
                   $"You have won {GamesWon} games and lost {GamesLost} games. " +
                   $"Your longest win streak is {LongestWinStreak} game(s)." +
                   ( LowestMovesToWin > 0 ? $" Your fastest win was in {LowestMovesToWin} moves." : "" );
        }
    }
}
