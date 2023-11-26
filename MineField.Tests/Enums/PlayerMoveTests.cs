using MineField.Enums;
using MineField.Exceptions;
using MineField.Records;

namespace MineField.Tests.Enums
{
    public class PlayerMoveTests
    {
        [Fact]
        public void PlayerMoveUp_IsValidMove_ReturnsTrue()
        {
            GameBoard gameBoard = new GameBoard(5, 5, 3, Difficulty.Normal);

            bool isValidMove = PlayerMove.Up.IsValidMove(gameBoard);

            Assert.True(isValidMove);
        }

        [Fact]
        public void PlayerMoveUp_Move_ReturnsCorrectBoardPosition()
        {
            GameBoard gameBoard = new GameBoard(5, 5, 3, Difficulty.Normal);

            BoardPosition newPosition = PlayerMove.Up.Move(gameBoard);

            Assert.Equal(new BoardPosition(1, 2), newPosition);
        }

        [Fact]
        public void PlayerMoveUp_InvalidMove_ThrowsInvalidMoveException()
        {
            GameBoard gameBoard = new GameBoard(5, 5, 3, Difficulty.Normal);
            gameBoard.UpdatePlayerPosition(new BoardPosition(1, 5)); // Set position at the top

            Assert.Throws<InvalidMoveException>(() => PlayerMove.Up.Move(gameBoard));
        }

        [Fact]
        public void PlayerMoveDown_IsValidMove_ReturnsTrue()
        {
            GameBoard gameBoard = new GameBoard(5, 5, 3, Difficulty.Normal);
            gameBoard.UpdatePlayerPosition(new BoardPosition(1, 2));

            bool isValidMove = PlayerMove.Down.IsValidMove(gameBoard);

            Assert.True(isValidMove);
        }

        [Fact]
        public void PlayerMoveDown_Move_ReturnsCorrectBoardPosition()
        {
            GameBoard gameBoard = new GameBoard(5, 5, 3, Difficulty.Normal);
            gameBoard.UpdatePlayerPosition(new BoardPosition(1, 2));

            BoardPosition newPosition = PlayerMove.Down.Move(gameBoard);

            Assert.Equal(new BoardPosition(1, 1), newPosition);
        }

        [Fact]
        public void PlayerMoveDown_InvalidMove_ThrowsInvalidMoveException()
        {
            GameBoard gameBoard = new GameBoard(5, 5, 3, Difficulty.Normal);

            Assert.Throws<InvalidMoveException>(() => PlayerMove.Down.Move(gameBoard));
        }

        [Fact]
        public void PlayerMoveLeft_IsValidMove_ReturnsTrue()
        {
            GameBoard gameBoard = new GameBoard(5, 5, 3, Difficulty.Normal);
            gameBoard.UpdatePlayerPosition(new BoardPosition(2, 1));

            bool isValidMove = PlayerMove.Left.IsValidMove(gameBoard);

            Assert.True(isValidMove);
        }

        [Fact]
        public void PlayerMoveLeft_Move_ReturnsCorrectBoardPosition()
        {
            GameBoard gameBoard = new GameBoard(5, 5, 3, Difficulty.Normal);
            gameBoard.UpdatePlayerPosition(new BoardPosition(2, 1));

            BoardPosition newPosition = PlayerMove.Left.Move(gameBoard);

            Assert.Equal(new BoardPosition(1, 1), newPosition);
        }

        [Fact]
        public void PlayerMoveLeft_InvalidMove_ThrowsInvalidMoveException()
        {
            GameBoard gameBoard = new GameBoard(5, 5, 3, Difficulty.Normal);

            Assert.Throws<InvalidMoveException>(() => PlayerMove.Left.Move(gameBoard));
        }

        [Fact]
        public void PlayerMoveRight_IsValidMove_ReturnsTrue()
        {
            GameBoard gameBoard = new GameBoard(5, 5, 3, Difficulty.Normal);

            bool isValidMove = PlayerMove.Right.IsValidMove(gameBoard);

            Assert.True(isValidMove);
        }

        [Fact]
        public void PlayerMoveRight_Move_ReturnsCorrectBoardPosition()
        {
            GameBoard gameBoard = new GameBoard(5, 5, 3, Difficulty.Normal);

            BoardPosition newPosition = PlayerMove.Right.Move(gameBoard);

            Assert.Equal(new BoardPosition(2, 1), newPosition);
        }

        [Fact]
        public void PlayerMoveRight_InvalidMove_ThrowsInvalidMoveException()
        {
            GameBoard gameBoard = new GameBoard(5, 5, 3, Difficulty.Normal);
            gameBoard.UpdatePlayerPosition(new BoardPosition(5, 1));

            Assert.Throws<InvalidMoveException>(() => PlayerMove.Right.Move(gameBoard));
        }
    }
}
