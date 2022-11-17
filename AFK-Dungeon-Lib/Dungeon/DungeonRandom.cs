using AFK_Dungeon_Lib.IOC;

namespace AFK_Dungeon_Lib.Dungeon;

public class DungeonRandom
{
	public Random Random;

	public DungeonRandom(GameConfig gc)
	{
		Random = gc.DungeonSeed.HasValue ? new(gc.DungeonSeed.Value) : new();
	}
}
