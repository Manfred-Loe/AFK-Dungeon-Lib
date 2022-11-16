using AFK_Dungeon_Lib.Dungeon.DungeonComponents;
using AFK_Dungeon_Lib.Pawns;
using AFK_Dungeon_Lib.Pawns.Hero;
using AFK_Dungeon_Lib.Pawns.Enemies;
using AFK_Dungeon_Lib.Dungeon.DungeonObjects;
using AFK_Dungeon_Lib.IOC;
using AFK_Dungeon_Lib.Dungeon.DungeonFactories;

namespace AFK_Dungeon_Lib.Dungeon;

internal class DungeonDriver
{
	readonly DungeonState DungeonState;
	readonly DungeonRandom random;
	public DungeonDriver(List<IPawn> heroes, GameConfig gc, ZoneFactory zf, DungeonRandom random)
	{
		var dl = new DungeonLoader(gc, zf);
		//load zone/floor/room
		var zone = dl.GetCurrentZone();
		var floor = zone.GetFloorByNumber(gc.CurrentFloor);
		var room = floor.GetRoomByNumber(gc.CurrentRoom);
		DungeonState = new(heroes, zone, floor, room, 0, 0);
		this.random = random;
	}

	public void NextStep()
	{
		for (int i = 0; i < DungeonState.CurrentEntities.Count; i++)
		{
			//Next Step
			DungeonState.CurrentEntities[i].NextStep();
			//update DungeonState
		}
	}
	private static int CompareDexterity(IPawnEntity x, IPawnEntity y)
	{
		int returnValue;

		if (x.Entity is Hero heroX)
		{
			if (y.Entity is Hero heroY)
			{
				returnValue = heroX.Stats.Dexterity.Final.CompareTo(heroY.Stats.Dexterity.Final);
			}
			else
			{
				Enemy enemy = (Enemy)y.Entity;
				returnValue = heroX.Stats.Dexterity.Final.CompareTo(enemy.Stats.Dexterity.Final);
			}
		}
		else
		{
			Enemy enemy = (Enemy)x.Entity;
			if (y.Entity is Hero heroY)
			{
				returnValue = enemy.Stats.Dexterity.Final.CompareTo(heroY.Stats.Dexterity.Final);
			}
			else
			{
				Enemy enemyTwo = (Enemy)y.Entity;
				returnValue = enemy.Stats.Dexterity.Final.CompareTo(enemyTwo.Stats.Dexterity.Final);
			}
		}
		return returnValue;
	}

	public static List<IPawnEntity> GetInitialTurnOrder(List<HeroEntity> heroes, List<EnemyEntity> enemies)
	{
		List<IPawnEntity> turns = new();
		turns.AddRange(heroes);
		turns.AddRange(enemies);
		turns.Sort(CompareDexterity);
		return turns;
	}
}
