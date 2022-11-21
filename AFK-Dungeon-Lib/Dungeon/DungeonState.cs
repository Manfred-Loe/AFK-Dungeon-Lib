
using AFK_Dungeon_Lib.Dungeon.DungeonComponents;
using AFK_Dungeon_Lib.Dungeon.DungeonObjects;
using AFK_Dungeon_Lib.Pawns;
using AFK_Dungeon_Lib.Pawns.Enemies;
using AFK_Dungeon_Lib.Pawns.Hero;

namespace AFK_Dungeon_Lib.Dungeon;

public class DungeonState
{
	public DateTime TimeStart;
	public DateTime TimeEnd;

	public Zone CurrentZone;
	public Floor CurrentFloor;
	public Room CurrentRoom;

	public int TotalEnemiesKilledCount;
	public int ClearedRoomCount;
	public List<HeroEntity> Heroes;
	public List<EnemyEntity> CurrentEnemies;
	public List<IPawnEntity> CurrentEntities;
	public DungeonGrid DungeonGrid;
	readonly public DungeonRandom Random;

	public DungeonState(List<Hero> heroes, DungeonRandom random, Zone currentZone, Floor currentFloor, Room currentRoom, int totalEnemiesKilledCount, int clearedRoomCount)
	{
		//initialize objects
		this.Heroes = new();
		this.CurrentEnemies = new();
		this.CurrentEntities = new();
		this.TotalEnemiesKilledCount = totalEnemiesKilledCount;
		this.ClearedRoomCount = clearedRoomCount;
		this.CurrentZone = currentZone;
		this.CurrentFloor = currentFloor;
		this.CurrentRoom = currentRoom;
		this.Random = random;

		//fill heroes
		for (int i = 0; i < heroes.Count; i++)
		{
			Heroes.Add(new(heroes[i], heroes[i].Position, this));
		}
		//fill enemies
		for (int i = 0; i < currentRoom.Enemies.Count; i++)
		{
			CurrentEnemies.Add(new(currentRoom.Enemies[i], currentRoom.Enemies[i].Position, this));
		}

		if (currentRoom is BossRoom b)
		{
			CurrentEnemies.Add(new(b.Boss, b.BossPosition, this));
		}
		if (currentRoom is MiniBossRoom m)
		{
			CurrentEnemies.Add(new(m.Miniboss, m.MinibossPosition, this));
		}

		//determine initial turn orders
		this.CurrentEntities = DungeonDriver.GetInitialTurnOrder(Heroes, CurrentEnemies);
		//put all entities into the grid
		this.DungeonGrid = new(CurrentEntities);
		//set start time
		this.TimeStart = DateTime.Now;
	}

	public void UpdateEnemies()
	{
		bool updated = false;
		for (int i = 0; i < CurrentEnemies.Count; i++)
		{
			if (CurrentEnemies[i].EntityState == EntityState.Dead)
			{
				CurrentEnemies.RemoveAt(i);
				TotalEnemiesKilledCount++;
				i--;
				updated = true;
			}
		}
		if (updated)
		{
			UpdateEntities();
		}
	}
	public void UpdateHeroes()
	{
		bool updated = false;
		for (int i = 0; i < Heroes.Count; i++)
		{
			if (Heroes[i].EntityState == EntityState.Dead)
			{
				Heroes.RemoveAt(i);
				i--;
				updated = true;
			}
		}
		if (updated)
		{
			UpdateEntities();
		}
	}
	public void UpdateEntities()
	{
		for (int i = 0; i < CurrentEntities.Count; i++)
		{
			if (CurrentEntities[i].EntityState == EntityState.Dead)
			{
				DungeonGrid.RemoveEntityAt(CurrentEntities[i].Position);
				CurrentEntities.RemoveAt(i);
				i--;
			}
		}
	}
}
