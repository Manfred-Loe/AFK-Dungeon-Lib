using AFK_Console.Game;

namespace AFK_Console;
internal class Program
{
	GameProgram game;
	static void Main(string[] args)
	{
		game = new();
		game.Run();
	}
}