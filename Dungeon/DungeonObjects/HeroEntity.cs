using AFK_Dungeon_Lib.Dungeon;
using AFK_Dungeon_Lib.Pawns;
using AFK_Dungeon_Lib.Pawns.Hero;
using AFK_Dungeon_Lib.Pawns.Enemies;
using AFK_Dungeon_Lib.Controllers;
using AFK_Dungeon_Lib.AI;
using AFK_Dungeon_Lib.Items.Equipment;
using AFK_Dungeon_Lib.Items.Equipment.Weapon;
using AFK_Dungeon_Lib.Items.Equipment.Offhand;

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
		this.AttackSpeed = h.Stats.AttackSpeed.Current;
		this.TimeBetweenAttacks = 1f / AttackSpeed;
		this.TimeEllapsed = 0f;
		this.state = state;
	}
	public void NextStep()
	{
		//update stats to current
		UpdateStats();
		//check state and act accordingly
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

	//not turn to act, adjust accordingly
	public void Wait()
	{
		TimeEllapsed += 0.02f;
		if (TimeEllapsed >= TimeBetweenAttacks)
		{
			EntityState = EntityState.TakeAction;
			TimeEllapsed -= TimeBetweenAttacks;
		}
	}
	//perform action, attack if weapon, heal if healer
	public void TakeAction()
	{
		GetTarget();
		WeaponClass wc = ((Weapon)((Hero)Entity).EquippedMainHand!).WeaponClass;
		if (wc == WeaponClass.HolyStaff) { Heal(); }
		else { Attack(); }
	}

	public void Attack()
	{
		//determine base Damage
		int damage;
		Hero h = (Hero)Entity;
		float critChance;
		float critDamage;
		bool phys;

		if (h.Phys)
		{
			phys = true;
			if (h.AlternateDmg)
			{
				damage = Convert.ToInt32(h.Stats.DamageDex.Current * h.Stats.DamageModifier.Current);
			}
			else
			{
				damage = Convert.ToInt32(h.Stats.DamagePhys.Current * h.Stats.DamageModifier.Current);
			}
			//set crit to phys crit
			critChance = h.Stats.CritChancePhys.Current;
			critDamage = h.Stats.CritDmgPhys.Current;
		}
		else
		{
			phys = false;
			if (h.AlternateDmg)
			{
				damage = Convert.ToInt32(h.Stats.DamageWis.Current * h.Stats.DamageModifier.Current);
			}
			else
			{
				damage = Convert.ToInt32(h.Stats.DamageMage.Current * h.Stats.DamageModifier.Current);
			}
			//set crit to mage crit
			critChance = h.Stats.CritChanceMage.Current;
			critDamage = h.Stats.CritDmgMage.Current;
		}
		//determine if the attack lands as a crit
		if (state.Random.Random.Next(1, 101) <= critChance)
		{
			damage = Convert.ToInt32(damage * critDamage);
		}

		if (h.Priority == TargetPriority.Most)
		{
			if (((Offhand?)h.EquippedOffhand)?.Modifier == SpellModifier.Line)
			{
				state.DungeonGrid.GetEntityAt(Target)?.ApplyDamage(damage, phys);
				state.DungeonGrid.GetEntityAt(new(Target.X, Target.Y + 1))?.ApplyDamage(damage, phys);
				state.DungeonGrid.GetEntityAt(new(Target.X, Target.Y + 2))?.ApplyDamage(damage, phys);
				state.DungeonGrid.GetEntityAt(new(Target.X, Target.Y + 3))?.ApplyDamage(damage, phys);
			}
			else if (((Offhand?)h.EquippedOffhand)?.Modifier == SpellModifier.Cube)
			{
				state.DungeonGrid.GetEntityAt(Target)?.ApplyDamage(damage, phys);
				state.DungeonGrid.GetEntityAt(new(Target.X, Target.Y + 1))?.ApplyDamage(damage, phys);
				state.DungeonGrid.GetEntityAt(new(Target.X + 1, Target.Y))?.ApplyDamage(damage, phys);
				state.DungeonGrid.GetEntityAt(new(Target.X + 1, Target.Y + 1))?.ApplyDamage(damage, phys);
			}
		}
		else
		{
			state.DungeonGrid.GetEntityAt(Target)?.ApplyDamage(damage, phys);
		}
	}


	public void ApplyDamage(int damage, bool phys)
	{
		Hero h = (Hero)Entity;
		float damageReduction;
		if (phys)
		{
			damageReduction = 1 - ((float)h.Stats.Defense.Final / ((float)h.Stats.Defense.Final + 540.0f));
		}
		else
		{
			damageReduction = 1 - ((float)h.Stats.Resistance.Final / ((float)h.Stats.Resistance.Final + 540.0f));
		}

		h.Stats.Health.Current -= Convert.ToInt32(damage * damageReduction);
	}
	public void Heal()
	{

	}

	public void Kill()
	{

	}

	public void UpdateStats()
	{
		if (AttackSpeed != ((Hero)Entity).Stats.AttackSpeed.Current)
		{
			AttackSpeed = ((Hero)Entity).Stats.AttackSpeed.Current;
			TimeBetweenAttacks = 1f / AttackSpeed;
		}
	}
	public void GetTarget()
	{
		Hero h = (Hero)Entity;
		if (h.Priority != TargetPriority.Most)
		{
			if (state.DungeonGrid.Grid[Target.X, Target.Y] != null)
			{
				if (state.DungeonGrid.Grid[Target.X, Target.Y]!.EntityState == EntityState.Dead)
				{
					Target = Targeter.GetTarget(this, state.CurrentEnemies, state.Random);
				}
				else if (state.DungeonGrid.Grid[Target.X, Target.Y]!.EntityState == EntityState.Untargetable)
				{
					Target = Targeter.GetTarget(this, state.CurrentEnemies, state.Random);
				}
			}
			else
			{
				Target = Targeter.GetTarget(this, state.CurrentEnemies, state.Random);
			}
		}
		else
		{
			Target = Targeter.GetTarget(this, state.CurrentEnemies, state.Random);
		}
	}
}
