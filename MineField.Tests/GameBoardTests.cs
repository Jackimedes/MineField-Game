using FluentAssertions;
using MineField.Enums;
using MineField.Records;

namespace MineField.Tests
{
    public class GameBoardTests
    {
        [Fact]
        public void Constructor_ThrowsExceptionWhenColumnsLessThan3()
        {
            Assert.Throws<ArgumentOutOfRangeException>( () => new GameBoard(2, 4, 3, Difficulty.Normal));
        }

        [Fact]
        public void Constructor_ThrowsExceptionWhenRowsLessThan3()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new GameBoard(4, 2, 3, Difficulty.Normal));
        }

        [Fact]
        public void Constructor_InitializesProperties()
        {
            GameBoard gameBoard = new GameBoard(5, 5, 3, Difficulty.Normal);

            Assert.Equal(5, gameBoard.MaxColumn);
            Assert.Equal(5, gameBoard.MaxRow);
            Assert.Equal(3, gameBoard.PlayerLives);
            Assert.Equal(Difficulty.Normal, gameBoard.GameDifficulty);
            Assert.Equal(new BoardPosition(1, 1), gameBoard.CurrentBoardPosition);
            Assert.NotEmpty(gameBoard.MinePositions);
            Assert.Empty(gameBoard.HitMinePositions);
        }

        [Fact]
        public void PlaceMines_PlacesCorrectNumberOfMines()
        {
            GameBoard gameBoard = new GameBoard(5, 5, 3, Difficulty.Normal);

            gameBoard.MinePositions.Count.Should().BeGreaterThanOrEqualTo(3) ;
        }

        [Fact]
        public void UpdatePlayerPosition_HitsMine_ReducesLives()
        {
            GameBoard gameBoard = new GameBoard(5, 5, 1, Difficulty.Normal);
            BoardPosition minePosition = gameBoard.MinePositions.First();

            (bool MineHit, bool RemainingLives) result = gameBoard.UpdatePlayerPosition(minePosition);

            Assert.True(result.MineHit);
            Assert.False(result.RemainingLives);
            Assert.Contains(minePosition, gameBoard.HitMinePositions);
        }
    }
}
