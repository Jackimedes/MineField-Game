using MineField.Enums;
using MineField.Records;

namespace MineField
{
    /// <summary>
    /// Represents the state of a game.
    /// </summary>
    public sealed class GameState
    {
        public GameState(int maxColumn, int maxRow, int playerLives, Difficulty gameDifficulty)
        {
            GameBoard = new GameBoard(maxColumn, maxRow, playerLives, gameDifficulty);
        }

        public GameBoard GameBoard { get; private set; }

        private List<MoveResult> _gameMoveResults { get; set; } = new();
        public IReadOnlyCollection<MoveResult> GameMoveResults => _gameMoveResults;
        public GameSummary? GameSummary { get; private set; }

        /// <summary>
        /// Move the player on the game board in the selected direction.
        /// </summary>
        /// <param name="playerMove">The move to be executed.</param>
        /// <returns>The result of the player move.</returns>
        public MoveResult Move(PlayerMove playerMove)
        {
            (bool MineHit, bool RemainingLives) moveResult = GameBoard.UpdatePlayerPosition(playerMove.Move(GameBoard));

            if(moveResult.MineHit)
            {
                if(LoseConditionMet(moveResult.RemainingLives))
                {
                    return ProcessMoveResult(GameResult.Lose);
                }
            }

            if(WinConditionMet())
            {
                return ProcessMoveResult(GameResult.Win);
            }

            return ProcessMoveResult();
        }

        /// <summary>
        /// Determines whether the game has been won.
        /// </summary>
        /// <returns><c>true</c> if the game has been won; otherwise, <c>false</c>.</returns>
        private bool WinConditionMet()
        {
            return GameBoard.CurrentBoardPosition.Row == GameBoard.MaxRow;
        }

        /// <summary>
        /// Determines whether the game has been lost.
        /// </summary>
        /// <returns><c>true</c> if the game has been lost; otherwise, <c>false</c>.</returns>
        private bool LoseConditionMet(bool playerHasRemainingLives)
        {
            return !playerHasRemainingLives;
        }

        /// <summary>
        /// Processes the move result, updates game move results, and calculates the game summary (if required).
        /// </summary>
        /// <param name="gameResult">The result of the move (Win or Lose).</param>
        /// <returns>The move result.</returns>
        private MoveResult ProcessMoveResult(GameResult? gameResult = null)
        {
            MoveResult moveResult;
            if (gameResult is null)
            {
                moveResult = MoveResult.Default(GameBoard);
            }
            else
            {
                moveResult = gameResult == GameResult.Win 
                    ? MoveResult.Win(GameBoard) : MoveResult.Lose(GameBoard);
                GameSummary = gameResult == GameResult.Win 
                    ? GameSummary.Win(GameMoveResults.Count, GameBoard) 
                    : GameSummary.Lose(GameMoveResults.Count, GameBoard);
            }

            _gameMoveResults.Add( moveResult);
            return GameMoveResults.Last();
        }
    }
}
