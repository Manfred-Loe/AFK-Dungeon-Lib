using AFK_Dungeon_Lib.Pawns.Enemies;

namespace AFK_Dungeon_Lib.Dungeon.DungeonComponents;
public class MiniBossRoom : Room
{
	public Enemy Miniboss { get; }
	public List<Enemy> Enemies { get; private set; }

	public Coordinate MinibossPosition { get; private set; }

	public MiniBossRoom(List<Enemy> e, List<Coordinate> c, int rn, Enemy miniboss, Coordinate pos) : base(e, c, rn)
	{
		Enemies = e;
		Miniboss = miniboss;
		MinibossPosition = pos;
	}
}
