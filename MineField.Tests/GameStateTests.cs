using MineField.Enums;

namespace MineField.Tests
{
    public class GameStateTests
    {
        // Test case for a normal move
        [Fact]
        public void Move_NormalMove_ReturnsDefaultResult()
        {
            // Arrange
            GameState gameState = new GameState(maxRow: 3, maxColumn: 3, playerLives: 3, gameDifficulty: Difficulty.Easy);

            // Act
            Records.MoveResult result = gameState.Move(PlayerMove.Up);

            // Assert
            Assert.Null(result.GameResult);
            Assert.Equal(1, gameState.GameMoveResults.Count);
            Assert.Null(gameState.GameSummary);
        }
    }
}
