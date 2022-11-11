using AFK_Dungeon_Lib.Dungeon;
using AFK_Dungeon_Lib.Pawns;
using AFK_Dungeon_Lib.Pawns.Enemies;
using AFK_Dungeon_Lib.Pawns.Hero;

namespace AFK_Dungeon_Lib.Dungeon.DungeonObjects;

public class EnemyEntity : IPawnEntity
{
	public Coordinate Position { get; set; }
	public IPawn Entity { get; set; }
	public EntityState EntityState { get; set; }
	public IPawn? Target { get; set; }

	public EnemyEntity(Enemy e, Coordinate position)
	{
		Entity = e;
		Position = position;
	}
	public void GetTarget(List<IPawn> targets)
	{
		throw new NotImplementedException();
	}

	public void NextStep()
	{
		throw new NotImplementedException();
	}
}
