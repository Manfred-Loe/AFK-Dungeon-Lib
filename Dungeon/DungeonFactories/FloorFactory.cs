using AFK_Dungeon_Lib.Dungeon.DungeonComponents;

namespace AFK_Dungeon_Lib.Dungeon.DungeonFactories;
public static class FloorFactory
{
	//this is generic as a test case
	//and will make a floor, rooms containing 2 melee 2 ranged phys units
	//and miniboss as melee damage dealers;
	public static Floor GenerateBasicFloor(int floorLevel)
	{
		return new Floor(new List<Room>
		{
			RoomFactory.GetBasic(floorLevel, 1),
			RoomFactory.GetBasic(floorLevel, 2),
			RoomFactory.GetBasic(floorLevel, 3),
			RoomFactory.GetBasic(floorLevel, 4),
			RoomFactory.GetMiniBossRoom(floorLevel, true, false),
			RoomFactory.GetBasic(floorLevel, 6),
			RoomFactory.GetBasic(floorLevel, 7),
			RoomFactory.GetBasic(floorLevel, 8),
			RoomFactory.GetBasic(floorLevel, 9),
			RoomFactory.GetBossRoom(floorLevel, true, false)
		}, 1, 1);
	}
}
