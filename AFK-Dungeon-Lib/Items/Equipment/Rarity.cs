namespace AFK_Dungeon_Lib.Items.Equipment;
public enum Rarity
{
	Common = 0,
	Uncommon = 1,
	Rare = 2,
	Legendary = 3,
	Unique = 4
}
static class RarityExtension
{
	public static string ToString(this Rarity rarity)
	{
		return rarity switch
		{
			Rarity.Common => "Common",
			Rarity.Uncommon => "Uncommon",
			Rarity.Rare => "Rare",
			Rarity.Legendary => "Legendary",
			Rarity.Unique => "Unique",
			_ => "Error Incorrect value",
		};
	}
}