using System.Text;
using MineField.Enums;
using MineField.Exceptions;
using MineField.Interfaces;
using MineField.Records;

namespace MineField
{
    /// <summary>
    /// Game Manager for console visualisation
    /// </summary>
    public sealed class ConsoleGameManager : IGameManager
    {
        private const string _PlayAgainMessage = "Press (Y) to play again";
        private const string _RequestValidPlayerInputMessage = "Enter U D L or R for your next move.";

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ConsoleGameManager()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            //TODO: Open this up to player selection
            NewGame();
        }

        private List<GameState> _games { get; set; } = new List<GameState>();
        public IReadOnlyList<GameState> Games => _games;

        public GameState CurrentGame { get; private set; }

        public GameState NewGame(int maxColumn = 8, int maxRow = 8, int playerLives = 2, Difficulty gameDifficulty = Difficulty.Normal)
        {
            CurrentGame = new GameState(maxColumn, maxRow, playerLives, gameDifficulty);
            _games.Add(CurrentGame);
            return CurrentGame;
        }

        public string GameStart()
        {
            string consoleDisplay = $"Reach the top of the board without losing all your lives!{Environment.NewLine}";
            consoleDisplay += _RequestValidPlayerInputMessage.DoubleSpace();
            consoleDisplay += DrawCurrentGameBoard();
            return consoleDisplay;
        }

        public string AddPlayerInput(string playerInput)
        {
            // Handle incorrect input on complete game
            if(CurrentGame.GameSummary is not null)
            {
                string consoleOutput = "Invalid input".DoubleSpace();
                consoleOutput += EndGame();
                return consoleOutput;
            }

            try
            {
                if(!PlayerMove.TryFromValue(playerInput, out PlayerMove playerMove))
                    throw new ArgumentOutOfRangeException(nameof(playerInput), "Input could not be translated into a valid move. Please enter either U, D, L, or R");

                MoveResult moveResult = CurrentGame.Move(playerMove);

                string consoleDisplay = moveResult.GetBasicConsoleOutput();
                bool gameEnd = moveResult.GameResult.HasValue;

                if( !gameEnd )
                {
                    consoleDisplay += $" Game state: Playing.{Environment.NewLine}";
                    consoleDisplay += _RequestValidPlayerInputMessage.DoubleSpace();
                }
                else
                {
                    consoleDisplay += $" Game state: {moveResult.GameResult}.{Environment.NewLine}";
                }

                consoleDisplay += DrawCurrentGameBoard().DoubleSpace();

                if(moveResult.GameResult is not null)
                {
                    consoleDisplay += EndGame();
                }

                return consoleDisplay;
            }
            catch( Exception cex ) when( cex is InvalidMoveException || cex is ArgumentOutOfRangeException )
            {
                return $"{cex.Message}{Environment.NewLine}{GameStart()}";
            }
            catch( Exception ex )
            {
                return $"{ex.Message}{Environment.NewLine}";
            }
        }

        private string EndGame()
        {
            GameStats gameStats = ProduceGameStats();
            string endgameOutput = gameStats.GetStatsForConsole().DoubleSpace();
            endgameOutput += _PlayAgainMessage.DoubleSpace();
            return endgameOutput;
        }

        private GameStats ProduceGameStats()
        {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            List<GameSummary> CompletedGames = Games
            .Where(q => q.GameSummary is not null)
            .Select(s => s.GameSummary)
            .ToList();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            List<GameSummary> WinningGames = CompletedGames
                    .Where(summary => summary.GameResult == GameResult.Win)
                    .ToList();
            List<GameSummary> LosingGames = CompletedGames
                    .Where(summary => summary.GameResult == GameResult.Lose)
                    .ToList();
            int LowestMoveWin = WinningGames.Any() ? WinningGames.Min(game => game.NumberOfMoves) : 0;

            int curCount = 0;
            int maxCount = 0;
            for (int i = 0; i < CompletedGames.Count; i++ )
            {
                GameSummary game = CompletedGames[i];
                if(game.GameResult == GameResult.Win)
                {
                    curCount++;
                }
                else
                {
                    if(maxCount < curCount)
                        maxCount = curCount;

                    curCount = 0;
                }

                if(i == CompletedGames.Count - 1)
                    maxCount = curCount;
            }

            return new GameStats(CompletedGames.Count, WinningGames.Count, LosingGames.Count, LowestMoveWin, maxCount);
        }

        #region Console Drawing

        private string DrawCurrentGameBoard()
        {
            StringBuilder sb = new StringBuilder();

            for(int row = CurrentGame.GameBoard.MaxRow; row > 0; row--)
            {
                for(int col = 1; col <= CurrentGame.GameBoard.MaxColumn; col++)
                {
                    BoardPosition boardPosition = new BoardPosition(col, row);
                    bool isHitMinePosition = CurrentGame.GameBoard.HitMinePositions.Contains(boardPosition);
                    bool isPlayerPosition = CurrentGame.GameBoard.CurrentBoardPosition == boardPosition;

                    string boardSquare = GetBoardSquare(isPlayerPosition, isHitMinePosition);

                    sb.Append(boardSquare);

                    if(col == CurrentGame.GameBoard.MaxColumn)
                        sb.Append(Environment.NewLine + Environment.NewLine);
                }
            }

            return sb.ToString();
        }

        private string GetBoardSquare( bool isPlayerPosition, bool isHitMinePosition )
        {
            if( isPlayerPosition )
            {
                return isHitMinePosition ? "(X) " : "(-) ";
            }
            else
            {
                return isHitMinePosition ? "-X- " : "--- ";
            }
        }

        #endregion
    }

    public static class ConsoleStringExtensions
    {
        public static string DoubleSpace(this string str)
            => str + Environment.NewLine + Environment.NewLine;
    }
}
