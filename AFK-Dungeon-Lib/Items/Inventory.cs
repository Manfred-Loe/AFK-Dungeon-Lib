using AFK_Dungeon_Lib.Items.Equipment;

namespace AFK_Dungeon_Lib.Items;
public class Inventory
{
	public int Gold { get; set; }
	public int Crystals { get; set; }
	public int MetalScraps { get; set; }
	public int EtherScraps { get; set; }
	public int HighQualityMetal { get; set; }
	public int HighQualityEther { get; set; }
	public List<IEquipment> Equipment { get; }

	public Inventory()
	{
		Equipment = new List<IEquipment>();
	}
}
