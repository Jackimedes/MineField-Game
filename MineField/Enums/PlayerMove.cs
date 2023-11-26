using Ardalis.SmartEnum;
using MineField.Exceptions;
using MineField.Records;

namespace MineField.Enums
{
    public abstract class PlayerMove : SmartEnum<PlayerMove, string>
    {
        public static readonly PlayerMoveUp Up = new PlayerMoveUp(nameof(Up), "U");
        public static readonly PlayerMoveDown Down = new PlayerMoveDown(nameof(Down), "D");
        public static readonly PlayerMoveLeft Left = new PlayerMoveLeft(nameof(Left), "L");
        public static readonly PlayerMoveRight Right = new PlayerMoveRight(nameof(Right), "R");

        protected PlayerMove(string name, string value)
            : base(name, value)
        {
        }

        public abstract bool IsValidMove(GameBoard board);

        public abstract BoardPosition Move(GameBoard board);

        public sealed class PlayerMoveUp : PlayerMove
        {
            public PlayerMoveUp(string name, string value)
                : base(name, value)
            {
            }

            public override bool IsValidMove(GameBoard board)
            {
                return (board.CurrentBoardPosition.Row + 1) < board.MaxRow + 1;
            }

            public override BoardPosition Move(GameBoard board)
            {
                if (!IsValidMove(board))
                    throw new InvalidMoveException(board.CurrentBoardPosition, this);

                return new BoardPosition(board.CurrentBoardPosition.Column, board.CurrentBoardPosition.Row + 1);
            }
        }

        public sealed class PlayerMoveDown : PlayerMove
        {
            public PlayerMoveDown(string name, string value)
                : base(name, value)
            {
            }

            public override bool IsValidMove(GameBoard board)
            {
                return (board.CurrentBoardPosition.Row - 1) > 0;
            }

            public override BoardPosition Move(GameBoard board)
            {
                if (!IsValidMove(board))
                    throw new InvalidMoveException(board.CurrentBoardPosition, this);

                return new BoardPosition(board.CurrentBoardPosition.Column, board.CurrentBoardPosition.Row - 1);
            }
        }

        public sealed class PlayerMoveLeft : PlayerMove
        {
            public PlayerMoveLeft(string name, string value)
                : base(name, value)
            {
            }

            public override bool IsValidMove(GameBoard board)
            {
                return (board.CurrentBoardPosition.Column - 1) > 0;
            }

            public override BoardPosition Move(GameBoard board)
            {
                if (!IsValidMove(board))
                    throw new InvalidMoveException(board.CurrentBoardPosition, this);

                return new BoardPosition(board.CurrentBoardPosition.Column - 1, board.CurrentBoardPosition.Row);
            }
        }

        public sealed class PlayerMoveRight : PlayerMove
        {
            public PlayerMoveRight(string name, string value)
                : base(name, value)
            {
            }

            public override bool IsValidMove(GameBoard board)
            {
                return (board.CurrentBoardPosition.Column + 1) < board.MaxColumn + 1;
            }

            public override BoardPosition Move(GameBoard board)
            {
                if (!IsValidMove(board))
                    throw new InvalidMoveException(board.CurrentBoardPosition, this);

                return new BoardPosition(board.CurrentBoardPosition.Column + 1, board.CurrentBoardPosition.Row);
            }
        }
    }
}
