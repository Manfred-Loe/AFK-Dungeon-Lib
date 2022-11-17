namespace AFK_Dungeon_Lib.Items.Equipment;
public class Gear : IEquipment
{
	public string ItemName { get; set; }
	public int Level { get; set; }
	public Rarity Rarity { get; set; }
	public EquipmentType Type { get; set; }
	public List<EquipmentStat> ItemStats { get; set; }

	public Gear(string name, int level, Rarity r, EquipmentType t, List<EquipmentStat> stats)
	{
		this.ItemName = name;
		this.Level = level;
		this.Rarity = r;
		this.Type = t;
		ItemStats = stats;
	}
	public EquipmentMod GetStatType(int i) { return ItemStats[i].StatType; }
	public List<EquipmentMod> GetAllStatTypes()
	{
		var list = new List<EquipmentMod>();
		foreach (var x in ItemStats) { list.Add(x.StatType); }
		return list;
	}
	public int GetStatCount() { return ItemStats.Count; }
	public float GetStatValue(int i) { return ItemStats[i].StatValue; }
	public List<float> GetAllStatValues()
	{
		var list = new List<float>();
		foreach (var x in ItemStats) { list.Add((float)x.StatValue); }
		return list;
	}
	public void LevelUp()
	{
		if (Level < 10)
		{
			foreach (var stat in ItemStats)
			{
				if (stat.StatType != EquipmentMod.AttackSpeed)
				{
					stat.StatValue = MathFunc.Round(stat.StatValue * 1.5f, 2);
				}
				else
				{
					stat.StatValue = MathFunc.Round(stat.StatValue * 1.05f, 2);
				}
			}
		}
	}
}
