namespace AFK_Dungeon_Lib.Items.Equipment;

//TStat = Stat Type, TEnum = Enum Type
public interface IEquipment
{
	string ItemName { get; set; }
	int Level { get; set; }
	Rarity Rarity { get; set; }
	EquipmentType Type { get; set; }
	List<EquipmentStat> ItemStats { get; set; }

	EquipmentMod GetStatType(int i);
	List<EquipmentMod> GetAllStatTypes();
	int GetStatCount();
	float GetStatValue(int i);
	List<float> GetAllStatValues();
	void LevelUp();
}
