using AFK_Dungeon_Lib.Dungeon.DungeonComponents;

namespace AFK_Dungeon_Lib.Dungeon.DungeonFactories;
public static class ZoneFactory
{
	public static Zone GenerateZone(int scale, int zoneLevel)
	{
		var floors = new List<Floor>();

		for (int i = 1; i < 11; i++)
		{
			floors.Add(FloorFactory.GenerateBasicFloor(i));
		}
		return new Zone(floors, scale, zoneLevel);
	}
}