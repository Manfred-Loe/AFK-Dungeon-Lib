namespace AFK_Dungeon_Lib.Items.Equipment;

public enum EquipmentMod
{
	Strength = 0,
	Dexterity = 1,
	Vitality = 2,
	Intelligence = 3,
	Wisdom = 4,
	Fortitude = 5,
	Will = 6,
	Defense = 7,
	Resistance = 8,
	CritRate = 9,
	CritDamage = 10,
	AttackSpeed = 11,
}
static class WeaponModsExtension
{
	public static string ToString(this EquipmentMod type)
	{
		return type switch
		{
			EquipmentMod.Strength => "Strength",
			EquipmentMod.Dexterity => "Dexterity",
			EquipmentMod.Vitality => "Vitality",
			EquipmentMod.Intelligence => "Intelligence",
			EquipmentMod.Wisdom => "Wisdom",
			EquipmentMod.Fortitude => "Fortitude",
			EquipmentMod.Will => "Will",
			EquipmentMod.Defense => "Defense",
			EquipmentMod.Resistance => "Resistance",
			EquipmentMod.CritRate => "Crit Rate",
			EquipmentMod.CritDamage => "Crit Damage",
			EquipmentMod.AttackSpeed => "Attack Speed",
			_ => "Error Incorrect value",
		};
	}
}
