using AFK_Dungeon_Lib.Items;
using AFK_Dungeon_Lib.Items.Equipment;
using AFK_Dungeon_Lib.Items.Equipment.Weapon;
using AFK_Dungeon_Lib.Items.Equipment.Offhand;

namespace AFK_Dungeon_Lib.Pawns.Hero;

public class Hero : IPawn
{
	public string Name { get; private set; }
	public int Level { get; private set; }
	public HeroStat Stats { get; set; }
	public long CurrentExp { get; private set; }
	public long ExpToLevel { get; set; }
	public int AvailableAbilityPoints { get; private set; }
	public Coordinate Position { get; private set; }

	public IEquipment? EquippedHelmet;
	public IEquipment? EquippedChest;
	public IEquipment? EquippedBoots;
	public IEquipment? EquippedMainHand;
	public IEquipment? EquippedOffhand;

	public bool RangedWeapon { get; set; }
	public bool Phys { get; set; }
	public bool AlternateDmg { get; set; }
	public bool DualWielding { get; set; }

	public Hero(string name)
	{
		Name = name;
		Level = 1;
		CurrentExp = 0;
		ExpToLevel = HeroCalculator.CalcExpToNextLevel(1);
		AvailableAbilityPoints = 3;
		Position = new(0, 0);
		Stats = new();

		InitializeAbilityScores();
		HeroCalculator.CalcStats(this);
	}
	public Hero() : this("New Hero") { }

	private void InitializeAbilityScores()
	{
		Stats.Strength.Initial = 10;
		Stats.Dexterity.Initial = 10;
		Stats.Vitality.Initial = 10;
		Stats.Intelligence.Initial = 10;
		Stats.Wisdom.Initial = 10;
		Stats.Fortitude.Initial = 10;
		Stats.Will.Initial = 10;
	}

	//gives experience to the hero, if enough it will level them
	public void GiveExp(long xp)
	{
		CurrentExp += xp;
		HeroCalculator.LevelUp(this);
	}
	public void GiveAbilityPoints(int points)
	{
		AvailableAbilityPoints += points;
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
		return ability switch
		{
			StatsEnum.Strength => Stats.Strength.GetValue(state),
			StatsEnum.Dexterity => Stats.Dexterity.GetValue(state),
			StatsEnum.Vitality => Stats.Vitality.GetValue(state),
			StatsEnum.Intelligence => Stats.Intelligence.GetValue(state),
			StatsEnum.Wisdom => Stats.Wisdom.GetValue(state),
			StatsEnum.Fortitude => Stats.Fortitude.GetValue(state),
			StatsEnum.Will => Stats.Will.GetValue(state),
			_ => -1
		};
	}
}