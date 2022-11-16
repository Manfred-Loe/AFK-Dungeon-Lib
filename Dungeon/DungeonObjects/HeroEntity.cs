using AFK_Dungeon_Lib.Dungeon;
using AFK_Dungeon_Lib.Pawns;
using AFK_Dungeon_Lib.Pawns.Hero;
using AFK_Dungeon_Lib.Pawns.Enemies;
using AFK_Dungeon_Lib.Controllers;
using AFK_Dungeon_Lib.AI;

namespace AFK_Dungeon_Lib.Dungeon.DungeonObjects;

internal class HeroEntity : IPawnEntity
{
	public Coordinate Position { get; set; }
	public IPawn Entity { get; set; }
	public EntityState EntityState { get; set; }
	public Coordinate Target { get; set; }

	public float AttackSpeed;
	public float TimeBetweenAttacks;
	public float TimeEllapsed;
	readonly DungeonState state;

	public HeroEntity(Hero h, Coordinate position, DungeonState state)
	{
		this.Entity = h;
		this.Position = position;
		this.TimeBetweenAttacks = 1f / h.Stats.AttackSpeed.Current;
		this.TimeEllapsed = 0f;
		this.state = state;
	}
	public void NextStep()
	{
		switch (EntityState)
		{
			case EntityState.Wait: Wait(); break;
			case EntityState.TakeAction: TakeAction(); break;
			case EntityState.Dead: Kill(); break;
			case EntityState.Incapacitated: break;
			case EntityState.Untargetable: TakeAction(); break;
			default: break;
		}
	}

	public void Wait()
	{
		TimeEllapsed += 0.02f;
		if (TimeEllapsed >= TimeBetweenAttacks)
		{
			EntityState = EntityState.TakeAction;
			TimeEllapsed -= TimeBetweenAttacks;
		}
	}
	public void TakeAction()
	{

	}

	public void Kill()
	{

	}
	public void GetTarget()
	{
		Hero h = (Hero)Entity;
		if (h.Priority == TargetPriority.Closest)
		{
			if (state.DungeonGrid.Grid[Target.X, Target.Y] != null)
			{
				if (state.DungeonGrid.Grid[Target.X, Target.Y].EntityState == EntityState.Dead)
				{
					Targeter.GetTarget(this, state.CurrentEnemies, state.Random);
				}
			}
			else
			{
				Targeter.GetTarget(this, state.CurrentEnemies, state.Random);
			}
		}

		if (Target != null)
		{
			Targeter.GetTarget(this, state.CurrentEnemies, state.Random);
		}
	}
}
