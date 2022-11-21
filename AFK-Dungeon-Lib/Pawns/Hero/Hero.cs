using AFK_Dungeon_Lib.Items;
using AFK_Dungeon_Lib.Items.Equipment;
using AFK_Dungeon_Lib.Items.Equipment.Weapon;
using AFK_Dungeon_Lib.Items.Equipment.Offhand;
using AFK_Dungeon_Lib.AI;

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
	public TargetPriority Priority { get; set; }

	public IEquipment? EquippedHelmet;
	public IEquipment? EquippedChest;
	public IEquipment? EquippedBoots;
	public IEquipment? EquippedMainHand;
	public IEquipment? EquippedOffhand;

	public bool RangedWeapon { get; set; }
	public bool Phys { get; set; }
	public bool AlternateDmg { get; set; }
	public bool DualWielding { get; set; }

	public Hero(string name, int level, int currentExp, int availableAbilityPoints, Coordinate position, HeroStat stats, Helmet? helmet, Chest? chest, Boots? boots, Weapon? mainHand, IEquipment? offHand)
	{
		Name = name;
		Level = level;
		CurrentExp = currentExp;
		ExpToLevel = HeroCalculator.CalcExpToNextLevel(Level);
		AvailableAbilityPoints = availableAbilityPoints;
		Position = position;
		Stats = stats;

		//equip things if they should be equipped
		if (helmet != null) { EquippedHelmet = helmet; }
		if (chest != null) { EquippedChest = chest; }
		if (boots != null) { EquippedBoots = boots; }
		if (mainHand != null) { EquippedMainHand = mainHand; }
		if (offHand != null) { EquippedOffhand = offHand; }

		/*#! This needs to be change, only works for initialization on new character
		  #! Saved Characters will be overwritten, this is bad. Leave for now, testing*/
		InitializeAbilityScores();
		HeroCalculator.CalcStats(this);
	}
	public Hero(string name) : this(name, 1, 0, 3, new(0, 0), new(), null, null, null, null, null) { }
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
	public void LevelUp(StatsEnum ability)
	{
		if (AvailableAbilityPoints >= 0)
		{
			AvailableAbilityPoints--;
			switch (ability)
			{
				case StatsEnum.Strength: Stats.Strength.Initial++; break;
				case StatsEnum.Dexterity: Stats.Dexterity.Initial++; break;
				case StatsEnum.Vitality: Stats.Vitality.Initial++; break;
				case StatsEnum.Intelligence: Stats.Intelligence.Initial++; break;
				case StatsEnum.Wisdom: Stats.Wisdom.Initial++; break;
				case StatsEnum.Fortitude: Stats.Fortitude.Initial++; break;
				case StatsEnum.Will: Stats.Will.Initial++; break;
				default: AvailableAbilityPoints++; break;
			}
			HeroCalculator.CalcStats(this);
		}
	}
	public bool EquipGear(IEquipment equipment, bool offhandWeapon)
	{
		EquipmentType type = offhandWeapon ? EquipmentType.OffHand : equipment.Type;
		switch (type)
		{
			case EquipmentType.Helmet:
				if (EquippedHelmet != null)
				{
					EquippedHelmet.EquippedTo = null;
				}
				EquippedHelmet = equipment;
				break;
			case EquipmentType.Boots:
				if (EquippedBoots != null)
				{
					EquippedBoots.EquippedTo = null;
				}
				EquippedBoots = equipment;
				break;
			case EquipmentType.Chest:
				if (EquippedChest != null)
				{
					EquippedChest.EquippedTo = null;
				}
				EquippedChest = equipment;
				break;
			case EquipmentType.Weapon:
				if (EquippedOffhand != null)
				{
					if (((Weapon)equipment).TwoHanded)
					{
						return false;
					}
				}
				if (EquippedMainHand != null)
				{
					EquippedMainHand.EquippedTo = null;
				}
				EquippedMainHand = equipment;
				break;
			case EquipmentType.OffHand:

				if (EquippedMainHand is Weapon w)
				{
					if (w.TwoHanded)
					{
						return false;
					}
				}
				if (EquippedOffhand != null)
				{
					EquippedOffhand.EquippedTo = null;
				}
				EquippedOffhand = equipment;
				break;
		}
		HeroCalculator.CalcStats(this);
		return true;
	}
	public void ResetHero()
	{
		AvailableAbilityPoints = 3 * Level;
		InitializeAbilityScores();
		HeroCalculator.CalcStats(this);
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