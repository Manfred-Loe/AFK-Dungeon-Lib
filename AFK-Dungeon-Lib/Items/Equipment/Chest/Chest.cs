namespace AFK_Dungeon_Lib.Items.Equipment;
public class Chest : Gear
{
	public Chest() : base("Blank Helmet", 0, Rarity.Common, EquipmentType.Chest, new List<EquipmentStat>()) { }

	public Chest(string name, int level, Rarity r,
		List<EquipmentStat> stats) : base(name, level, r, EquipmentType.Chest, stats) { }
}
