using AFK_Dungeon_Lib.Pawns.Enemies;

namespace AFK_Dungeon_Lib.Dungeon.DungeonComponents;
public class Room
{
	public List<Enemy> Enemies { get; }
	//coodinates should range from (0-1,0-3)
	public int RoomNumber { get; }

	public Room(List<Enemy> e, List<Coordinate> c, int rn)
	{
		Enemies = e;

		for (int i = 0; i < e.Count; i++)
		{
			e[i].SetPosition(c[i]);
		}
		RoomNumber = rn;
	}

	public void RemoveEnemy(int i)
	{
		Enemies.RemoveAt(i);
	}
}
