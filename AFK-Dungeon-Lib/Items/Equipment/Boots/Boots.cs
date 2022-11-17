namespace AFK_Dungeon_Lib.Items.Equipment;
public class Boots : Gear
{
	public Boots() : base("Blank Helmet", 0, Rarity.Common, EquipmentType.Boots, new List<EquipmentStat>()) { }

	public Boots(string name, int level, Rarity r,
		List<EquipmentStat> stats) : base(name, level, r, EquipmentType.Boots, stats) { }
}
