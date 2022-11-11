using AFK_Dungeon_Lib.Dungeon.DungeonComponents;
using AFK_Dungeon_Lib.Pawns;
using AFK_Dungeon_Lib.Pawns.Hero;
using AFK_Dungeon_Lib.Pawns.Enemies;
using AFK_Dungeon_Lib.Dungeon.DungeonObjects;

namespace AFK_Dungeon_Lib.Dungeon;

public class DungeonDriver
{

	public DateTime TimeStart;
	public DateTime TimeEnd;

	public Zone CurrentZone;
	public Floor CurrentFloor;
	public Room CurrentRoom;

	public int EnemiesKilledCount;
	public int ClearedRoomCount;
	public List<HeroEntity> Heroes;
	public List<EnemyEntity> CurrentEnemies;
	public List<IPawnEntity> CurrentEntities;

	public DungeonDriver(List<IPawn> heroes, int initialZone, int currentZone, int currentFloor, int currentRoom)
	{
		//initialize lists and data
		Heroes = new();
		CurrentEnemies = new();
		CurrentEntities = new();
		EnemiesKilledCount = 0;
		//fill heroes
		for (int i = 0; i < heroes.Count; i++)
		{
			if (heroes[i] is Hero h)
			{
				Heroes.Add(new(h, h.Position));
			}
		}

		//load zone/floor/room
		var dl = new DungeonLoader(initialZone, currentZone, 1);
		CurrentZone = dl.GetCurrentZone();
		CurrentFloor = CurrentZone.GetFloorByNumber(currentFloor);
		CurrentRoom = CurrentFloor.GetRoomByNumber(currentRoom);

		//load enemies in first room
		for (int i = 0; i < CurrentRoom.Enemies.Count; i++)
		{
			CurrentEnemies.Add(new(CurrentRoom.Enemies[i], CurrentRoom.Enemies[i].Position));
		}

		if (CurrentRoom is BossRoom b)
		{
			CurrentEnemies.Add(new(b.Boss, b.BossPosition));
		}
		if (CurrentRoom is MiniBossRoom m)
		{
			CurrentEnemies.Add(new(m.Miniboss, m.MinibossPosition));
		}

		//Determine turn order
		CurrentEntities = GetInitialTurnOrder(Heroes, CurrentEnemies);

		//set start time
		TimeStart = DateTime.Now;
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
