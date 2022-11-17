namespace AFK_Dungeon_Lib.Items.Equipment;
public class Helmet : Gear
{
	public Helmet() : base("Blank Helmet", 0, Rarity.Common, EquipmentType.Helmet, new List<EquipmentStat>()) { }

	public Helmet(string name, int level, Rarity r,
		List<EquipmentStat> stats) : base(name, level, r, EquipmentType.Helmet, stats) { }
}
