namespace AFK_Dungeon_Lib.Dungeon.DungeonComponents;
public class Zone
{
	public List<Floor> Floors { get; private set; }
	public int LevelScaling { get; private set; }
	public int ZoneNumber { get; private set; }

	public Zone(List<Floor> f, int scale, int zoneNumber)
	{
		this.Floors = f;
		this.LevelScaling = scale;
		this.ZoneNumber = zoneNumber;
	}

	public Floor GetFloorByNumber(int floorNumber)
	{
		return Floors[floorNumber - 1];
	}

}
