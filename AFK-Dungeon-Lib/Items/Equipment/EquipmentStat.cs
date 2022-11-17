namespace AFK_Dungeon_Lib.Items.Equipment;
public class EquipmentStat
{
	public EquipmentMod StatType { get; }
	public float StatValue { get; set; }

	public EquipmentStat(EquipmentMod statType, float value)
	{
		StatType = statType;
		StatValue = value;
	}
}
