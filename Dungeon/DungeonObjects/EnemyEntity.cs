using AFK_Dungeon_Lib.AI;
using AFK_Dungeon_Lib.Dungeon;
using AFK_Dungeon_Lib.Items.Equipment.Offhand;
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
	public float TimeBetweenAttacks;
	public float TimeEllapsed;
	readonly DungeonState state;

	public EnemyEntity(Enemy e, Coordinate position, DungeonState state)
	{
		Entity = e;
		Position = position;
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
		GetTarget();
		if (!((Enemy)Entity).Healer)
		{
			Attack();
		}
		else
		{
			Heal();
		}
	}
	public void ApplyDamage(int damage, bool phys)
	{
		Enemy e = (Enemy)Entity;
		float damageReduction = 1 - ((float)e.Stats.Defense.Final / ((float)e.Stats.Defense.Final + 540.0f));
		e.Stats.Health.Current -= Convert.ToInt32(damage * damageReduction);
	}
	public void GetTarget()
	{
		Enemy e = (Enemy)Entity;
		if (e.Priority == TargetPriority.Closest)
		{
			if (state.DungeonGrid.Grid[Target.X, Target.Y] != null)
			{
				if (state.DungeonGrid.Grid[Target.X, Target.Y]!.EntityState == EntityState.Dead)
				{
					Targeter.GetTarget(this, state.Heroes, state.Random);
				}
				else if (state.DungeonGrid.Grid[Target.X, Target.Y]!.EntityState == EntityState.Untargetable)
				{
					Targeter.GetTarget(this, state.Heroes, state.Random);
				}
			}
			else
			{
				Targeter.GetTarget(this, state.CurrentEnemies, state.Random);
			}
		}
		else
		{

		}
	}

	public void Attack()
	{
		//determine base Damage
		Enemy e = (Enemy)Entity;
		int damage = e.Stats.Damage.Current;
		float critChance = e.Stats.CritChance.Current;
		float critDamage = e.Stats.CritDamage.Current;

		//determine if attack is a crit
		if (state.Random.Random.Next(1, 101) <= critChance)
		{
			damage = Convert.ToInt32(damage * critDamage);
		}

		if (e.Priority == TargetPriority.Most)
		{
			if (e.Modifier == SpellModifier.Line)
			{
				state.DungeonGrid.GetEntityAt(Target)?.ApplyDamage(damage, e.PhysicalDamage);
				state.DungeonGrid.GetEntityAt(new(Target.X, Target.Y + 1))?.ApplyDamage(damage, e.PhysicalDamage);
				state.DungeonGrid.GetEntityAt(new(Target.X, Target.Y + 2))?.ApplyDamage(damage, e.PhysicalDamage);
				state.DungeonGrid.GetEntityAt(new(Target.X, Target.Y + 3))?.ApplyDamage(damage, e.PhysicalDamage);
			}
			else if (e.Modifier == SpellModifier.Cube)
			{
				state.DungeonGrid.GetEntityAt(Target)?.ApplyDamage(damage, e.PhysicalDamage);
				state.DungeonGrid.GetEntityAt(new(Target.X, Target.Y + 1))?.ApplyDamage(damage, e.PhysicalDamage);
				state.DungeonGrid.GetEntityAt(new(Target.X + 1, Target.Y))?.ApplyDamage(damage, e.PhysicalDamage);
				state.DungeonGrid.GetEntityAt(new(Target.X + 1, Target.Y + 1))?.ApplyDamage(damage, e.PhysicalDamage);
			}
		}
		else
		{
			state.DungeonGrid.GetEntityAt(Target)?.ApplyDamage(damage, e.PhysicalDamage);
		}

	}
	public void Heal()
	{

	}
	public void Kill()
	{

	}
}
