namespace AFK_Dungeon_Lib.Dungeon.DungeonComponents;
public class Floor
{
	public List<Room> Rooms { get; private set; }
	public int LevelScaling { get; private set; }
	public int FloorNumber { get; private set; }

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
