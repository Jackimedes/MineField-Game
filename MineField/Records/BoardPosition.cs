namespace MineField.Records
{
    /// <summary>
    /// A position on a game board.
    /// </summary>
    /// <param name="Column">The X value of the position.</param>
    /// <param name="Row">The Y value of the position.</param>
    public sealed record BoardPosition(int Column, int Row)
    {
    }
}
