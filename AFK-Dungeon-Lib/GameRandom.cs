using AFK_Dungeon_Lib.IOC;

namespace AFK_Dungeon_Lib;
public class GameRandom
{
	public Random Random;

	public GameRandom(GameConfig gc)
	{
		Random = gc.GameSeed.HasValue ? new(gc.GameSeed.Value) : new();
	}
}
