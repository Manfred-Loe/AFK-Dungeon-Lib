using AFK_Dungeon_Lib.AI;
using AFK_Dungeon_Lib.Dungeon;
using AFK_Dungeon_Lib.Items.Equipment.Offhand;
using AFK_Dungeon_Lib.Pawns;
using AFK_Dungeon_Lib.Pawns.Enemies;
using AFK_Dungeon_Lib.Pawns.Hero;

namespace AFK_Dungeon_Lib.Dungeon.DungeonObjects;

public class EnemyEntity : IPawnEntity
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
		this.TimeBetweenAttacks = 1f / e.Stats.AttackSpeed.Current;
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
		float damageReduction;
		if (phys)
		{
			damageReduction = 1 - ((float)e.Stats.Defense.Final / ((float)e.Stats.Defense.Final + GameConstants.DAMAGE_REDUCTION_MODIFIER));
		}
		else
		{
			damageReduction = 1 - ((float)e.Stats.Resistance.Final / ((float)e.Stats.Resistance.Final + GameConstants.DAMAGE_REDUCTION_MODIFIER));
		}

		e.Stats.Health.Current -= Convert.ToInt32(damage * damageReduction);
		if (e.Stats.Health.Current <= 0)
		{
			Kill();
		}
	}
	public void GetTarget()
	{
		Enemy e = (Enemy)Entity;
		if (e.Priority != TargetPriority.Most)
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
			Target = Targeter.GetTarget(this, state.CurrentEnemies, state.Random);
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
		Coordinate healtarget = Targeter.GetTarget(this, state.CurrentEnemies, state.Random);
		IPawnEntity pawnEntity = (IPawnEntity)state.DungeonGrid.GetEntityAt(healtarget)!;
		((Enemy)pawnEntity.Entity).Stats.Health.Current += ((Enemy)Entity).Stats.Damage.Current;
	}
	public void Kill()
	{
		EntityState = EntityState.Dead;
	}
}
