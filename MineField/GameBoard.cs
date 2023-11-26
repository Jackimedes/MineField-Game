using MineField.Enums;
using MineField.Records;

namespace MineField
{
    /// <summary>
    /// Represents the game board
    /// </summary>
    public sealed class GameBoard
    {
        private readonly Random _random = new Random();
        private const int _maxMineSearchDepth = 1000;

        public GameBoard(int maxColumn, int maxRow, int playerLives, Difficulty difficulty)
        {
            if( maxColumn < 3 )
                throw new ArgumentOutOfRangeException(nameof(maxColumn), "Board must have at least 3 columns");
            if (maxRow < 3 )
                throw new ArgumentOutOfRangeException(nameof(maxRow), "Board must have at least 3 rows");

            MaxColumn = maxColumn;
            MaxRow = maxRow;
            PlayerLives = playerLives;
            GameDifficulty = difficulty;

            PlaceMines();
        }

        public int MaxColumn { get; private set; }
        public int MaxRow { get; private set; }
        public Difficulty GameDifficulty { get; private set; }
        public int PlayerLives { get; private set; }

        public BoardPosition CurrentBoardPosition { get; private set; } = new BoardPosition( 1, 1 );

        private List<BoardPosition> _minePositions { get; set; } = new ();
        public IReadOnlyCollection<BoardPosition> MinePositions => _minePositions;

        private List<BoardPosition> _hitMinePositions { get; set; } = new();
        public IReadOnlyCollection<BoardPosition> HitMinePositions => _hitMinePositions;

        /// <summary>
        /// Update the position of the player on the board
        /// </summary>
        /// <param name="boardPosition"></param>
        /// <returns></returns>
        public (bool MineHit, bool RemainingLives) UpdatePlayerPosition(BoardPosition boardPosition)
        {
            CurrentBoardPosition = boardPosition;

            return MineHit(boardPosition);
        }

        /// <summary>
        /// Is a mine hit at a given board position
        /// </summary>
        /// <param name="boardPosition"></param>
        /// <returns></returns>
        private (bool MineHit, bool RemainingLives) MineHit(BoardPosition boardPosition)
        {
            // True if there is a mine at the position that hasn't already been hit
            bool mineHit = MinePositions.Contains(boardPosition) && !HitMinePositions.Contains(boardPosition);
            
            if( mineHit )
            {
                // Track the hit and reduce the player's lives
                _hitMinePositions.Add(boardPosition);
                bool remainingLives = ReducePlayerLives();
                return (mineHit, remainingLives);
            }

            return (mineHit, true);
        }

        /// <summary>
        /// Reduce the amount of remaining lives for the player
        /// </summary>
        /// <returns>True if the player has lives remaining, otherwise false</returns>
        private bool ReducePlayerLives()
        {
            PlayerLives--;

            return PlayerLives > 0;
        }

        /// <summary>
        /// Place mines on the game board
        /// </summary>
        private void PlaceMines()
        {
            // Establish protected positions for fairness
            List<BoardPosition> protectedBoardPositions = GetDefaultProtectedBoardPositions( MaxRow, MaxColumn );

            int totalAvailablePositions = (MaxColumn * MaxRow) - protectedBoardPositions.Count;
            
            // Either the number of lives the player has (so the game can be lost) or based on game difficulty and board size
            int maxMines = Convert.ToInt32(Math.Round((decimal)totalAvailablePositions * ((int)GameDifficulty / 100), 0, MidpointRounding.AwayFromZero));
            maxMines = Math.Max(PlayerLives, maxMines);

            // Get a random number of mines, limited by the 
            int numberOfMines = _random.Next(maxMines, maxMines + 1);

            // Seed the board with mines
            for(int i = 0; i < numberOfMines; i++)
            {
                // Get a position for the mine, add it to the tracked mine positions
                // Also add it to the restricted list to prevent duplication
                BoardPosition minePosition = GetAllowedMinePosition(protectedBoardPositions);
                _minePositions.Add(minePosition);
                protectedBoardPositions.Add(minePosition);
            }
        }

        private List<BoardPosition> GetDefaultProtectedBoardPositions(int maxRow, int maxColumn)
        {
            // Starting position, and next 2 possible moves should be protected for fairness
            List<BoardPosition> protectedPositions = new List<BoardPosition>()
            {
                new BoardPosition(1,1),
                new BoardPosition(1,2),
                new BoardPosition(2,1)
            };

            // There also should be at least one safe position at the top of the board to end on
            int safeColumn = _random.Next(1, maxColumn + 1);
            protectedPositions.Add(new BoardPosition(maxRow, safeColumn));

            return protectedPositions;
        }

        private BoardPosition GetAllowedMinePosition(List<BoardPosition> restrictedBoardPositions, int depth = 0)
        {
            // We've dug too deep, get out!
            if(depth > _maxMineSearchDepth)
                throw new InvalidOperationException("Unable to find suitable mine location");

            depth++;

            int mineRow = _random.Next(1, MaxRow + 1);
            int mineColumn = _random.Next(1, MaxColumn + 1);

            BoardPosition minePosition = new BoardPosition(mineRow, mineColumn);
            if(!restrictedBoardPositions.Contains( minePosition))
            {
                return minePosition;
            }
            else
            {
                return GetAllowedMinePosition(restrictedBoardPositions, depth);
            }
        }
    }
}