namespace AFK_Dungeon_Lib.Items.Equipment;
public enum EquipmentType
{
	Helmet,
	Chest,
	Boots,
	Weapon,
	OffHand
}

static class EquipmentTypeExtension
{
	public static string ToString(this EquipmentType type)
	{
		return type switch
		{
			EquipmentType.Helmet => "Helmet",
			EquipmentType.Chest => "Chest",
			EquipmentType.Boots => "Boots",
			EquipmentType.Weapon => "Weapon",
			EquipmentType.OffHand => "OffHand",
			_ => "Error Incorrect value",
		};
	}
}
