using AFK_Dungeon_Lib.Pawns.Enemies;

namespace AFK_Dungeon_Lib.Dungeon.DungeonComponents;
public class BossRoom : Room
{
	public Enemy Boss { get; }
	public Coordinate BossPosition { get; private set; }

	public BossRoom(List<Enemy> e, List<Coordinate> c, int rn, Enemy boss, Coordinate pos) : base(e, c, rn)
	{
		Boss = boss;
		BossPosition = pos;
	}
}
