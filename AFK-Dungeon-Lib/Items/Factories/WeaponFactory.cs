using AFK_Dungeon_Lib.Items.Equipment;
using AFK_Dungeon_Lib.Items.Equipment.Weapon;

namespace AFK_Dungeon_Lib.Items.Factories;

public class WeaponFactory
{
	//Will create a random weapon
	readonly GameRandom random;

	public WeaponFactory(GameRandom random)
	{
		this.random = random;
	}

	public static Weapon GetSimple()
	{
		List<EquipmentStat> listStats = new()
		{
			new(EquipmentMod.Strength,10f),
			new(EquipmentMod.Dexterity,10f),
			new(EquipmentMod.AttackSpeed,0.1f)
		};
		return new("Simple 1H Sword", false, 1, 1.5f, 0.05f, 0.5f, WeaponClass.OneHandedSword, Rarity.Rare, 1.0f, 0, listStats);
	}

	public Weapon CraftWeapon(Rarity r)
	{
		WeaponClass wepClass = RandomWeaponClass();
		return wepClass switch
		{
			WeaponClass.OneHandedSword => CraftOneHandedSword(r),
			WeaponClass.TwoHandedSword => CraftTwoHandedSword(r),
			WeaponClass.OneHandedMace => CraftOneHandedMace(r),
			WeaponClass.TwoHandedMace => CraftTwoHandedMace(r),
			WeaponClass.OneHandedAxe => CraftOneHandedAxe(r),
			WeaponClass.TwoHandedAxe => CraftTwoHandedAxe(r),
			WeaponClass.Dagger => CraftDagger(r),
			WeaponClass.Bow => CraftBow(r),
			WeaponClass.Crossbow => CraftCrossbow(r),
			WeaponClass.ArcaneStaff => CraftArcaneStaff(r),
			WeaponClass.HolyStaff => CraftHolyStaff(r),
			WeaponClass.Wand => CraftWand(r),
			_ => CraftOneHandedSword(r),
		};
	}
	private Weapon Craft(string weaponName, bool TwoHanded, Rarity r, WeaponClass weaponClass,
						float baseSpeed, float baseDmg, float baseCritChance, float baseCritDamage,
						EquipmentMod primary, float primaryValue,
						EquipmentMod secondary, float secondaryValue,
						EquipmentMod tertiary, float tertiaryValue)
	{
		float modifier = (GetModifier() / 100f) + 1f;
		float aSpeed = baseSpeed * modifier;
		float dmg = baseDmg * modifier;
		float baseCC = baseCritChance * modifier;
		float baseCD = baseCritDamage * modifier;
		var stats = new List<EquipmentStat>();

		if (r == Rarity.Common)
		{
			stats.Add(new EquipmentStat(primary, 0f));
		}
		else if (r == Rarity.Uncommon)
		{
			stats.Add(new EquipmentStat(primary, primaryValue));
		}
		else if (r == Rarity.Rare)
		{
			stats.Add(new EquipmentStat(primary, primaryValue));
			if (random.Random.Next(0, 2) == 0)
			{
				stats.Add(new EquipmentStat(secondary, secondaryValue));
			}
			else
			{
				stats.Add(new EquipmentStat(tertiary, tertiaryValue));
			}
		}
		else if (r == Rarity.Legendary)
		{
			stats.Add(new EquipmentStat(primary, primaryValue));
			stats.Add(new EquipmentStat(secondary, secondaryValue));
			stats.Add(new EquipmentStat(tertiary, tertiaryValue));
		}
		else
		{
			//fancy shit because unique
		}
		return new Weapon(weaponName, TwoHanded, aSpeed, dmg, baseCC,
								baseCD, weaponClass, r, modifier, 0, stats);
	}

	public Weapon CraftPhysWeapon(Rarity r)
	{
		return random.Random.Next(0, 9) switch
		{
			0 => CraftOneHandedSword(r),
			1 => CraftTwoHandedSword(r),
			2 => CraftOneHandedMace(r),
			3 => CraftTwoHandedMace(r),
			4 => CraftOneHandedAxe(r),
			5 => CraftTwoHandedAxe(r),
			6 => CraftDagger(r),
			7 => CraftBow(r),
			8 => CraftCrossbow(r),
			_ => CraftOneHandedSword(r),
		};
	}

	public Weapon CraftMagicWeapon(Rarity r)
	{
		return random.Random.Next(0, 3) switch
		{
			0 => CraftArcaneStaff(r),
			1 => CraftHolyStaff(r),
			2 => CraftWand(r),
			_ => CraftArcaneStaff(r),
		};
	}
	public Weapon CraftWeapon(WeaponClass wc, Rarity r)
	{
		return wc switch
		{
			WeaponClass.OneHandedSword => CraftOneHandedSword(r),
			WeaponClass.TwoHandedSword => CraftTwoHandedSword(r),
			WeaponClass.OneHandedMace => CraftOneHandedMace(r),
			WeaponClass.TwoHandedMace => CraftTwoHandedMace(r),
			WeaponClass.OneHandedAxe => CraftOneHandedAxe(r),
			WeaponClass.TwoHandedAxe => CraftTwoHandedAxe(r),
			WeaponClass.Dagger => CraftDagger(r),
			WeaponClass.Bow => CraftBow(r),
			WeaponClass.Crossbow => CraftCrossbow(r),
			WeaponClass.ArcaneStaff => CraftArcaneStaff(r),
			WeaponClass.HolyStaff => CraftHolyStaff(r),
			WeaponClass.Wand => CraftWand(r),
			_ => CraftOneHandedSword(r),
		};
	}

