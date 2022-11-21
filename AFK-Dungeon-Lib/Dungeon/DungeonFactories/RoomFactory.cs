using AFK_Dungeon_Lib.Dungeon.DungeonComponents;
using AFK_Dungeon_Lib.Pawns.Factories;
using AFK_Dungeon_Lib.Pawns.Enemies;

namespace AFK_Dungeon_Lib.Dungeon.DungeonFactories;
public static class RoomFactory
{
	public static Room GetBasic(int floorModifier, int roomNumber)
	{
		var enemyList = new List<Enemy>();
		var coordinateList = new List<Coordinate>();
		enemyList.Add(EnemyFactory.GetMeleePhys(floorModifier));
		coordinateList.Add(new Coordinate(2, 1));
		enemyList.Add(EnemyFactory.GetMeleePhys(floorModifier));
		coordinateList.Add(new Coordinate(2, 2));
		enemyList.Add(EnemyFactory.GetRangePhys(floorModifier));
		coordinateList.Add(new Coordinate(3, 1));
		enemyList.Add(EnemyFactory.GetRangePhys(floorModifier));
		coordinateList.Add(new Coordinate(3, 2));
		return new Room(enemyList, coordinateList, roomNumber);
	}

	public static BossRoom GetBossRoom(int floorModifier, bool phys, bool ranged)
	{
		var enemyList = new List<Enemy>();
		var coordinateList = new List<Coordinate>();
		Boss boss = EnemyFactory.GetBossBasic(floorModifier, phys, ranged);
		return new BossRoom(enemyList, coordinateList, 10, boss, new Coordinate(1, 1));
	}
	public static MiniBossRoom GetMiniBossRoom(int floorModifier, bool phys, bool ranged)
	{
		var enemyList = new List<Enemy>();
		var coordinateList = new List<Coordinate>();

		Enemy boss = EnemyFactory.GetBossBasic(floorModifier, phys, ranged);
		return new MiniBossRoom(enemyList, coordinateList, 5, boss, new Coordinate(1, 1));
	}
}
