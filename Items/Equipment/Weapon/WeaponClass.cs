namespace AFK_Dungeon_Lib.Items.Equipment.Weapon;
public enum WeaponClass
{
	OneHandedSword = 0, //str dex/attack speed
	TwoHandedSword = 1, //str dex/cd
	OneHandedMace = 2, //vit fort/res
	TwoHandedMace = 3, //str vit/cd
	OneHandedAxe = 4, //str cr/cd
	TwoHandedAxe = 5, //str cr/cd
	Dagger = 6, //dex cr/cd
	Bow = 7, //dex attack speed/cr
	Crossbow = 8, //dex cr/cd
	ArcaneStaff = 9, //int wis/cr
	HolyStaff = 10, //int wis/attack speed
	Wand = 11 //wis int/cd
}

static class WeaponClassExtension
{
	public static string ToString(this WeaponClass type)
	{
		return type switch
		{
			WeaponClass.OneHandedSword => "One Handed Sword",
			WeaponClass.TwoHandedSword => "Two Handed Sword",
			WeaponClass.OneHandedMace => "One Handed Mace",
			WeaponClass.TwoHandedMace => "Two Handed Mace",
			WeaponClass.OneHandedAxe => "One Handed Axe",
			WeaponClass.TwoHandedAxe => "Two Handed Axe",
			WeaponClass.Dagger => "Dagger",
			WeaponClass.Bow => "Bow",
			WeaponClass.Crossbow => "Crossbow",
			WeaponClass.ArcaneStaff => "Arcane Staff",
			WeaponClass.HolyStaff => "Holy Staff",
			WeaponClass.Wand => "Wand",
			_ => "Error Incorrect value",
		};
	}
}