	private Weapon CraftOneHandedSword(Rarity r)
	{
		return Craft("1H Sword", false, r, WeaponClass.OneHandedSword, 1, 1.5f, 0.05f, 0.5f,
					EquipmentMod.Strength, 10f, EquipmentMod.Dexterity, 10f, EquipmentMod.AttackSpeed, 0.1f);
	}

	private Weapon CraftTwoHandedSword(Rarity r)
	{
		return Craft("2H Sword", true, r, WeaponClass.TwoHandedSword, 0.5f, 2.0f, 0.05f, 0.75f,
					EquipmentMod.Strength, 10f, EquipmentMod.Dexterity, 10f, EquipmentMod.CritDamage, 0.1f);
	}

	private Weapon CraftOneHandedMace(Rarity r)
	{
		return Craft("1H Mace", false, r, WeaponClass.OneHandedMace, 1.0f, 1.5f, 0.05f, 0.5f,
					EquipmentMod.Vitality, 10f, EquipmentMod.Fortitude, 10f, EquipmentMod.Resistance, 0.1f);
	}

	private Weapon CraftTwoHandedMace(Rarity r)
	{
		return Craft("2H Mace", true, r, WeaponClass.TwoHandedMace, 0.5f, 2.0f, 0.03f, 0.75f,
					EquipmentMod.Strength, 10f, EquipmentMod.Vitality, 10f, EquipmentMod.CritDamage, 0.1f);
	}

	private Weapon CraftOneHandedAxe(Rarity r)
	{
		return Craft("1H Axe", false, r, WeaponClass.OneHandedAxe, 1.0f, 1.5f, 0.0f, 0.75f,
					EquipmentMod.Strength, 10f, EquipmentMod.CritRate, 0.1f, EquipmentMod.CritDamage, 0.1f);
	}

	private Weapon CraftTwoHandedAxe(Rarity r)
	{
		return Craft("2H Axe", true, r, WeaponClass.TwoHandedAxe, 0.5f, 2.0f, 0.06f, 1.0f,
					EquipmentMod.Strength, 10f, EquipmentMod.CritRate, 0.1f, EquipmentMod.CritDamage, 0.1f);
	}

	private Weapon CraftDagger(Rarity r)
	{
		return Craft("Dagger", false, r, WeaponClass.Dagger, 1.5f, 1.0f, 0.06f, 0.75f,
					EquipmentMod.Dexterity, 10f, EquipmentMod.CritRate, 0.1f, EquipmentMod.CritRate, 0.1f);
	}

	private Weapon CraftBow(Rarity r)
	{
		return Craft("Bow", false, r, WeaponClass.Bow, 1.0f, 1.5f, 0.06f, 0.75f,
					EquipmentMod.Dexterity, 10f, EquipmentMod.AttackSpeed, 0.1f, EquipmentMod.CritRate, 0.1f);
	}

	private Weapon CraftCrossbow(Rarity r)
	{
		return Craft("Crossbow", false, r, WeaponClass.Crossbow, 0.5f, 1.5f, 0.06f, 0.75f,
			EquipmentMod.Dexterity, 10f, EquipmentMod.CritRate, 0.1f, EquipmentMod.CritDamage, 0.1f);
	}

	private Weapon CraftArcaneStaff(Rarity r)
	{
		return Craft("Arcane Staff", false, r, WeaponClass.ArcaneStaff, 0.75f, 1.25f, 0.05f, 0.5f,
					EquipmentMod.Intelligence, 10f, EquipmentMod.Wisdom, 10f, EquipmentMod.CritRate, 0.1f);
	}

	private Weapon CraftHolyStaff(Rarity r)
	{
		return Craft("Holy Staff", false, r, WeaponClass.HolyStaff, 0.75f, 1.5f, 0.0f, 0.5f,
					EquipmentMod.Intelligence, 10f, EquipmentMod.Wisdom, 10f, EquipmentMod.AttackSpeed, 0.1f);
	}

	private Weapon CraftWand(Rarity r)
	{
		return Craft("Wand", false, r, WeaponClass.Wand, 1.0f, 1.0f, 0.06f, 0.5f,
					EquipmentMod.Wisdom, 10f, EquipmentMod.Intelligence, 10f, EquipmentMod.CritDamage, 0.1f);
	}

	private float GetModifier()
	{
		//random number between -15 and 15
		//generate 0-3000
		//reduce it to 0.00-30.00
		//round to integer, and turn into %
		double randomNumber = (double)random.Random.Next(0, 3001);
		randomNumber = Math.Round(randomNumber / 100) - 15;
		return (float)(randomNumber / 100f);
	}
	private WeaponClass RandomWeaponClass()
	{
		WeaponClass wc = (WeaponClass)random.Random.Next(0, 12);
		return wc;
	}
}
