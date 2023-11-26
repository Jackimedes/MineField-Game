using MineField.Enums;

namespace MineField.Records
{
    /// <summary>
    /// Represents a summary of a completed game.
    /// </summary>
    public sealed record GameSummary(int NumberOfMoves, int LivesRemaining, Difficulty GameDifficulty, GameResult GameResult)
    {
        /// <summary>
        /// Creates a GameSummary for a lost game.
        /// </summary>
        /// <param name="numberOfMoves">The number of moves taken in the game.</param>
        /// <param name="gameBoard">The current Game Board.</param>
        /// <returns>A GameSummary representing a lost game.</returns>
        public static GameSummary Lose(int numberOfMoves, GameBoard gameBoard)
            => new GameSummary(numberOfMoves, gameBoard.PlayerLives, gameBoard.GameDifficulty, GameResult.Lose);

        /// <summary>
        /// Creates a GameSummary for a won game.
        /// </summary>
        /// <param name="numberOfMoves">The number of moves taken in the game.</param>
        /// <param name="gameBoard">The current Game Board.</param>
        /// <returns>A GameSummary representing a won game.</returns>
        public static GameSummary Win(int numberOfMoves, GameBoard gameBoard)
            => new GameSummary(numberOfMoves, gameBoard.PlayerLives, gameBoard.GameDifficulty, GameResult.Win);
    }
}
