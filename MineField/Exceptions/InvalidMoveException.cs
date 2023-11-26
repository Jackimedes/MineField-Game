using MineField.Enums;
using MineField.Records;

namespace MineField.Exceptions
{
    public sealed class InvalidMoveException : Exception
    {
        public InvalidMoveException(BoardPosition currentBoardPosition, PlayerMove playerMove) 
            : base($"Moving {playerMove.Name} is not valid from Position {currentBoardPosition.Column},{currentBoardPosition.Row}")
        { 
        }
    }
}
