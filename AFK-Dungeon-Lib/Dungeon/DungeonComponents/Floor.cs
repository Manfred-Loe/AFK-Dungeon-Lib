namespace AFK_Dungeon_Lib.Dungeon.DungeonComponents;
public class Floor
{
	public List<Room> Rooms { get; }
	public int LevelScaling { get; }
	public int FloorNumber { get; }

	public Floor(List<Room> r, int scale, int floorNumber)
	{
		Rooms = r;
		LevelScaling = scale;
		FloorNumber = floorNumber;
	}
	public Room GetRoomByNumber(int roomNumber)
	{
		return Rooms[roomNumber - 1];
	}
}
