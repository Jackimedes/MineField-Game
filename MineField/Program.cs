using MineField;
using MineField.Interfaces;

IGameManager gameManager = new ConsoleGameManager();
Console.WriteLine(gameManager.GameStart());

while(true)
{
    string input = Console.ReadLine();
    
    if( input == null ) continue;
    else input = input.Trim().ToUpper();
    
    if(input == "Y")
    {
        gameManager.NewGame();
        Console.WriteLine(gameManager.GameStart());
    }
    else
    {
        Console.Write(gameManager.AddPlayerInput(input));
    }
}
