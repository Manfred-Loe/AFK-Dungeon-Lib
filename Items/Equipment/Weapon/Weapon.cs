namespace AFK_Dungeon_Lib.Items.Equipment.Weapon;
public class Weapon : IEquipment
{
	public string ItemName { get; set; }
	public int Level { get; set; }
	public Rarity Rarity { get; set; }
	public EquipmentType Type { get; set; }
	public List<EquipmentStat> ItemStats { get; set; }
	public bool TwoHanded { get; }
	public float AttackSpeed { get; }
	public float DamageModifier { get; }
	public float CritChance { get; }
	public float CritDamage { get; }
	public WeaponClass WeaponClass { get; }
	public float QualityModifier { get; }

	public Weapon()
	{
		this.ItemName = "Blank Weapon";
		this.Level = 0;
		this.Rarity = Rarity.Common;
		this.Type = EquipmentType.Weapon;
		TwoHanded = true;
		AttackSpeed = 0.7f;
		DamageModifier = 2.0f;
		CritChance = 0.05f;
		CritDamage = 0.75f;
		WeaponClass = WeaponClass.TwoHandedSword;
		QualityModifier = 0.0f;
		ItemStats = new List<EquipmentStat>
		{
			new EquipmentStat(EquipmentMod.Strength, 0f)
		};
	}

	public Weapon(string name, bool twoHand, float aSpeed, float dmg, float critchance, float critdamage, WeaponClass wc, Rarity r, float q, int level, List<EquipmentStat> stats)
	{
		this.ItemName = name;
		this.Level = level;
		this.Rarity = r;
		this.Type = EquipmentType.Weapon;
		this.TwoHanded = twoHand;
		this.AttackSpeed = aSpeed;
		this.DamageModifier = dmg;
		this.CritChance = critchance;
		this.CritDamage = critdamage;
		this.WeaponClass = wc;
		this.QualityModifier = q;
		this.ItemStats = stats;

		foreach (EquipmentStat x in ItemStats)
		{
			if (x.StatType == EquipmentMod.AttackSpeed)
			{
				AttackSpeed *= (1f + x.StatValue);
			}
			else if (x.StatType == EquipmentMod.CritRate)
			{
				CritChance *= (1f + x.StatValue);
			}
			else if (x.StatType == EquipmentMod.CritDamage)
			{
				CritDamage *= (1f + x.StatValue);
			}
		}

		//round the numbers to 2 decimals
		DamageModifier = MathFunc.Round(DamageModifier, 2);
		AttackSpeed = MathFunc.Round(AttackSpeed, 2);
		CritChance = MathFunc.Round(CritChance, 2);
		CritDamage = MathFunc.Round(CritDamage, 2);
	}

	public EquipmentMod GetStatType(int i) { return ItemStats[i].StatType; }

	public List<EquipmentMod> GetAllStatTypes()
	{
		var list = new List<EquipmentMod>();
		foreach (var x in ItemStats) { list.Add(x.StatType); }
		return list;
	}
	public int GetStatCount() { return ItemStats.Count; }
	public float GetStatValue(int i) { return ItemStats[i].StatValue; }
	public List<float> GetAllStatValues()
	{
		var list = new List<float>();
		foreach (var x in ItemStats) { list.Add(x.StatValue); }
		return list;
	}

	public string DisplayString()
	{
		string statString = "";
		foreach (EquipmentStat stat in ItemStats)
		{
			statString += stat.StatType.ToString() + " " + stat.StatValue + " ";
		}

		return string.Format("Name: {0}: Item Level: {1}: Item Rarity: {2}: Weapon Class: {3}: \n" +
								"Damage Modifier: {4}%: Base Crit: {5}%: Base Crit Damage: {6}%: Attack Speed: {7}: \n" +
								"Quality Modifier: {8}%: Stats: {9}:",
								ItemName, Level, Rarity.ToString(), WeaponClass.ToString(),
								DamageModifier * 100, CritChance * 100, CritDamage * 100, AttackSpeed,
								QualityModifier * 100, statString);
	}
}