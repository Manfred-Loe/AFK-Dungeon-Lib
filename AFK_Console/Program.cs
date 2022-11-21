using AFK_Console.Game;

namespace AFK_Console;
internal static class Program
{
	static void Main(string[] args)
	{
		GameProgram game = new();
		game.Run();
	}
}