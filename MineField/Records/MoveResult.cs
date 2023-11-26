using MineField.Enums;

namespace MineField.Records
{
    /// <summary>
    /// Represents the result of a player move.
    /// </summary>
    public sealed record MoveResult(int LandminesHit, int RemainingLives, BoardPosition NewBoardPosition, GameResult? GameResult = null)
    {
        /// <summary>
        /// Creates a MoveResult for a move resulting in a lost game.
        /// </summary>
        /// <param name="gameBoard">The current Game Board</param>
        /// <returns>A MoveResult representing a lost game.</returns>
        public static MoveResult Lose(GameBoard gameBoard)
            => new MoveResult(gameBoard.HitMinePositions.Count, gameBoard.PlayerLives, gameBoard.CurrentBoardPosition, Enums.GameResult.Lose);

        /// <summary>
        /// Creates a MoveResult for a move resulting in a won game.
        /// </summary>
        /// <param name="gameBoard">The current Game Board</param>
        /// <returns>A MoveResult representing a won game.</returns>
        public static MoveResult Win(GameBoard gameBoard)
            => new MoveResult(gameBoard.HitMinePositions.Count, gameBoard.PlayerLives, gameBoard.CurrentBoardPosition, Enums.GameResult.Win);

        /// <summary>
        /// Creates a MoveResult for a move resulting in no end game state.
        /// </summary>
        /// <param name="gameBoard"></param>
        /// <returns></returns>
        public static MoveResult Default(GameBoard gameBoard)
            => new MoveResult(gameBoard.HitMinePositions.Count, gameBoard.PlayerLives, gameBoard.CurrentBoardPosition);

        /// <summary>
        /// Gets a basic console output string representing the move result.
        /// </summary>
        /// <returns>A string containing basic move result information for console output.</returns>
        public string GetBasicConsoleOutput()
        {
            return $"New player position: {NewBoardPosition.Column},{NewBoardPosition.Row}." +
                   $" Landmines hit: {LandminesHit}. Lives remaining: {RemainingLives}. ";
        }
    }
}
