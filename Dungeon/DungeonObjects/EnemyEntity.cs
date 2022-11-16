using AFK_Dungeon_Lib.AI;
using AFK_Dungeon_Lib.Dungeon;
using AFK_Dungeon_Lib.Pawns;
using AFK_Dungeon_Lib.Pawns.Enemies;
using AFK_Dungeon_Lib.Pawns.Hero;

namespace AFK_Dungeon_Lib.Dungeon.DungeonObjects;

internal class EnemyEntity : IPawnEntity
{
	public Coordinate Position { get; set; }
	public IPawn Entity { get; set; }
	public EntityState EntityState { get; set; }
	public Coordinate Target { get; set; }
	readonly DungeonState state;

	public EnemyEntity(Enemy e, Coordinate position, DungeonState state)
	{
		Entity = e;
		Position = position;
		this.state = state;
	}
	public void NextStep()
	{
		if (Target != null)
		{
			Targeter.GetTarget(this, state.Heroes, state.Random);
		}
	}
	public void GetTarget()
	{
		if (Target != null)
		{
			Targeter.GetTarget(this, state.CurrentEnemies, state.Random);
		}
	}
}
