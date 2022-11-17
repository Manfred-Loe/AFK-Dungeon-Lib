namespace AFK_Dungeon_Lib.Items.Equipment.Offhand;
public class Offhand : Gear
{
	public bool TwoHandEquip { get; }
	public SpellModifier Modifier { get; }
	public OffhandClass OffhandClass { get; }

	public Offhand() : this("Blank Shield", 0, Rarity.Common, new List<EquipmentStat>(), false, 0, OffhandClass.Shield) { }

	public Offhand(string name, int level, Rarity r,
			List<EquipmentStat> stats, bool twoHandEquip, SpellModifier modifier, OffhandClass oc) : base(name, level, r, EquipmentType.OffHand, stats)
	{
		TwoHandEquip = twoHandEquip;
		Modifier = modifier;
		OffhandClass = oc;
	}
}
