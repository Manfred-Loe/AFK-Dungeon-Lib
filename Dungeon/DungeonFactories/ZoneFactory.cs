using AFK_Dungeon_Lib.Dungeon.DungeonComponents;
using AFK_Dungeon_Lib.IOC;

namespace AFK_Dungeon_Lib.Dungeon.DungeonFactories;
public class ZoneFactory
{
	public readonly int scale;
	public ZoneFactory(GameConfig gc)
	{
		this.scale = gc.ZoneScaling;
	}

	public Zone GenerateZone(int zoneLevel)
	{
		var floors = new List<Floor>();

		for (int i = 1; i < 11; i++)
		{
			floors.Add(FloorFactory.GenerateBasicFloor(i));
		}
		return new Zone(floors, scale, zoneLevel);
	}
}