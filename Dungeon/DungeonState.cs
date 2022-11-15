
using AFK_Dungeon_Lib.Dungeon.DungeonComponents;
using AFK_Dungeon_Lib.Dungeon.DungeonObjects;
using AFK_Dungeon_Lib.Pawns;
using AFK_Dungeon_Lib.Pawns.Enemies;
using AFK_Dungeon_Lib.Pawns.Hero;

namespace AFK_Dungeon_Lib.Dungeon;

internal class DungeonState
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

	public DungeonState(List<IPawn> heroes, Zone currentZone, Floor currentFloor, Room currentRoom, int totalEnemiesKilledCount, int clearedRoomCount)
	{
		//initialize objects
		Heroes = new();
		CurrentEnemies = new();
		CurrentEntities = new();
		TotalEnemiesKilledCount = totalEnemiesKilledCount;
		ClearedRoomCount = clearedRoomCount;
		CurrentZone = currentZone;
		CurrentFloor = currentFloor;
		CurrentRoom = currentRoom;

		//fill heroes
		for (int i = 0; i < heroes.Count; i++)
		{
			if (heroes[i] is Hero h)
			{
				Heroes.Add(new(h, h.Position));
			}
		}
		//fill enemies
		for (int i = 0; i < currentRoom.Enemies.Count; i++)
		{
			CurrentEnemies.Add(new(currentRoom.Enemies[i], currentRoom.Enemies[i].Position));
		}

		if (currentRoom is BossRoom b)
		{
			CurrentEnemies.Add(new(b.Boss, b.BossPosition));
		}
		if (currentRoom is MiniBossRoom m)
		{
			CurrentEnemies.Add(new(m.Miniboss, m.MinibossPosition));
		}

		//determine initial turn orders
		CurrentEntities = DungeonDriver.GetInitialTurnOrder(Heroes, CurrentEnemies);
		//put all entities into the grid
		DungeonGrid = new(CurrentEntities);
		//set start time
		TimeStart = DateTime.Now;
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
