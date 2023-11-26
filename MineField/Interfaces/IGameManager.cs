using MineField.Enums;

namespace MineField.Interfaces
{
    public interface IGameManager
    {
        GameState NewGame(int maxColumn = 8, int maxRow = 8, int playerLives = 2, Difficulty gameDifficulty = Difficulty.Normal);
        string GameStart();
        string AddPlayerInput(string playerInput);
    }
}
