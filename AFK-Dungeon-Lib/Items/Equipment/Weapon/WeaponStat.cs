namespace AFK_Dungeon_Lib.Items.Equipment.Weapon;
public class WeaponStat
{
	public EquipmentMod StatType { get; }
	public float StatValue { get; }

	public WeaponStat(EquipmentMod type, float value)
	{
		StatType = type;
		StatValue = value;
	}
}
