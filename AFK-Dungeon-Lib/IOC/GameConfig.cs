namespace AFK_Dungeon_Lib.IOC;

public class GameConfig
{
	public int? GameSeed;
	public int? DungeonSeed;
	public int ZoneScaling;
	public int InitialZoneLevel;
	public int CurrentZoneLevel;
	public int CurrentFloor;
	public int CurrentRoom;

	//need to configure either from file or whatever
	public GameConfig(int zoneScaling, int initialZone, int currentZone, int currentFloor, int currentRoom, int? gameSeed, int? dungeonSeed)
	{
		this.GameSeed = gameSeed;
		this.DungeonSeed = dungeonSeed;
		this.ZoneScaling = zoneScaling;
		this.InitialZoneLevel = initialZone;
		this.CurrentZoneLevel = currentZone;
		this.CurrentFloor = currentFloor;
		this.CurrentRoom = currentRoom;
	}
}
