namespace AFK_Dungeon_Lib.Pawns.Enemies;

public class Enemy : IPawn
{
	public string Name { get; private set; }
	public int Level { get; private set; }
	public EnemyStat Stats;
	public Coordinate Position { get; private set; }
	public bool PhysicalDamage;
	public bool RangedWeapon;
	public bool AlternateDamage;

	public Enemy(string name, int level, bool phys, bool range, bool alternateDmg, Coordinate pos) :
		this(name, level, phys, range, alternateDmg, pos, 10, 0.1f, 0.5f, 1.0f, 30, 10, 10, 10, 10, 10, 10, 10, 10, 10)
	{
	}
	public Enemy() : this("Basic Enemy", 1, true, false, false, new(0, 0)) { }

	public Enemy(string name, int level, bool phys, bool range, bool alternateDmg, Coordinate pos,
				int baseDamage, float baseCritChance, float baseCritDamage, float attackSpeed,
				int baseHealth, int baseDefense, int baseResist,
				int strength, int dexterity, int vitality, int intelligence, int wisdom, int fortitude, int will)
	{
		this.Name = name;
		this.Level = level;
		this.Position = pos;
		this.PhysicalDamage = phys;
		this.RangedWeapon = range;
		this.AlternateDamage = alternateDmg;
		Stats = new();
		Stats.Strength.Initial = strength;
		Stats.Dexterity.Initial = dexterity;
		Stats.Vitality.Initial = vitality;
		Stats.Intelligence.Initial = intelligence;
		Stats.Wisdom.Initial = wisdom;
		Stats.Fortitude.Initial = fortitude;
		Stats.Will.Initial = will;
		Stats.Damage.Initial = baseDamage;
		Stats.CritChance.Initial = baseCritChance;
		Stats.CritDamage.Initial = baseCritDamage;
		Stats.AttackSpeed.Initial = attackSpeed;
		Stats.Health.Initial = baseHealth;
		Stats.Defense.Initial = baseDefense;
		Stats.Resistance.Initial = baseResist;
		EnemyCalculator.InitializeEnemy(this);
		EnemyCalculator.CalcStats(this);
		EnemyCalculator.ResetCurrent(this);
	}

	public void IncrementLevel(int level)
	{
		Level += level;
	}
	public void ChangeName(string newName)
	{
		Name = newName;
	}
	public void SetPosition(Coordinate c)
	{
		Position = c;
	}

	public int GetAbilityScore(StatsEnum ability, StatStateEnum state)
	{
		throw new NotImplementedException();
	}

}
